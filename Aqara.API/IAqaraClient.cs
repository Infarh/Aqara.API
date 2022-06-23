using System.ComponentModel;
using Aqara.API.Exceptions;
using Aqara.API.Models;

namespace Aqara.API;

public interface IAqaraClient
{
    /// <summary>Запрос кода авторизации</summary>
    /// <param name="Account">Аккаунт Aqara (адрес электронной почты, на который придёт код авторизации, либо телефонный номер для СМС)</param>
    /// <param name="AccessTokenValidity">Интервал времени, в который запрошенный код будет актуальным</param>
    /// <param name="AccountType">Тип аккаунта (логин Aqara, проект, виртуальный аккаунт)</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Строка с кодом авторизации - если её вышлет сервер (не вышлет - код придёт на указанный адрес почты)</returns>
    /// <exception cref="RequestAuthorizationCodeException">В случае ошибки авторизации</exception>
    Task<string?> GetAuthorizationKey(
        string Account,
        string AccessTokenValidity = "1h",
        AccountType AccountType = AccountType.Aqara,
        CancellationToken Cancel = default);

    /// <summary>Запрос токена авторизации на основе кода авторизации</summary>
    /// <param name="VerificationCode">Код авторизации, полученный при помощи <see cref="AqaraClient.GetAuthorizationKey"/></param>
    /// <param name="Account">Аккаунт, указанный в запросе кода авторизации <see cref="AqaraClient.GetAuthorizationKey"/></param>
    /// <param name="AccountType">Тип аккаунта из запроса <see cref="AqaraClient.GetAuthorizationKey"/></param>
    /// <param name="Cancel"></param>
    /// <returns>Токен авторизации</returns>
    /// <exception cref="RequestAccessTokenException">В случае если не удалось получить данные от сервиса</exception>
    Task<AccessTokenInfo> GetAccessToken(
        string VerificationCode,
        string? Account = null,
        AccountType AccountType = AccountType.Aqara,
        CancellationToken Cancel = default);

    /// <summary>Обновить токен авторизации</summary>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Новый токен авторизации</returns>
    /// <exception cref="InvalidOperationException">Возникает при отсутствии токена обновления токена авторизации</exception>
    /// <exception cref="RefreshAccessTokenException">В случае если не удалось получить данные от сервиса</exception>
    Task<AccessTokenInfo> RefreshAccessToken(CancellationToken Cancel = default);

    /// <summary>Обновить токен авторизации</summary>
    /// <param name="RefreshToken">Токен обновления токена авторизации</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Новый токен авторизации</returns>
    /// <exception cref="InvalidOperationException">Возникает при отсутствии токена обновления токена авторизации</exception>
    /// <exception cref="RefreshAccessTokenException">В случае если не удалось получить данные от сервиса</exception>
    Task<AccessTokenInfo> RefreshAccessToken(string RefreshToken, CancellationToken Cancel = default);

    /// <summary>Получить список местоположений</summary>
    /// <param name="ParentPositionId">Родительское местоположение (если указано)</param>
    /// <param name="Page">Номер страницы (должно быть больше 0)</param>
    /// <param name="PageSize">Размер страницы должен быть больше 0</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Массив местоположений</returns>
    /// <exception cref="GetPositionsException">В случае если не удалось получить данные от сервиса</exception>
    Task<(PositionInfo[] Positions, int TotalCount)> GetPositions(string? ParentPositionId = null, int? Page = 1, int? PageSize = 30, CancellationToken Cancel = default);

    /// <summary>Получить перечень устройств по заданному местоположению (если положеие не указано, то возвращается полный список устройств</summary>
    /// <param name="PositionId">Идентификатор местоположения (если не указан, то будет возвращён полный список всех устройств)</param>
    /// <param name="Page">Номер страницы (начиная с 1)</param>
    /// <param name="PageSize">Количество страниц (должно быть больше 0)</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Массив устройств указанного местоположения</returns>
    /// <exception cref="GetDevicesByPositionException">В случае если не удалось получить данные от сервиса</exception>
    Task<(DeviceInfo[] Devices, int TotalCount)> GetDevicesByPosition(string? PositionId = null, int? Page = 1, int PageSize = 30, CancellationToken Cancel = default);

    /// <summary>Получить перечень возможностей устройства</summary>
    /// <param name="Model">Идентификатор модели устройства</param>
    /// <param name="ResourceId">Идентификатор ресурса (если не указан, будет возвращена информация о всех возможностях)</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Массив с информацией о возможностях устройства</returns>
    /// <exception cref="GetDeviceModelFeaturesException">В случае если не удалось получить данные от сервиса</exception>
    Task<DeviceFeatureInfo[]> GetDeviceModelFeatures(string Model, string? ResourceId = null, CancellationToken Cancel = default);

    /// <summary>Получить статистические данные о параметрах устройства</summary>
    /// <param name="DeviceId">Идентификатор устройства</param>
    /// <param name="FeatureId">Перечисление идентификаторов интересующих параметров</param>
    /// <param name="AggregationType">Виды статистических данных</param>
    /// <param name="StartTime">Время начала сбора данных</param>
    /// <param name="Dimension">Разрешающая способность выборки</param>
    /// <param name="EndTime">Время окончания выборки</param>
    /// <param name="Size">Размер выборки</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Массив со значениями выборки</returns>
    /// <exception cref="InvalidEnumArgumentException">При некорректном значении параметра <paramref name="Dimension"/></exception>
    /// <exception cref="GetDeviceFeatureStatisticException">В случае если не удалось получить данные от сервиса</exception>
    Task<StatisticValueInfo[]> GetDeviceFeatureStatistic(
        string DeviceId,
        IEnumerable<string> FeatureId,
        FeatureStatisticAggregationType AggregationType,
        DateTime StartTime,
        FeatureStatisticAggregationDimension Dimension = FeatureStatisticAggregationDimension.Interval30m,
        DateTime? EndTime = null,
        int? Size = null,
        CancellationToken Cancel = default);

    /// <summary>Получить значения параметров указанных устройств</summary>
    /// <param name="Features">Параметры запроса устройств и их параметров</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Значения параметров устройств</returns>
    /// <exception cref="GetDevicesFeaturesValuesException">В случае если не удалось получить данные от сервиса</exception>
    Task<DeviceFeatureValue[]> GetDevicesFeaturesValues((string DeviceId, string[] FeatureId)[] Features, CancellationToken Cancel = default);

    /// <summary>Установка значения параметров устройств</summary>
    /// <param name="Values">Идентификаторы устанавливаемых параметров устройств</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Задача установки значения параметров</returns>
    /// <exception cref="SetDevicesFeaturesValuesException">В случае если не удалось получить данные от сервиса</exception>
    Task SetDevicesFeaturesValues((string DeviceId, (string FeatureId, double Value)[] Values)[] Values, CancellationToken Cancel = default);
}

public static class AqaraClientExtensions
{
    /// <summary>Получить значения параметров устройства</summary>
    /// <param name="client">Клиент Aqara API</param>
    /// <param name="DeviceId">Идентификатор устройства</param>
    /// <param name="FeaturesIds">Идентификаторы требуемых параметров</param>
    /// <returns>Массив значений запрошенных параметров</returns>
    /// <exception cref="SetDevicesFeaturesValuesException">В случае если не удалось получить данные от сервиса</exception>
    public static async Task<DeviceFeatureValue[]> GetDeviceFeaturesValues(this IAqaraClient client, string DeviceId, params string[] FeaturesIds) => await client.GetDevicesFeaturesValues(new[] { (DeviceId, FeaturesIds) }).ConfigureAwait(false);

    /// <summary>Получить значения параметров устройства</summary>
    /// <param name="client">Клиент Aqara API</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <param name="DeviceId">Идентификатор устройства</param>
    /// <param name="FeaturesIds">Идентификаторы требуемых параметров</param>
    /// <returns>Массив значений параметров устройства</returns>
    /// <exception cref="SetDevicesFeaturesValuesException">В случае если не удалось получить данные от сервиса</exception>
    public static async Task<DeviceFeatureValue[]> GetDeviceFeaturesValues(this IAqaraClient client, CancellationToken Cancel, string DeviceId, params string[] FeaturesIds) => await client.GetDevicesFeaturesValues(new[] { (DeviceId, FeaturesIds) }, Cancel).ConfigureAwait(false);

    /// <summary>Получить значение параметра устройства</summary>
    /// <param name="client">Клиент Aqara API</param>
    /// <param name="DeviceId">Идентификатор устройства</param>
    /// <param name="FeatureId">Идентификатор параметра</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Значение параметра</returns>
    /// <exception cref="SetDevicesFeaturesValuesException">В случае если не удалось получить данные от сервиса</exception>
    public static async Task<DeviceFeatureValue> GetDeviceFeatureValue(this IAqaraClient client, string DeviceId, string FeatureId, CancellationToken Cancel = default)
    {
        var values = await client.GetDeviceFeaturesValues(Cancel, DeviceId, FeatureId).ConfigureAwait(false);
        return values[0];
    }

    /// <summary>Установка значения параметров устройства</summary>
    /// <param name="client">Клиент Aqara API</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <param name="DeviceId">Идентификатор устройства</param>
    /// <param name="Values">Устанавливаемые параметры</param>
    /// <returns>Задача установки параметров устройства</returns>
    public static async Task SetDeviceFeaturesValues(this IAqaraClient client, CancellationToken Cancel, string DeviceId, params (string FeatureId, double Value)[] Values)
    {
        await client.SetDevicesFeaturesValues(new[] { (DeviceId, Values) }, Cancel).ConfigureAwait(false);
    }

    /// <summary>Установка значения параметров устройства</summary>
    /// <param name="client">Клиент Aqara API</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <param name="DeviceId">Идентификатор устройства</param>
    /// <param name="FeatureId">Идентификатор параметра</param>
    /// <param name="Value">Значение параметра</param>
    /// <returns>Задача установки параметров устройства</returns>
    public static async Task SetDeviceFeaturesValues(this IAqaraClient client, CancellationToken Cancel, string DeviceId, string FeatureId, double Value)
    {
        await client.SetDevicesFeaturesValues(new[] { (DeviceId, new []{ (FeatureId, Value) }) }, Cancel).ConfigureAwait(false);
    }
}