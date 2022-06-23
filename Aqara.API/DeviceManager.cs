using System.Collections.Concurrent;

using Aqara.API.Devices;
using Aqara.API.Models;

using Microsoft.Extensions.Logging;

namespace Aqara.API;

public interface IDeviceManager
{
    IAqaraClient API { get; }

    Task Authorize(string Account, string? Code = null);

    Task<ICollection<Device>> GetDevices();

    ValueTask<Device?> GetDeviceById(string Id);

    ValueTask<Device?> GetDeviceByName(string Name);
}

public class DeviceManager : IDeviceManager
{
    private readonly IAqaraClient _Client;
    private readonly ILogger<DeviceManager> _Logger;

    private readonly ConcurrentDictionary<DeviceInfo, Device> _Devices = new();
    private readonly ConcurrentDictionary<string, Device> _DeviceId = new();
    private readonly ConcurrentDictionary<string, Device> _DeviceNames = new();

    public IAqaraClient API => _Client;

    public DeviceManager(IAqaraClient Client, ILogger<DeviceManager> Logger)
    {
        _Client = Client;
        _Logger = Logger;
    }

    public async Task Authorize(string Account, string? Code = null)
    {
        if (Code is not { Length: > 0 })
            await _Client.GetAuthorizationKey(Account).ConfigureAwait(false);
        else
            await _Client.GetAccessToken(Code, Account).ConfigureAwait(false);
    }

    public async Task<ICollection<Device>> GetDevices()
    {
        var (response, total_count) = await _Client.GetDevicesByPosition().ConfigureAwait(false);
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
            var (result, _) = await _Client.GetDevicesByPosition(Page: page);

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
                lock (_Devices)
                {
                    _Devices.TryRemove(known, out _);
                    _DeviceId.TryRemove(known.Id, out _);
                    _DeviceNames.TryRemove(known.Name, out _);
                }
    }

    private void AddDevice(DeviceInfo Info)
    {
        if (_Devices.ContainsKey(Info)) return;
        lock (_Devices)
        {
            var device = Device.Create(this, Info);
            _Devices[Info] = device;
            _DeviceId[Info.Id] = device;
            _DeviceNames[Info.Name] = device;
        }
    }

    public async ValueTask<Device?> GetDeviceById(string Id)
    {
        if (_Devices.Count == 0)
            await GetDevices().ConfigureAwait(false);

        return _DeviceId.TryGetValue(Id, out var device) ? device : null;
    }

    public async ValueTask<Device?> GetDeviceByName(string Name)
    {
        if (_Devices.Count == 0)
            await GetDevices().ConfigureAwait(false);

        return _DeviceNames.TryGetValue(Name, out var device) ? device : null;
    }
}
