using System.ComponentModel;

namespace Aqara.API;

/// <summary>Содержит константы адресов (ключей) методов API Aqara</summary>
public static class Addresses
{
    /// <summary>Операции, связанные с авторизацией</summary>
    public static class Auth
    {
        ///<summary>Создать виртуальный аккаунт</summary>
        [Description("Создать виртуальный аккаунт")]
        public const string CreateVirtualAccount = "config.auth.createAccount";

        ///<summary>Получить код подтверждения авторизации</summary>
        [Description("Получить код подтверждения авторизации")]
        public const string GetAuthorizationVerificationCode = "config.auth.getAuthCode";

        ///<summary>Получить токен доступа</summary>
        [Description("Получить токен доступа")]
        public const string ObtainAccessToken = "config.auth.getToken";

        ///<summary>Обновить токен доступа</summary>
        [Description("Обновить токен доступа")]
        public const string RefreshAccessToken = "config.auth.refreshToken";
    }

    /// <summary>Операции, связанные с позициями и местоположениями</summary>
    public static class Position
    {
        ///<summary>Создать позицию</summary>
        [Description("Создать позицию")]
        public const string CreatePosition = "config.position.create";

        ///<summary>Удалить позицию</summary>
        [Description("Удалить позицию")]
        public const string DeletePosition = "config.position.delete";

        ///<summary>Обновить информацию позиции</summary>
        [Description("Обновить информацию позиции")]
        public const string UpdatePositionInformation = "config.position.update";

        ///<summary>Обновить часовой пояс позиции</summary>
        [Description("Обновить часовой пояс позиции")]
        public const string UpdatePositionTimezone = "config.position.timeZone";

        ///<summary>Запросить информацию о подчинённых позициях текущей родительской позиции</summary>
        [Description("Запросить информацию о подчинённых позициях текущей родительской позиции")]
        public const string QueryTheSubordinatePositionInformationOfTheCurrentParentPosition = "query.position.info";

        ///<summary>Запросить подробности позиции</summary>
        [Description("Запросить подробности позиции")]
        public const string QueryPositionDetail = "query.position.detail";

        ///<summary>Запросить список шлюзов, поддерживающих подустройства, по местоположению</summary>
        [Description("Запросить список шлюзов, поддерживающих подустройства, по местоположению")]
        public const string QueryTheListOfGatewaysThatSupportSubDevicesBasedOnLocation = "query.position.supportGateway";
    }

    /// <summary>Операции, связанные с устройствами</summary>
    public static class Device
    {
        ///<summary>Получить временные учётные данные (bindKey) перед регистрацией устройства</summary>
        [Description("Получить временные учётные данные (bindKey) перед регистрацией устройства")]
        public const string ObtainTemporaryCredentialsBindKeyBeforeTheDeviceIsRegistered = "query.device.bindKey";

        ///<summary>Запрос статуса регистрации устройства</summary>
        [Description("Запрос статуса регистрации устройства")]
        public const string DeviceRegistrationStatusQuery = "query.device.bind";

        ///<summary>Запросить информацию об устройстве</summary>
        [Description("Запросить информацию об устройстве")]
        public const string QueryDeviceInformation = "query.device.info";

        ///<summary>Запросить информацию о подустройствах по шлюзу</summary>
        [Description("Запросить информацию о подустройствах по шлюзу")]
        public const string QuerySubDeviceInformationBasedOnTheGateway = "query.device.subInfo";

        ///<summary>Обновить информацию об устройстве</summary>
        [Description("Обновить информацию об устройстве")]
        public const string UpdateDeviceInformation = "config.device.name";

        ///<summary>Обновить местоположение устройства</summary>
        [Description("Обновить местоположение устройства")]
        public const string UpdateDevicePosition = "config.device.position";

        ///<summary>Включить режим добавления подустройств на хабе</summary>
        [Description("Включить режим добавления подустройств на хабе")]
        public const string EnableHubToAddSubdeviceMode = "write.device.openConnect";

        ///<summary>Отключить режим добавления подустройств на хабе</summary>
        [Description("Отключить режим добавления подустройств на хабе")]
        public const string DisableHubToAddSubdeviceMode = "write.device.closeConnect";

        ///<summary>Запросить список шлюзов, поддерживающих подустройства</summary>
        [Description("Запросить список шлюзов, поддерживающих подустройства")]
        public const string QueryTheListOfGatewaysThatSupportSubDevices = "query.device.supportGateway";

        ///<summary>Отвязать устройство</summary>
        [Description("Отвязать устройство")]
        public const string UnbindDevice = "write.device.unbind";
    }

    /// <summary>Операции, связанные с ресурсами и атрибутами устройств</summary>
    public static class Resource
    {
        ///<summary>Запросить детали открытых атрибутов</summary>
        [Description("Запросить детали открытых атрибутов")]
        public const string QueryTheDetailsOfTheAttributesThatHaveBeenOpened = "query.resource.info";

        ///<summary>Запросить имя атрибута устройства</summary>
        [Description("Запросить имя атрибута устройства")]
        public const string QueryDeviceAttributeName = "query.resource.name";

        ///<summary>Изменить информацию об атрибуте устройства</summary>
        [Description("Изменить информацию об атрибуте устройства")]
        public const string ModifyDeviceAttributeInformation = "config.resource.info";

        ///<summary>Запросить значение атрибута устройства</summary>
        [Description("Запросить значение атрибута устройства")]
        public const string QueryDeviceAttribute = "query.resource.value";

        ///<summary>Управлять устройством</summary>
        [Description("Управлять устройством")]
        public const string ControlDevice = "write.resource.device";

        ///<summary>Запросить историю атрибутов устройства</summary>
        [Description("Запросить историю атрибутов устройства")]
        public const string QueryTheHistoryOfDeviceAttributes = "fetch.resource.history";

        ///<summary>Запросить статистические исторические значения атрибута устройства</summary>
        [Description("Запросить статистические исторические значения атрибута устройства")]
        public const string QueryTheStatisticalHistoryValueOfTheDeviceAttribute = "fetch.resource.statistics";

        ///<summary>Подписаться на атрибут устройства</summary>
        [Description("Подписаться на атрибут устройства")]
        public const string SubscribeDeviceAttribute = "config.resource.subscribe";

        ///<summary>Отписаться от ресурса устройства</summary>
        [Description("Отписаться от ресурса устройства")]
        public const string UnsubscribeDeviceResource = "config.resource.unsubscribe";
    }

    /// <summary>Операции IFTTT</summary>
    public static class IFTTT
    {
        ///<summary>Запросить, какие триггеры есть у указанного типа объекта (IF)</summary>
        [Description("Запросить, какие триггеры есть у указанного типа объекта (IF)")]
        public const string QueryWhatTriggersTheSpecifiedObjectTypeHasIf = "query.ifttt.trigger";

        ///<summary>Запросить, какие действия есть у указанного типа объекта (Then)</summary>
        [Description("Запросить, какие действия есть у указанного типа объекта (Then)")]
        public const string QueryWhatActionsTheSpecifiedObjectTypeHasThen = "query.ifttt.action";
    }

    /// <summary>Операции, связанные со связками и автоматизациями</summary>
    public static class Linkage
    {
        ///<summary>Создать связку</summary>
        [Description("Создать связку")]
        public const string CreateLinkage = "config.linkage.create";

        ///<summary>Запросить подробную информацию о связке</summary>
        [Description("Запросить подробную информацию о связке")]
        public const string QueryDetailInformationOfTheLinkage = "query.linkage.detail";

        ///<summary>Обновить связку</summary>
        [Description("Обновить связку")]
        public const string UpdateLinkage = "config.linkage.update";

        ///<summary>Удалить связку</summary>
        [Description("Удалить связку")]
        public const string DeleteLinkage = "config.linkage.delete";

        ///<summary>Включить/отключить связку</summary>
        [Description("Включить/отключить связку")]
        public const string EnableDisableLinkage = "config.linkage.enable";

        ///<summary>Запросить список связок по местоположению</summary>
        [Description("Запросить список связок по местоположению")]
        public const string QueryLinkageListBasedOnLocation = "query.linkage.listByPositionId";

        ///<summary>Запросить список связок по идентификатору объекта</summary>
        [Description("Запросить список связок по идентификатору объекта")]
        public const string QueryLinkageListBasedOnObjectID = "query.linkage.listBySubjectId";
    }

    /// <summary>Операции, связанные со сценами</summary>
    public static class Scene
    {
        ///<summary>Создать сцену</summary>
        [Description("Создать сцену")]
        public const string CreateScene = "config.scene.create";

        ///<summary>Обновить сцену</summary>
        [Description("Обновить сцену")]
        public const string UpdateScene = "config.scene.update";

        ///<summary>Удалить сцену</summary>
        [Description("Удалить сцену")]
        public const string DeleteScene = "config.scene.delete";

        ///<summary>Выполнить сцену</summary>
        [Description("Выполнить сцену")]
        public const string ExecuteScene = "config.scene.run";

        ///<summary>Запросить подробную информацию о сцене</summary>
        [Description("Запросить подробную информацию о сцене")]
        public const string QueryDetailInformationOfTheScene = "query.scene.detail";

        ///<summary>Запросить список сцен по идентификатору объекта</summary>
        [Description("Запросить список сцен по идентификатору объекта")]
        public const string QuerySceneListBasedOnObjectID = "query.scene.listBySubjectId";

        ///<summary>Запросить список сцен по местоположению</summary>
        [Description("Запросить список сцен по местоположению")]
        public const string QuerySceneListBasedOnLocation = "query.scene.listByPositionId";
    }

    /// <summary>Операции, связанные с условиями и событиями</summary>
    public static class Event
    {
        ///<summary>Создать множественные условия</summary>
        [Description("Создать множественные условия")]
        public const string CreateMultipleConditions = "config.event.create";

        ///<summary>Обновить множественные условия</summary>
        [Description("Обновить множественные условия")]
        public const string UpdateMultipleConditions = "config.event.update";

        ///<summary>Удалить множественные условия</summary>
        [Description("Удалить множественные условия")]
        public const string DeleteMultipleConditions = "config.event.delete";

        ///<summary>Запросить подробную информацию о множественных условиях</summary>
        [Description("Запросить подробную информацию о множественных условиях")]
        public const string QueryDetailInformationOfMultipleConditions = "query.event.detail";

        ///<summary>Запросить множественные условия по идентификатору объекта</summary>
        [Description("Запросить множественные условия по идентификатору объекта")]
        public const string QueryMultipleConditionsBasedOnSubjectId = "query.event.listBySubjectId";

        ///<summary>Запросить множественные условия по местоположению</summary>
        [Description("Запросить множественные условия по местоположению")]
        public const string QueryMultipleConditionsBasedOnLocation = "query.event.listByPositionId";
    }

    /// <summary>Операции, связанные с OTA и прошивками</summary>
    public static class Ota
    {
        ///<summary>Запросить прошивку устройства по модели</summary>
        [Description("Запросить прошивку устройства по модели")]
        public const string QueryDeviceFirmwareBasedOnDeviceModel = "query.ota.firmware";

        ///<summary>Обновить прошивку (пакетное обновление)</summary>
        [Description("Обновить прошивку (пакетное обновление)")]
        public const string UpgradeFirmwareBatchUpgrade = "write.ota.upgrade";

        ///<summary>Запросить статус обновления</summary>
        [Description("Запросить статус обновления")]
        public const string QueryTheUpgradeStatus = "query.ota.upgrade";
    }

    /// <summary>Операции, связанные с инфракрасными устройствами и пультами</summary>
    public static class IR
    {
        ///<summary>Получить информацию соответствия дерева</summary>
        [Description("Получить информацию соответствия дерева")]
        public const string MatchTreeInformation = "query.ir.match";

        ///<summary>Получить список типов устройств</summary>
        [Description("Получить список типов устройств")]
        public const string ObtainDeviceTypeList = "query.ir.categories";

        ///<summary>Запросить список брендов по типу устройства</summary>
        [Description("Запросить список брендов по типу устройства")]
        public const string QueryBrandListBasedOnDeviceType = "query.ir.brands";

        ///<summary>Получить информацию о пульте</summary>
        [Description("Получить информацию о пульте")]
        public const string GetRemoteControlInformation = "query.ir.info";

        ///<summary>Запросить список пультов под шлюзом</summary>
        [Description("Запросить список пультов под шлюзом")]
        public const string QueryTheRemoteControlListUnderTheGateway = "query.ir.list";

        ///<summary>Запросить состояние кондиционера</summary>
        [Description("Запросить состояние кондиционера")]
        public const string QueryTheStateOfTheStatefulAirConditioner = "query.ir.acState";

        ///<summary>Запросить функции пульта</summary>
        [Description("Запросить функции пульта")]
        public const string QueryRemoteControlFunction = "query.ir.functions";

        ///<summary>Запросить кнопки пульта</summary>
        [Description("Запросить кнопки пульта")]
        public const string QueryRemoteControlButtons = "query.ir.keys";

        ///<summary>Добавить пульт</summary>
        [Description("Добавить пульт")]
        public const string AddRemoteControl = "config.ir.create";

        ///<summary>Удалить пульт</summary>
        [Description("Удалить пульт")]
        public const string DeleteRemoteControl = "config.ir.delete";

        ///<summary>Обновить пульт</summary>
        [Description("Обновить пульт")]
        public const string UpdateRemoteControl = "config.ir.update";

        ///<summary>Добавить кастомный пульт</summary>
        [Description("Добавить кастомный пульт")]
        public const string AddACustomRemote = "config.ir.custom";

        ///<summary>Нажать кнопку пульта</summary>
        [Description("Нажать кнопку пульта")]
        public const string ClickTheRemoteControlButton = "write.ir.click";

        ///<summary>Включить инфракрасное обучение</summary>
        [Description("Включить инфракрасное обучение")]
        public const string TurnOnInfraredLearning = "write.ir.startLearn";

        ///<summary>Отменить инфракрасное обучение</summary>
        [Description("Отменить инфракрасное обучение")]
        public const string CancelInfraredLearning = "write.ir.cancelLearn";

        ///<summary>Запросить результаты инфракрасного обучения</summary>
        [Description("Запросить результаты инфракрасного обучения")]
        public const string QueryInfraredLearningResults = "query.ir.learnResult";
    }

    /// <summary>Операции по работе с push-сообщениями</summary>
    public static class Push
    {
        ///<summary>Запросить детали неудачного сообщения</summary>
        [Description("Запросить детали неудачного сообщения")]
        public const string QueryTheDetailsOfTheFailedMessage = "query.push.errorMsg";
    }
}
