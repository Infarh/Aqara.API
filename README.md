# Aqara.API

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE.txt)

Библиотека для взаимодействия с [Open API Aqara](https://open.aqara.com/) версии 3.0. Позволяет авторизовываться, получать список устройств, управлять ими, собирать статистику и работать с местоположениями в экосистеме Aqara.

## Возможности

- **Авторизация** — получение кода подтверждения, запрос и обновление токена доступа, поддержка нескольких типов аккаунтов (Aqara, проектный, виртуальный)
- **Управление устройствами** — получение списка устройств, поиск по идентификатору и имени, чтение и установка значений параметров
- **Поддержка моделей устройств** — типизированные классы для популярных устройств Aqara (датчики, выключатели, шлюзы, розетки)
- **Статистика** — получение агрегированных данных по параметрам устройств с настраиваемым разрешением и типами агрегации
- **Местоположения** — получение иерархической структуры местоположений
- **Региональная поддержка** — предустановленные URL для Европы, России, Кореи, США и Китая

## Установка

### Через dotnet CLI

```bash
dotnet add package Aqara.API
```

### Добавление в проект

```xml
<ItemGroup>
  <PackageReference Include="Aqara.API" Version="*" />
</ItemGroup>
```

## Быстрый старт

### Конфигурация

Для работы с API Aqara необходимы учётные данные приложения, полученные на [портале разработчика Aqara](https://open.aqara.com/).

```csharp
// Конфигурация клиента
var config = new AqaraClientConfig
{
    AppId = "your-app-id-24-chars",
    AppKey = "your-app-key-32-chars",
    KeyId = "your-key-id-20-chars"
};
```

### Создание клиента

Библиотека использует стандартные механизмы .NET DI и `IHttpClientFactory`. Рекомендуемый способ подключения:

```csharp
// Регистрация в DI-контейнере
services.AddSingleton(config);
services.AddSingleton<IAccessTokenSource, AccessTokenFileSource>(
    _ => new AccessTokenFileSource("token.json"));
services.AddHttpClient<IAqaraClient, AqaraClient>(client =>
{
    client.BaseAddress = new Uri(Servers.Russia);
});
```

### Авторизация

```csharp
// 1. Запрос кода подтверждения (придёт на email)
await client.GetAuthorizationKey("user@example.com");

// 2. Получение токена доступа
var token = await client.GetAccessToken("verification-code-from-email");
```

### Получение устройств

#### Через IAqaraClient

```csharp
// Все устройства
var (devices, totalCount) = await client.GetDevicesByPosition();

// Устройства конкретного местоположения
var (devices, totalCount) = await client.GetDevicesByPosition("position-id");

// Параметры устройства
var temperature = await client.GetDeviceFeatureValue("device-id", Features.Temperature);
```

#### Через IDeviceManager

```csharp
var manager = new DeviceManager(client, logger);

// Загрузить все устройства
var devices = await manager.GetDevices();

// Найти устройство по имени
var device = await manager.GetDeviceByName("Гостиная");

// Получить доступные возможности устройства
var features = await device.GetFeatures();
```

### Установка значений параметров

```csharp
// Включить выключатель
await client.SetDeviceFeaturesValues(
    deviceId: "device-id",
    ("4.1.85", 1.0)  // SwitchState = On
);
```

## Архитектура

### Основные компоненты

| Компонент | Описание |
|---|---|
| `IAqaraClient` / `AqaraClient` | Основной клиент API. Содержит методы для всех операций: авторизация, получение устройств, управление параметрами, статистика |
| `IDeviceManager` / `DeviceManager` | Менеджер устройств с кешированием. Загружает устройства, поддерживает поиск по id и имени |
| `Device` (и наследники) | Базовый класс устройства. Для поддерживаемых моделей автоматически создаются типизированные экземпляры |
| `IAccessTokenSource` | Абстракция хранилища токена доступа. Встроенная реализация `AccessTokenFileSource` сохраняет токен в JSON-файл |
| `AqaraClientConfig` | Конфигурация приложения: AppId, AppKey, KeyId |

### Поддерживаемые устройства

| Класс | Модель | Описание |
|---|---|---|
| `DeviceLumiWeatherV1` | `lumi.weather.v1` | Датчик температуры, влажности и давления |
| `DeviceLumiSwitchL1aeu1` | `lumi.switch.l1aeu1` | Настенный выключатель с одной клавишей |
| `DeviceLumiSwitchL2aeu1` | `lumi.switch.l2aeu1` | Настенный выключатель с двумя клавишами |
| `DeviceLumiAirmonitorAcn01` | `lumi.airmonitor.acn01` | Монитор качества воздуха |
| `DeviceLumiPlugMaeu01` | `lumi.plug.maeu01` | Розетка |
| `DeviceLumiSensorMotionAq2` | `lumi.sensor_motion.aq2` | Датчик движения |
| `DeviceLumiSensorMagnetAq2` | `lumi.sensor_magnet.aq2` | Датчик открытия (магнитный) |
| `DeviceLumiRemoteB1acn01` | `lumi.remote.b1acn01` | Пульт дистанционного управления |
| `DeviceLumiSensorWleakAq1` | `lumi.sensor_wleak.aq1` | Датчик протечки воды |
| `DeviceLumiGatewayIragl7` | `lumi.gateway.iragl7` | Шлюз Aqara M1 |

Для моделей, отсутствующих в списке, создаётся экземпляр базового класса `Device`.

### Региональные серверы

```csharp
Servers.Europe  // https://open-ger.aqara.com/v3.0/open/api
Servers.Russia  // https://open-ru.aqara.com/v3.0/open/api
Servers.Korea   // https://open-kr.aqara.com/v3.0/open/api
Servers.USA     // https://open-usa.aqara.com/v3.0/open/api
Servers.China   // https://open-cn.aqara.com/v3.0/open/api
```

## API методы

### Авторизация

| Метод | Описание |
|---|---|
| `GetAuthorizationKey` | Запросить код подтверждения на email или телефон |
| `GetAccessToken` | Получить токен доступа по коду подтверждения |
| `RefreshAccessToken` | Обновить токен доступа по refresh-токену |
| `IsAuthorisationNeeded` | Проверить, требуется ли авторизация |
| `IsAccessTokenValid` | Проверить валидность текущего токена |

### Устройства и местоположения

| Метод | Описание |
|---|---|
| `GetPositions` | Получить список местоположений (с пагинацией) |
| `GetDevicesByPosition` | Получить список устройств по местоположению |
| `GetDeviceModelFeatures` | Получить возможности устройства по модели |
| `GetDevicesFeaturesValues` | Получить значения параметров нескольких устройств |
| `SetDevicesFeaturesValues` | Установить значения параметров устройств |

### Статистика

| Метод | Описание |
|---|---|
| `GetDeviceFeatureStatistic` | Получить статистические данные параметра за период |

### Расширения (`AqaraClientExtensions`)

| Метод | Описание |
|---|---|
| `GetDeviceFeaturesValues` | Получить значения нескольких параметров одного устройства |
| `GetDeviceFeatureValue` | Получить значение одного параметра устройства |
| `SetDeviceFeaturesValues` | Установить значения параметров одного устройства |

## Исключения

Все исключения, специфичные для API, находятся в пространстве имён `Aqara.API.Exceptions`:

- `RequestAuthorizationCodeException` — ошибка запроса кода авторизации
- `RequestAccessTokenException` — ошибка получения токена доступа
- `RefreshAccessTokenException` — ошибка обновления токена
- `GetPositionsException` — ошибка получения местоположений
- `GetDevicesByPositionException` — ошибка получения устройств
- `GetDeviceModelFeaturesException` — ошибка получения возможностей модели
- `GetDevicesFeaturesValuesException` — ошибка получения значений параметров
- `SetDevicesFeaturesValuesException` — ошибка установки значений параметров
- `GetDeviceFeatureStatisticException` — ошибка получения статистики

## Идентификаторы возможностей (Features)

Библиотека предоставляет константы основных идентификаторов параметров в классе `Features`:

```csharp
Features.Temperature   // "0.1.85" — значение температуры (t * 1000)
Features.SwitchState   // "4.1.85" — состояние выключателя (0/1)
```

Полный список возможностей для конкретного устройства можно получить через `GetDeviceModelFeatures`.

## Зависимости

- .NET 10.0
- `Microsoft.Extensions.Http` — фабрика HTTP-клиентов
- `Microsoft.Extensions.Options.ConfigurationExtensions` — поддержка конфигурации
- `Microsoft.Extensions.Options.DataAnnotations` — валидация конфигурации
- `Supernova.Enum.Generators` — генерация `ToString()` для перечислений

## Лицензия

Проект распространяется под лицензией MIT. Подробнее в файле [LICENSE.txt](LICENSE.txt).
