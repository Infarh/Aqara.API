using System.Collections.Concurrent;

using Aqara.API.Devices;
using Aqara.API.Models;

using Microsoft.Extensions.Logging;

namespace Aqara.API;

/// <summary>Менеджер устройств, представляющий контракт для получения и поиска устройств</summary>
public interface IDeviceManager
{
    /// <summary>Клиент API для взаимодействия с сервером Aqara</summary>
    IAqaraClient API { get; }

    /// <summary>Авторизует аккаунт пользователя по коду или инициирует получение ключа авторизации</summary>
    /// <param name="Account">Идентификатор аккаунта</param>
    /// <param name="Code">Код подтверждения, если известен</param>
    Task Authorize(string Account, string? Code = null, CancellationToken Cancel = default);

    /// <summary>Возвращает коллекцию устройств, доступных в аккаунте</summary>
    /// <returns>Коллекция найденных устройств</returns>
    Task<ICollection<Device>> GetDevices(CancellationToken Cancel = default);

    /// <summary>Находит устройство по его уникальному идентификатору</summary>
    /// <param name="Id">Уникальный идентификатор устройства</param>
    /// <returns>Экземпляр устройства или null если не найдено</returns>
    ValueTask<Device?> GetDeviceById(string Id, CancellationToken Cancel = default);

    /// <summary>Находит устройство по его имени</summary>
    /// <param name="Name">Имя устройства</param>
    /// <returns>Экземпляр устройства или null если не найдено</returns>
    ValueTask<Device?> GetDeviceByName(string Name, CancellationToken Cancel = default);
}

/// <summary>Реализация менеджера устройств с кешированием и поиском по id и имени</summary>
/// <param name="Client">Экземпляр API клиента</param>
/// <param name="Logger">Логгер для вывода сообщений</param>
public class DeviceManager(IAqaraClient Client, ILogger<DeviceManager> Logger) : IDeviceManager
{
    private readonly ILogger<DeviceManager> _Logger = Logger;

    private readonly Lock _DevicesLock = new();

    private readonly ConcurrentDictionary<DeviceInfo, Device> _Devices = new();

    private readonly ConcurrentDictionary<string, Device> _DeviceId = new();

    private readonly ConcurrentDictionary<string, Device> _DeviceNames = new();

    /// <summary>Клиент API для взаимодействия с сервером Aqara</summary>
    public IAqaraClient API => Client;

    /// <summary>Авторизует аккаунт пользователя по коду или инициирует получение ключа авторизации</summary>
    /// <param name="Account">Идентификатор аккаунта</param>
    /// <param name="Code">Код подтверждения, если известен</param>
    public async Task Authorize(string Account, string? Code = null, CancellationToken Cancel = default)
    {
        if (Code is not { Length: > 0 })
            await Client.GetAuthorizationKey(Account, Cancel: Cancel).ConfigureAwait(false);
        else
            await Client.GetAccessToken(Code, Account, Cancel: Cancel).ConfigureAwait(false);
    }

    /// <summary>Загружает все устройства из API и обновляет локальный кеш</summary>
    /// <returns>Коллекция текущих устройств в кеше</returns>
    public async Task<ICollection<Device>> GetDevices(CancellationToken Cancel = default)
    {
        var (response, total_count) = await Client.GetDevicesByPosition(Cancel: Cancel).ConfigureAwait(false);
        var received_count = response.Length;

        var ids = new HashSet<string>(total_count);

        foreach (var device_info in response)
        {
            AddDevice(device_info);
            ids.Add(device_info.Id);
        }

        var page = 2;
        while (received_count < total_count)
        {
            var (result, _) = await Client.GetDevicesByPosition(Page: page, Cancel: Cancel).ConfigureAwait(false);

            if (result.Length == 0) break;

            foreach (var device_info in result)
            {
                AddDevice(device_info);
                ids.Add(device_info.Id);
            }

            received_count += result.Length;
            page++;
        }

        ClearRemovedDevices(ids);
        return _Devices.Values;
    }

    private void ClearRemovedDevices(HashSet<string> ExistIds)
    {
        foreach (var known in _Devices.Keys.ToArray())
            if (!ExistIds.Contains(known.Id))
                lock (_DevicesLock)
                {
                    _Devices.TryRemove(known, out _);
                    _DeviceId.TryRemove(known.Id, out _);
                    _DeviceNames.TryRemove(known.Name, out _);
                }
    }

    private void AddDevice(DeviceInfo Info)
    {
        if (_Devices.ContainsKey(Info)) return;
        lock (_DevicesLock)
        {
            var device = Device.Create(this, Info);
            _Devices[Info] = device;
            _DeviceId[Info.Id] = device;
            _DeviceNames[Info.Name] = device;
        }
    }

    /// <summary>Ищет устройство по уникальному идентификатору, при пустом стане кеша загружает список устройств</summary>
    /// <param name="Id">Уникальный идентификатор устройства</param>
    /// <returns>Найденное устройство или null</returns>
    public async ValueTask<Device?> GetDeviceById(string Id, CancellationToken Cancel = default)
    {
        if (_Devices.IsEmpty)
            await GetDevices(Cancel).ConfigureAwait(false);

        return _DeviceId.TryGetValue(Id, out var device) ? device : null;
    }

    /// <summary>Ищет устройство по имени, при пустом стане кеша загружает список устройств</summary>
    /// <param name="Name">Имя устройства</param>
    /// <returns>Найденное устройство или null</returns>
    public async ValueTask<Device?> GetDeviceByName(string Name, CancellationToken Cancel = default)
    {
        if (_Devices.IsEmpty)
            await GetDevices(Cancel).ConfigureAwait(false);

        return _DeviceNames.TryGetValue(Name, out var device) ? device : null;
    }
}
