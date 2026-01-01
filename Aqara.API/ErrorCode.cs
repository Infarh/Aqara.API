using System.ComponentModel.DataAnnotations;

using EnumFastToStringGenerated;

namespace Aqara.API;

/// <summary>Тип ошибки</summary>
[EnumGenerator]
public enum ErrorCode : short
{
    ///<summary>Неизвестная ошибка</summary>
    [Display(Name = "Неизвестная ошибка")]
    Unknown = -1,

    ///<summary>Успех</summary>
    [Display(Name = "Успех")]
    Success = 0,

    ///<summary>Таймаут</summary>
    [Display(Name = "Таймаут")]
    Timeout = 100,

    ///<summary>Неверный пакет данных</summary>
    [Display(Name = "Неверный пакет данных")]
    InvalidDataPackage = 101,

    ///<summary>Пакет данных был изменён</summary>
    [Display(Name = "Пакет данных был изменён")]
    DataPackageHasAltered = 102,

    ///<summary>Пакет данных может быть утерян</summary>
    [Display(Name = "Пакет данных может быть утерян")]
    DataPackageMayLose = 103,

    ///<summary>Сервер перегружен</summary>
    [Display(Name = "Сервер перегружен")]
    ServerBusy = 104,

    ///<summary>Срок действия пакета данных истёк</summary>
    [Display(Name = "Срок действия пакета данных истёк")]
    DataPackageHasExpired = 105,

    ///<summary>Неверная подпись</summary>
    [Display(Name = "Неверная подпись")]
    InvalidSign = 106,

    ///<summary>Недопустимый appKey</summary>
    [Display(Name = "Недопустимый appKey")]
    IllegalAppKey = 107,

    ///<summary>Токен истёк</summary>
    [Display(Name = "Токен истёк")]
    TokenHasExpired = 108,

    ///<summary>Токен отсутствует</summary>
    [Display(Name = "Токен отсутствует")]
    TokenIsAbsence = 109,

    ///<summary>Ошибка параметров</summary>
    [Display(Name = "Ошибка параметров")]
    ParamsError = 302,

    ///<summary>Неверный тип параметров запроса</summary>
    [Display(Name = "Неверный тип параметров запроса")]
    RequestParamsTypeError = 303,

    ///<summary>Метод запроса не поддерживается</summary>
    [Display(Name = "Метод запроса не поддерживается")]
    RequestMethodNotSupport = 304,

    ///<summary>Ошибка параметров заголовка</summary>
    [Display(Name = "Ошибка параметров заголовка")]
    HeaderParamsError = 305,

    ///<summary>Путь запроса не открыт</summary>
    [Display(Name = "Путь запроса не открыт")]
    RequestPathNotOpen = 306,

    ///<summary>Доступ запрещён</summary>
    [Display(Name = "Доступ запрещён")]
    RequestForbidden = 403,

    ///<summary>Слишком много запросов</summary>
    [Display(Name = "Слишком много запросов")]
    TooManyRequests = 429,

    ///<summary>Ошибка реализации сервиса</summary>
    [Display(Name = "Ошибка реализации сервиса")]
    ServiceImplError = 500,

    ///<summary>Ошибка прокси сервиса</summary>
    [Display(Name = "Ошибка прокси сервиса")]
    ServiceProxyError = 501,

    ///<summary>Устройство не зарегистрировано</summary>
    [Display(Name = "Устройство не зарегистрировано")]
    DeviceNotRegister = 601,

    ///<summary>Устройство не в сети</summary>
    [Display(Name = "Устройство не в сети")]
    DeviceIsOffline = 602,

    ///<summary>Отказано в доступе к устройству</summary>
    [Display(Name = "Отказано в доступе к устройству")]
    DevicePermissionDenied = 603,

    ///<summary>Недопустимый идентификатор устройства</summary>
    [Display(Name = "Недопустимый идентификатор устройства")]
    IllegalDeviceId = 604,

    ///<summary>Несоответствие информации об устройстве</summary>
    [Display(Name = "Несоответствие информации об устройстве")]
    DeviceInfoInconsistent = 605,

    ///<summary>Запрос устройства не поддерживается</summary>
    [Display(Name = "Запрос устройства не поддерживается")]
    DeviceRequestNotSupport = 606,

    ///<summary>Шлюз уже привязан</summary>
    [Display(Name = "Шлюз уже привязан")]
    GatewayHasBeenBind = 607,

    ///<summary>Ошибка привязки подустройства</summary>
    [Display(Name = "Ошибка привязки подустройства")]
    SubDeviceBindError = 608,

    ///<summary>Ошибка отвязки шлюза</summary>
    [Display(Name = "Ошибка отвязки шлюза")]
    GatewayUnbindError = 609,

    ///<summary>Ошибка отвязки подустройства</summary>
    [Display(Name = "Ошибка отвязки подустройства")]
    SubdeviceUnbindError = 610,

    ///<summary>Подустройство не привязано</summary>
    [Display(Name = "Подустройство не привязано")]
    SubdeviceNotBind = 611,

    ///<summary>Шлюз не отвечает на запрос</summary>
    [Display(Name = "Шлюз не отвечает на запрос")]
    GatewayRequestNotResponse = 612,

    ///<summary>Родительское устройство не найдено</summary>
    [Display(Name = "Родительское устройство не найдено")]
    NotFindParentDevice = 615,

    ///<summary>Время BindKey истекло</summary>
    [Display(Name = "Время BindKey истекло")]
    BindKeyTimeOut = 636,

    ///<summary>Irid не существует</summary>
    [Display(Name = "Irid не существует")]
    IridNotExists = 637,

    ///<summary>Подустройство не поддерживает эту операцию</summary>
    [Display(Name = "Подустройство не поддерживает эту операцию")]
    SubDeviceNotSupportThisOperation = 638,

    ///<summary>Устройство не может подключить подустройство</summary>
    [Display(Name = "Устройство не может подключить подустройство")]
    DeviceCannotMountSubDevice = 639,

    ///<summary>Код устройства не найден</summary>
    [Display(Name = "Код устройства не найден")]
    DeviceFiveCodeNotFound = 640,

    ///<summary>Неправильный шаг при работе с Bluetooth-устройством</summary>
    [Display(Name = "Неправильный шаг при работе с Bluetooth-устройством")]
    BluetoothDeviceOperateWithWrongStep = 641,

    ///<summary>Ошибка валидации Bluetooth-устройства</summary>
    [Display(Name = "Ошибка валидации Bluetooth-устройства")]
    BluetoothDeviceValidateWrong = 642,

    ///<summary>Информация о Bluetooth отсутствует</summary>
    [Display(Name = "Информация о Bluetooth отсутствует")]
    BluetoothInfoNotExist = 643,

    ///<summary>Не удалось проверить код безопасности</summary>
    [Display(Name = "Не удалось проверить код безопасности")]
    FailedValidateSecurityCode = 644,

    ///<summary>Неправильный шаг при регистрации Bluetooth-устройства в приложении</summary>
    [Display(Name = "Неправильный шаг при регистрации Bluetooth-устройства в приложении")]
    AppBluetoothDeviceRegisterWrongStep = 645,

    ///<summary>Шлюз не существует</summary>
    [Display(Name = "Шлюз не существует")]
    GatewayNotExists = 651,

    ///<summary>Превышен лимит шлюзов</summary>
    [Display(Name = "Превышен лимит шлюзов")]
    GatewayLimit = 652,

    ///<summary>Сбой выполнения динамической последовательности</summary>
    [Display(Name = "Сбой выполнения динамической последовательности")]
    DynamicSequenceRunFailed = 655,

    ///<summary>Привязка устройства запрещена</summary>
    [Display(Name = "Привязка устройства запрещена")]
    DeviceNotAllowBind = 656,

    ///<summary>Конфигурация группы устройств уже существует</summary>
    [Display(Name = "Конфигурация группы устройств уже существует")]
    DeviceGroupConfigExist = 657,

    ///<summary>Это IR-устройство не поддерживает копирование</summary>
    [Display(Name = "Это IR-устройство не поддерживает копирование")]
    ThisIrDeviceNotSupportCopy = 658,

    ///<summary>Функция устройства не поддерживается</summary>
    [Display(Name = "Функция устройства не поддерживается")]
    DeviceFunctionNotSupport = 664,

    ///<summary>Позиция не существует</summary>
    [Display(Name = "Позиция не существует")]
    PositionNotExist = 701,

    ///<summary>Позиция не может быть удалена</summary>
    [Display(Name = "Позиция не может быть удалена")]
    PositionCannotDeleted = 702,

    ///<summary>Дублирование имени позиции</summary>
    [Display(Name = "Дублирование имени позиции")]
    PositionNameDuplication = 703,

    ///<summary>Дублирование при создании позиции по умолчанию</summary>
    [Display(Name = "Дублирование при создании позиции по умолчанию")]
    DefaultPositionCreateDuplication = 704,

    ///<summary>Дублирование имени устройства</summary>
    [Display(Name = "Дублирование имени устройства")]
    DeviceNameDuplication = 705,

    ///<summary>Отказано в доступе к устройству</summary>
    [Display(Name = "Отказано в доступе к устройству")]
    DevicePermissionDenied1 = 706,

    ///<summary>Отказано в доступе IFTTT</summary>
    [Display(Name = "Отказано в доступе IFTTT")]
    IftttPermissionDenied = 707,

    ///<summary>Отказано в доступе к сцене</summary>
    [Display(Name = "Отказано в доступе к сцене")]
    ScenePermissionDenied = 708,

    ///<summary>Отказано в доступе к сервису</summary>
    [Display(Name = "Отказано в доступе к сервису")]
    ServicePermissionDenied = 709,

    ///<summary>Отказано в доступе к позиции</summary>
    [Display(Name = "Отказано в доступе к позиции")]
    PositionPermissionDenied = 710,

    ///<summary>Ошибка родительской позиции</summary>
    [Display(Name = "Ошибка родительской позиции")]
    ParentPositionError = 712,

    ///<summary>Позиция не является реальной позицией</summary>
    [Display(Name = "Позиция не является реальной позицией")]
    PositionNotRealPosition = 713,

    ///<summary>Позиция не допускается к удалению</summary>
    [Display(Name = "Позиция не допускается к удалению")]
    PositionIsNotAllowedToBeDeleted = 714,

    ///<summary>Ошибка позиции</summary>
    [Display(Name = "Ошибка позиции")]
    PositionError = 715,

    ///<summary>Сцена не существует</summary>
    [Display(Name = "Сцена не существует")]
    SceneNotExist = 716,

    ///<summary>Устройство не принадлежит этому пользователю</summary>
    [Display(Name = "Устройство не принадлежит этому пользователю")]
    DeviceDoesNotBelongToThisUser = 717,

    ///<summary>Ошибка данных</summary>
    [Display(Name = "Ошибка данных")]
    DataError = 718,

    ///<summary>Устройство не привязано к пользователю</summary>
    [Display(Name = "Устройство не привязано к пользователю")]
    DeviceNoBindUser = 719,

    ///<summary>Выход за пределы уровня позиции</summary>
    [Display(Name = "Выход за пределы уровня позиции")]
    OutOfPositionLayer = 722,

    ///<summary>Размер устройства превышает допустимый</summary>
    [Display(Name = "Размер устройства превышает допустимый")]
    DeviceSizeBeyond = 726,

    ///<summary>Размер позиции превышает допустимый</summary>
    [Display(Name = "Размер позиции превышает допустимый")]
    PositionSizeBeyond = 727,

    ///<summary>Время начала или окончания не может быть пустым</summary>
    [Display(Name = "Время начала или окончания не может быть пустым")]
    StartOrEndTimeCannotBeEmpty = 728,

    ///<summary>Время начала не должно быть больше времени окончания</summary>
    [Display(Name = "Время начала не должно быть больше времени окончания")]
    TheStartTimeMustNotBeGreaterThanTheEndTime = 729,

    ///<summary>Время начала или окончания не является временной меткой</summary>
    [Display(Name = "Время начала или окончания не является временной меткой")]
    StartOrEndTimeNotATimestamp = 730,

    ///<summary>IFTTT не существует</summary>
    [Display(Name = "IFTTT не существует")]
    IftttNotExists = 731,

    ///<summary>BindKey не существует</summary>
    [Display(Name = "BindKey не существует")]
    BindKeyNotExists = 745,

    ///<summary>Шлюз не подключен к облаку</summary>
    [Display(Name = "Шлюз не подключен к облаку")]
    GatewayNotConnectCloud = 746,

    ///<summary>Устройство не поддерживается</summary>
    [Display(Name = "Устройство не поддерживается")]
    DeviceUnsupported = 747,

    ///<summary>Модель категории не существует</summary>
    [Display(Name = "Модель категории не существует")]
    CategoryModelNotExists = 748,

    ///<summary>Дублирование имени пользовательского действия</summary>
    [Display(Name = "Дублирование имени пользовательского действия")]
    CustomActionNameDuplicate = 749,

    ///<summary>Ключ Ircode не существует</summary>
    [Display(Name = "Ключ Ircode не существует")]
    IrcodeKeyNotExists = 750,

    ///<summary>BindKey уже использован</summary>
    [Display(Name = "BindKey уже использован")]
    BindKeyHasBeenUsed = 751,

    ///<summary>Лимит IR-устройств</summary>
    [Display(Name = "Лимит IR-устройств")]
    IrDeviceLimit = 753,

    ///<summary>Пользовательское действие не существует</summary>
    [Display(Name = "Пользовательское действие не существует")]
    CustomActionNotExist = 754,

    ///<summary>Отказано в доступе субъекту</summary>
    [Display(Name = "Отказано в доступе субъекту")]
    SubjectPermissionDenied = 755,

    ///<summary>Нет прав</summary>
    [Display(Name = "Нет прав")]
    NoPermissions = 756,

    ///<summary>Устройство не привязано к пользователю</summary>
    [Display(Name = "Устройство не привязано к пользователю")]
    DeviceNotBindUser = 757,

    ///<summary>Превышен лимит длины параметра</summary>
    [Display(Name = "Превышен лимит длины параметра")]
    ParamLengthLimit = 758,

    ///<summary>Действие не поддерживается</summary>
    [Display(Name = "Действие не поддерживается")]
    ActionNotSupport = 760,

    ///<summary>Триггер не поддерживается</summary>
    [Display(Name = "Триггер не поддерживается")]
    TriggerNotSupport = 763,

    ///<summary>Имя IFTTT уже существует</summary>
    [Display(Name = "Имя IFTTT уже существует")]
    IftttNameHasExists = 768,

    ///<summary>Имя сцены уже существует</summary>
    [Display(Name = "Имя сцены уже существует")]
    SceneNameHasExists = 769,

    ///<summary>Имя устройства уже существует</summary>
    [Display(Name = "Имя устройства уже существует")]
    DeviceNameHasExists = 770,

    ///<summary>Устройство нельзя отвязать</summary>
    [Display(Name = "Устройство нельзя отвязать")]
    TheDeviceCanNotUnbind = 778,

    ///<summary>Отказано в доступе к событию условия</summary>
    [Display(Name = "Отказано в доступе к событию условия")]
    ConditionEventPermissionDenied = 788,

    ///<summary>Дублирование имени события условия</summary>
    [Display(Name = "Дублирование имени события условия")]
    ConditionEventDuplicateName = 789,

    ///<summary>Учётная запись не зарегистрирована</summary>
    [Display(Name = "Учётная запись не зарегистрирована")]
    AccountNotRegister = 801,

    ///<summary>Пользователь не вошёл в систему</summary>
    [Display(Name = "Пользователь не вошёл в систему")]
    AccountNotLogin = 802,

    ///<summary>Отказано в доступе пользователю</summary>
    [Display(Name = "Отказано в доступе пользователю")]
    UserPermissionDenied = 803,

    ///<summary>Ошибка токена</summary>
    [Display(Name = "Ошибка токена")]
    TokenFailed = 804,

    ///<summary>Аккаунт уже зарегистрирован</summary>
    [Display(Name = "Аккаунт уже зарегистрирован")]
    AccountHasRegister = 805,

    ///<summary>Неверный формат аккаунта</summary>
    [Display(Name = "Неверный формат аккаунта")]
    AccountFormatError = 807,

    ///<summary>Неверный пароль</summary>
    [Display(Name = "Неверный пароль")]
    PasswordIncorrect = 810,

    ///<summary>Неверный код подтверждения</summary>
    [Display(Name = "Неверный код подтверждения")]
    AuthCodeIncorrect = 811,

    ///<summary>Тип аккаунта не поддерживается</summary>
    [Display(Name = "Тип аккаунта не поддерживается")]
    AccountTypeUnsupport = 812,

    ///<summary>Неверный код подтверждения</summary>
    [Display(Name = "Неверный код подтверждения")]
    AuthCodeIncorrect1 = 816,

    ///<summary>Код подтверждения отправляется слишком часто</summary>
    [Display(Name = "Код подтверждения отправляется слишком часто")]
    AuthCodeSendAllTooOften = 817,

    ///<summary>Код подтверждения недействителен</summary>
    [Display(Name = "Код подтверждения недействителен")]
    AuthCodeIsInvalid = 820,

    ///<summary>Ошибка обновления</summary>
    [Display(Name = "Ошибка обновления")]
    UpgradeError = 901,

    ///<summary>Прошивка не существует</summary>
    [Display(Name = "Прошивка не существует")]
    FirmwareNotExist = 902,

    ///<summary>Пакет не существует</summary>
    [Display(Name = "Пакет не существует")]
    PackageNotExist = 903,

    ///<summary>Прошивка уже обновлена</summary>
    [Display(Name = "Прошивка уже обновлена")]
    FirmwareAlreadyUpToDate = 904,

    ///<summary>Запрос прошивки пуст</summary>
    [Display(Name = "Запрос прошивки пуст")]
    FirmwareQueryIsEmpty = 905,

    ///<summary>Нет доступной для обновления прошивки</summary>
    [Display(Name = "Нет доступной для обновления прошивки")]
    NoUpdatableFirmware = 906,

    ///<summary>Не удалось обновить прошивку</summary>
    [Display(Name = "Не удалось обновить прошивку")]
    FirmwareUpgradeFailed = 907,

    ///<summary>Устройство обновляется</summary>
    [Display(Name = "Устройство обновляется")]
    DeviceIsBeingUpgrade = 908,

    ///<summary>Шлюз субустройства обновляется</summary>
    [Display(Name = "Шлюз субустройства обновляется")]
    TheSubDeviceGatewayIsBeingUpgraded = 909,

    ///<summary>Подустройства шлюза обновляются</summary>
    [Display(Name = "Подустройства шлюза обновляются")]
    TheGatewaySubDeviceAreBeingUpgraded = 910,

    ///<summary>Шлюз субустройства также обновлён</summary>
    [Display(Name = "Шлюз субустройства также обновлён")]
    TheSubDeviceGatewayIsAlsoUpgraded = 911,

    ///<summary>Модель не может быть обновлена с помощью прошивки</summary>
    [Display(Name = "Модель не может быть обновлена с помощью прошивки")]
    ModelCanNotBeUpgradedWithFirmware = 912,

    ///<summary>Недопустимый атрибут ресурса</summary>
    [Display(Name = "Недопустимый атрибут ресурса")]
    ResourceAttrIllegal = 1003,

    ///<summary>Недопустимое значение ресурса</summary>
    [Display(Name = "Недопустимое значение ресурса")]
    ResourceValueIllegal = 1004,

    ///<summary>Тип субъекта не поддерживается</summary>
    [Display(Name = "Тип субъекта не поддерживается")]
    SubjectTypeNotSupport = 1006,

    ///<summary>Запись ресурса не поддерживается</summary>
    [Display(Name = "Запись ресурса не поддерживается")]
    ResourceWriteNotSupport = 1007,

    ///<summary>Атрибут ресурса не существует</summary>
    [Display(Name = "Атрибут ресурса не существует")]
    ResourceAttrNotExist = 1008,

    ///<summary>Ошибка отчёта атрибута</summary>
    [Display(Name = "Ошибка отчёта атрибута")]
    ReportAttrError = 1009,

    ///<summary>Ошибка идентификатора ресурса при отчёте</summary>
    [Display(Name = "Ошибка идентификатора ресурса при отчёте")]
    ReportResourceIdError = 1010,

    ///<summary>Связь не существует</summary>
    [Display(Name = "Связь не существует")]
    LinkageNotExist = 1201,

    ///<summary>Сцена не существует</summary>
    [Display(Name = "Сцена не существует")]
    SceneNotExist1 = 1202,

    ///<summary>Условие выполнения IFTTT не выполнено</summary>
    [Display(Name = "Условие выполнения IFTTT не выполнено")]
    IftttExecuteConditionNotSatisfied = 1203,

    ///<summary>В связи отсутствует устройство</summary>
    [Display(Name = "В связи отсутствует устройство")]
    LinkageNoDevice = 1204,

    ///<summary>В сцене отсутствует устройство</summary>
    [Display(Name = "В сцене отсутствует устройство")]
    SceneNoDevice = 1205,

    ///<summary>Не удалось удалить локальную связь</summary>
    [Display(Name = "Не удалось удалить локальную связь")]
    DeleteLocalLinkageFailed = 1206,

    ///<summary>Операция не удалась</summary>
    [Display(Name = "Операция не удалась")]
    OperationFailed = 1207,

    ///<summary>Ошибка параметров IFTTT</summary>
    [Display(Name = "Ошибка параметров IFTTT")]
    IftttParameterError = 1208,

    ///<summary>Действие не определено</summary>
    [Display(Name = "Действие не определено")]
    ThisActionNotDefinition = 1210,

    ///<summary>Триггер не определён</summary>
    [Display(Name = "Триггер не определён")]
    ThisTriggerNotDefinition = 1211,

    ///<summary>Действие пусто</summary>
    [Display(Name = "Действие пусто")]
    ActionIsEmpty = 1212,

    ///<summary>Выполнение IFTTT не удалось</summary>
    [Display(Name = "Выполнение IFTTT не удалось")]
    IftttExecuteFailed = 1221,

    ///<summary>IFTTT с таким именем уже существует</summary>
    [Display(Name = "IFTTT с таким именем уже существует")]
    IftttSameName = 1223,

    ///<summary>Сцена с таким именем уже существует</summary>
    [Display(Name = "Сцена с таким именем уже существует")]
    SceneSameName = 1224,

    ///<summary>Условия конфигурации некорректны</summary>
    [Display(Name = "Условия конфигурации некорректны")]
    ConditionsOfConfigurationIsNotCorrect = 1226,

    ///<summary>Условия конфигурации некорректны</summary>
    [Display(Name = "Условия конфигурации некорректны")]
    ConditionsOfConfigurationIsNotCorrect1 = 1227,

    ///<summary>Условие повторяется</summary>
    [Display(Name = "Условие повторяется")]
    ConditionIsRepeated = 1228,

    ///<summary>Условия конфигурации некорректны</summary>
    [Display(Name = "Условия конфигурации некорректны")]
    ConditionsOfConfigurationIsNotCorrect2 = 1229,

    ///<summary>Действие повторяется</summary>
    [Display(Name = "Действие повторяется")]
    ActionIsRepeated = 1230,

    ///<summary>Действия в конфигурации некорректны</summary>
    [Display(Name = "Действия в конфигурации некорректны")]
    ActionsOfConfigurationIsNotCorrect = 1231,

    ///<summary>Условия и действия конфигурации некорректны</summary>
    [Display(Name = "Условия и действия конфигурации некорректны")]
    ConditionsAndActionsOfConfigurationIsNotCorrect = 1232,

    ///<summary>IFTTT работает ненормально</summary>
    [Display(Name = "IFTTT работает ненормально")]
    IftttIsAbnormal = 1238,

    ///<summary>Условия конфигурации некорректны</summary>
    [Display(Name = "Условия конфигурации некорректны")]
    ConditionsOfConfigurationIsNotCorrect3 = 1239,

    ///<summary>Данные находятся в процессе операции</summary>
    [Display(Name = "Данные находятся в процессе операции")]
    TheDataIsInOperation = 1300,

    ///<summary>Ошибка получения списка разработчиков</summary>
    [Display(Name = "Ошибка получения списка разработчиков")]
    GetDeveloperListError = 2001,

    ///<summary>Неверный appid или appkey</summary>
    [Display(Name = "Неверный appid или appkey")]
    AppidOrAppkeyIllegal = 2002,

    ///<summary>Неверный код подтверждения</summary>
    [Display(Name = "Неверный код подтверждения")]
    AuthCodeIncorrect2 = 2003,

    ///<summary>Неверный AccessToken</summary>
    [Display(Name = "Неверный AccessToken")]
    AccessTokenIncorrect = 2004,

    ///<summary>AccessToken истёк</summary>
    [Display(Name = "AccessToken истёк")]
    AccessTokenExpired = 2005,

    ///<summary>Неверный RefreshToken</summary>
    [Display(Name = "Неверный RefreshToken")]
    RefreshTokenIncorrect = 2006,

    ///<summary>RefreshToken истёк</summary>
    [Display(Name = "RefreshToken истёк")]
    RefreshTokenExpired = 2007,

    ///<summary>Доступ запрещён</summary>
    [Display(Name = "Доступ запрещён")]
    PermissionDenied = 2008,

    ///<summary>Недействительный OpenId</summary>
    [Display(Name = "Недействительный OpenId")]
    InvalidOpenId = 2009,

    ///<summary>Неавторизованный пользователь</summary>
    [Display(Name = "Неавторизованный пользователь")]
    UnauthorizedUser = 2010,

    ///<summary>Результат запроса пуст</summary>
    [Display(Name = "Результат запроса пуст")]
    TheQueryResultIsEmpty = 2011,

    ///<summary>Некорректный запрос</summary>
    [Display(Name = "Некорректный запрос")]
    InvalidApply = 2012,

    ///<summary>Отказано в доступе разработчику</summary>
    [Display(Name = "Отказано в доступе разработчику")]
    DeveloperPermissionDenied = 2013,

    ///<summary>Отказано в доступе к ресурсу</summary>
    [Display(Name = "Отказано в доступе к ресурсу")]
    ResourcePermissionDenied = 2014,

    ///<summary>Ошибка подписки</summary>
    [Display(Name = "Ошибка подписки")]
    SubscriberFaild = 2015,

    ///<summary>AccountId уже существует</summary>
    [Display(Name = "AccountId уже существует")]
    AccountIdHasExist = 2016,

    ///<summary>Appkey превышает лимит</summary>
    [Display(Name = "Appkey превышает лимит")]
    AppkeyExceedsTheLimit = 2017,

    ///<summary>Конфигурация IP превышает лимит</summary>
    [Display(Name = "Конфигурация IP превышает лимит")]
    IPConfigExceedTheLimit = 2018,

    ///<summary>Приложение не активировано</summary>
    [Display(Name = "Приложение не активировано")]
    ApplicationNotActivated = 2022,
}
