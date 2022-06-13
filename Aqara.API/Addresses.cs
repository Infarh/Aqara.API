using System.ComponentModel;

namespace Aqara.API;

public static class Addresses
{
    public static class Auth
    {
        ///<summary>Create a virtual account</summary>
        [Description("Create a virtual account")]
        public const string CreateVirtualAccount = "config.auth.createAccount";
        
        ///<summary>Get authorization verification code</summary>
        [Description("Get authorization verification code")]
        public const string GetAuthorizationVerificationCode = "config.auth.getAuthCode";
        
        ///<summary>Obtain access-token</summary>
        [Description("Obtain access-token")]
        public const string ObtainAccessToken = "config.auth.getToken";
        
        ///<summary>Refresh access token</summary>
        [Description("Refresh access token")]
        public const string RefreshAccessToken = "config.auth.refreshToken";
    }

    public static class Position
    {
        ///<summary>Create a position</summary>
        [Description("Create a position")]
        public const string CreatePosition = "config.position.create";
        
        ///<summary>Delete position</summary>
        [Description("Delete position")]
        public const string DeletePosition = "config.position.delete";
        
        ///<summary>Update position information</summary>
        [Description("Update position information")]
        public const string UpdatePositionInformation = "config.position.update";
        
        ///<summary>Update position timezone</summary>
        [Description("Update position timezone")]
        public const string UpdatePositionTimezone = "config.position.timeZone";
        
        ///<summary>Query the subordinate position information of the current parent position</summary>
        [Description("Query the subordinate position information of the current parent position")]
        public const string QueryTheSubordinatePositionInformationOfTheCurrentParentPosition = "query.position.info";
        
        ///<summary>Query position detail</summary>
        [Description("Query position detail")]
        public const string QueryPositionDetail = "query.position.detail";
        
        ///<summary>Query the list of gateways that support sub-devices based on location</summary>
        [Description("Query the list of gateways that support sub-devices based on location")]
        public const string QueryTheListOfGatewaysThatSupportSubDevicesBasedOnLocation = "query.position.supportGateway";
    }

    public static class Device
    {
        ///<summary>Obtain temporary credentials(bindKey) before the device is registered</summary>
        [Description("Obtain temporary credentials(bindKey) before the device is registered")]
        public const string ObtainTemporaryCredentialsBindKeyBeforeTheDeviceIsRegistered = "query.device.bindKey";
        
        ///<summary>Device registration status query</summary>
        [Description("Device registration status query")]
        public const string DeviceRegistrationStatusQuery = "query.device.bind";
        
        ///<summary>Query device information</summary>
        [Description("Query device information")]
        public const string QueryDeviceInformation = "query.device.info";
        
        ///<summary>Query sub-device information based on the Gateway</summary>
        [Description("Query sub-device information based on the Gateway")]
        public const string QuerySubDeviceInformationBasedOnTheGateway = "query.device.subInfo";
        
        ///<summary>Update device information</summary>
        [Description("Update device information")]
        public const string UpdateDeviceInformation = "config.device.name";
        
        ///<summary>Update device position</summary>
        [Description("Update device position")]
        public const string UpdateDevicePosition = "config.device.position";
        
        ///<summary>Enable Hub to add subdevice mode</summary>
        [Description("Enable Hub to add subdevice mode")]
        public const string EnableHubToAddSubdeviceMode = "write.device.openConnect";
        
        ///<summary>Disable Hub to add subdevice mode</summary>
        [Description("Disable Hub to add subdevice mode")]
        public const string DisableHubToAddSubdeviceMode = "write.device.closeConnect";
        
        ///<summary>Query the list of gateways that support sub-devices</summary>
        [Description("Query the list of gateways that support sub-devices")]
        public const string QueryTheListOfGatewaysThatSupportSubDevices = "query.device.supportGateway";
        
        ///<summary>Unbind device</summary>
        [Description("Unbind device")]
        public const string UnbindDevice = "write.device.unbind";
    }

    public static class Resource
    {
        ///<summary>Query the details of the attributes that have been opened</summary>
        [Description("Query the details of the attributes that have been opened")]
        public const string QueryTheDetailsOfTheAttributesThatHaveBeenOpened = "query.resource.info";
        
        ///<summary>Query device attribute name</summary>
        [Description("Query device attribute name")]
        public const string QueryDeviceAttributeName = "query.resource.name";
        
        ///<summary>Modify device attribute information</summary>
        [Description("Modify device attribute information")]
        public const string ModifyDeviceAttributeInformation = "config.resource.info";
        
        ///<summary>Query device attribute</summary>
        [Description("Query device attribute")]
        public const string QueryDeviceAttribute = "query.resource.value";
        
        ///<summary>Control device</summary>
        [Description("Control device")]
        public const string ControlDevice = "write.resource.device";
        
        ///<summary>Query the history of device attributes</summary>
        [Description("Query the history of device attributes")]
        public const string QueryTheHistoryOfDeviceAttributes = "fetch.resource.history";
        
        ///<summary>Query the statistical history value of the device attribute</summary>
        [Description("Query the statistical history value of the device attribute")]
        public const string QueryTheStatisticalHistoryValueOfTheDeviceAttribute = "fetch.resource.statistics";
        
        ///<summary>Subscribe device attribute</summary>
        [Description("Subscribe device attribute")]
        public const string SubscribeDeviceAttribute = "config.resource.subscribe";
        
        ///<summary>Unsubscribe device resource</summary>
        [Description("Unsubscribe device resource")]
        public const string UnsubscribeDeviceResource = "config.resource.unsubscribe";
    }

    public static class IFTTT
    {
        ///<summary>Query what triggers the specified object type has (IF)</summary>
        [Description("Query what triggers the specified object type has (IF)")]
        public const string QueryWhatTriggersTheSpecifiedObjectTypeHasIf = "query.ifttt.trigger";
        
        ///<summary>Query what actions the specified object type has (Then)</summary>
        [Description("Query what actions the specified object type has (Then)")]
        public const string QueryWhatActionsTheSpecifiedObjectTypeHasThen = "query.ifttt.action";
    }

    public static class Linkage
    {
        ///<summary>Create linkage</summary>
        [Description("Create linkage")]
        public const string CreateLinkage = "config.linkage.create";
        
        ///<summary>Query detail information of the linkage</summary>
        [Description("Query detail information of the linkage")]
        public const string QueryDetailInformationOfTheLinkage = "query.linkage.detail";
        
        ///<summary>Update linkage</summary>
        [Description("Update linkage")]
        public const string UpdateLinkage = "config.linkage.update";
        
        ///<summary>Delete linkage</summary>
        [Description("Delete linkage")]
        public const string DeleteLinkage = "config.linkage.delete";
        
        ///<summary>Enable/disable linkage</summary>
        [Description("Enable/disable linkage")]
        public const string EnableDisableLinkage = "config.linkage.enable";
        
        ///<summary>Query linkage list based on location</summary>
        [Description("Query linkage list based on location")]
        public const string QueryLinkageListBasedOnLocation = "query.linkage.listByPositionId";
        
        ///<summary>Query linkage list based on object ID</summary>
        [Description("Query linkage list based on object ID")]
        public const string QueryLinkageListBasedOnObjectID = "query.linkage.listBySubjectId";
    }

    public static class Scene
    {
        ///<summary>Create scene</summary>
        [Description("Create scene")]
        public const string CreateScene = "config.scene.create";
        
        ///<summary>Update scene</summary>
        [Description("Update scene")]
        public const string UpdateScene = "config.scene.update";
        
        ///<summary>Delete scene</summary>
        [Description("Delete scene")]
        public const string DeleteScene = "config.scene.delete";
        
        ///<summary>Execute scene</summary>
        [Description("Execute scene")]
        public const string ExecuteScene = "config.scene.run";
        
        ///<summary>Query detail information of the scene</summary>
        [Description("Query detail information of the scene")]
        public const string QueryDetailInformationOfTheScene = "query.scene.detail";
        
        ///<summary>Query scene list based on object ID</summary>
        [Description("Query scene list based on object ID")]
        public const string QuerySceneListBasedOnObjectID = "query.scene.listBySubjectId";
        
        ///<summary>Query scene list based on location</summary>
        [Description("Query scene list based on location")]
        public const string QuerySceneListBasedOnLocation = "query.scene.listByPositionId";
    }

    public static class Event
    {
        ///<summary>Create multiple-conditions</summary>
        [Description("Create multiple-conditions")]
        public const string CreateMultipleConditions = "config.event.create";
        
        ///<summary>Update multiple-conditions</summary>
        [Description("Update multiple-conditions")]
        public const string UpdateMultipleConditions = "config.event.update";
        
        ///<summary>Delete multiple-conditions</summary>
        [Description("Delete multiple-conditions")]
        public const string DeleteMultipleConditions = "config.event.delete";
        
        ///<summary>Query detail information of multiple-conditions</summary>
        [Description("Query detail information of multiple-conditions")]
        public const string QueryDetailInformationOfMultipleConditions = "query.event.detail";
        
        ///<summary>Query multiple-conditions based on subject Id</summary>
        [Description("Query multiple-conditions based on subject Id")]
        public const string QueryMultipleConditionsBasedOnSubjectId = "query.event.listBySubjectId";
        
        ///<summary>Query multiple-conditions based on location</summary>
        [Description("Query multiple-conditions based on location")]
        public const string QueryMultipleConditionsBasedOnLocation = "query.event.listByPositionId";
    }

    public static class Ota
    {
        ///<summary>Query device firmware based on device model</summary>
        [Description("Query device firmware based on device model")]
        public const string QueryDeviceFirmwareBasedOnDeviceModel = "query.ota.firmware";
        
        ///<summary>Upgrade firmware (Batch upgrade)</summary>
        [Description("Upgrade firmware (Batch upgrade)")]
        public const string UpgradeFirmwareBatchUpgrade = "write.ota.upgrade";
        
        ///<summary>Query the upgrade status</summary>
        [Description("Query the upgrade status")]
        public const string QueryTheUpgradeStatus = "query.ota.upgrade";
    }

    public static class IR
    {
        ///<summary>Match tree information</summary>
        [Description("Match tree information")]
        public const string MatchTreeInformation = "query.ir.match";
        
        ///<summary>Obtain device type list</summary>
        [Description("Obtain device type list")]
        public const string ObtainDeviceTypeList = "query.ir.categories";
        
        ///<summary>Query brand list based on device type</summary>
        [Description("Query brand list based on device type")]
        public const string QueryBrandListBasedOnDeviceType = "query.ir.brands";
        
        ///<summary>Get remote control information</summary>
        [Description("Get remote control information")]
        public const string GetRemoteControlInformation = "query.ir.info";
        
        ///<summary>Query the remote control list under the gateway</summary>
        [Description("Query the remote control list under the gateway")]
        public const string QueryTheRemoteControlListUnderTheGateway = "query.ir.list";
        
        ///<summary>Query the state of the stateful air conditioner</summary>
        [Description("Query the state of the stateful air conditioner")]
        public const string QueryTheStateOfTheStatefulAirConditioner = "query.ir.acState";
        
        ///<summary>Query remote control function</summary>
        [Description("Query remote control function")]
        public const string QueryRemoteControlFunction = "query.ir.functions";
        
        ///<summary>Query remote control buttons</summary>
        [Description("Query remote control buttons")]
        public const string QueryRemoteControlButtons = "query.ir.keys";
        
        ///<summary>Add remote control</summary>
        [Description("Add remote control")]
        public const string AddRemoteControl = "config.ir.create";
        
        ///<summary>Delete remote control</summary>
        [Description("Delete remote control")]
        public const string DeleteRemoteControl = "config.ir.delete";
        
        ///<summary>Update remote control</summary>
        [Description("Update remote control")]
        public const string UpdateRemoteControl = "config.ir.update";
        
        ///<summary>Add a custom remote</summary>
        [Description("Add a custom remote")]
        public const string AddACustomRemote = "config.ir.custom";
        
        ///<summary>Click the remote control button</summary>
        [Description("Click the remote control button")]
        public const string ClickTheRemoteControlButton = "write.ir.click";
        
        ///<summary>Turn on infrared learning</summary>
        [Description("Turn on infrared learning")]
        public const string TurnOnInfraredLearning = "write.ir.startLearn";
        
        ///<summary>Cancel infrared learning</summary>
        [Description("Cancel infrared learning")]
        public const string CancelInfraredLearning = "write.ir.cancelLearn";
        
        ///<summary>Query infrared learning results</summary>
        [Description("Query infrared learning results")]
        public const string QueryInfraredLearningResults = "query.ir.learnResult";
    }

    public static class Push
    {
        ///<summary>Query the details of the failed message</summary>
        [Description("Query the details of the failed message")]
        public const string QueryTheDetailsOfTheFailedMessage = "query.push.errorMsg";
    }


}
