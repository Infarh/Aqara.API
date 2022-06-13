using System.ComponentModel;

namespace Aqara.API;

public enum ErrorCode : short
{
    Unknown = -1,

    ///<summary>Success</summary>
    Success = 0,
    
    ///<summary>Timeout</summary>
    Timeout = 100,
    
    ///<summary>Invalid data package</summary>
    [Description("Invalid data package")]
    InvalidDataPackage = 101,
    
    ///<summary>Data package has altered</summary>
    [Description("Data package has altered")]
    DataPackageHasAltered = 102,
    
    ///<summary>Data package may lose</summary>
    [Description("Data package may lose")]
    DataPackageMayLose = 103,
    
    ///<summary>Server busy</summary>
    [Description("Server busy")]
    ServerBusy = 104,
    
    ///<summary>Data package has expired</summary>
    [Description("Data package has expired")]
    DataPackageHasExpired = 105,
    
    ///<summary>Invalid sign</summary>
    [Description("Invalid sign")]
    InvalidSign = 106,
    
    ///<summary>Illegal appKey</summary>
    [Description("Illegal appKey")]
    IllegalAppKey = 107,
    
    ///<summary>Token has expired</summary>
    [Description("Token has expired")]
    TokenHasExpired = 108,
    
    ///<summary>Token is absence</summary>
    [Description("Token is absence")]
    TokenIsAbsence = 109,
    
    ///<summary>Params error</summary>
    [Description("Params error")]
    ParamsError = 302,
    
    ///<summary>Request params type error</summary>
    [Description("Request params type error")]
    RequestParamsTypeError = 303,
    
    ///<summary>Request method not support</summary>
    [Description("Request method not support")]
    RequestMethodNotSupport = 304,
    
    ///<summary>Header Params error</summary>
    [Description("Header Params error")]
    HeaderParamsError = 305,
    
    ///<summary>Request path not open</summary>
    [Description("Request path not open")]
    RequestPathNotOpen = 306,
    
    ///<summary>Request forbidden</summary>
    [Description("Request forbidden")]
    RequestForbidden = 403,
    
    ///<summary>Too Many Requests</summary>
    [Description("Too Many Requests")]
    TooManyRequests = 429,
    
    ///<summary>Service impl error</summary>
    [Description("Service impl error")]
    ServiceImplError = 500,
    
    ///<summary>Service proxy error</summary>
    [Description("Service proxy error")]
    ServiceProxyError = 501,
    
    ///<summary>Device not register</summary>
    [Description("Device not register")]
    DeviceNotRegister = 601,
    
    ///<summary>Device is offline</summary>
    [Description("Device is offline")]
    DeviceIsOffline = 602,
    
    ///<summary>Device permission denied</summary>
    [Description("Device permission denied")]
    DevicePermissionDenied = 603,
    
    ///<summary>Illegal device id</summary>
    [Description("Illegal device id")]
    IllegalDeviceId = 604,
    
    ///<summary>Device info inconsistent</summary>
    [Description("Device info inconsistent")]
    DeviceInfoInconsistent = 605,
    
    ///<summary>Device request not support</summary>
    [Description("Device request not support")]
    DeviceRequestNotSupport = 606,
    
    ///<summary>Gateway has been bind</summary>
    [Description("Gateway has been bind")]
    GatewayHasBeenBind = 607,
    
    ///<summary>Sub device bind error</summary>
    [Description("Sub device bind error")]
    SubDeviceBindError = 608,
    
    ///<summary>Gateway unbind error</summary>
    [Description("Gateway unbind error")]
    GatewayUnbindError = 609,
    
    ///<summary>Subdevice unbind error</summary>
    [Description("Subdevice unbind error")]
    SubdeviceUnbindError = 610,
    
    ///<summary>Subdevice not bind</summary>
    [Description("Subdevice not bind")]
    SubdeviceNotBind = 611,
    
    ///<summary>Gateway request not response</summary>
    [Description("Gateway request not response")]
    GatewayRequestNotResponse = 612,
    
    ///<summary>Not find parent device</summary>
    [Description("Not find parent device")]
    NotFindParentDevice = 615,
    
    ///<summary>BindKey time out</summary>
    [Description("BindKey time out")]
    BindKeyTimeOut = 636,
    
    ///<summary>Irid not exists</summary>
    [Description("Irid not exists")]
    IridNotExists = 637,
    
    ///<summary>Sub device not support this operation</summary>
    [Description("Sub device not support this operation")]
    SubDeviceNotSupportThisOperation = 638,
    
    ///<summary>Device cannot mount sub device</summary>
    [Description("Device cannot mount sub device")]
    DeviceCannotMountSubDevice = 639,
    
    ///<summary>Device five code not found</summary>
    [Description("Device five code not found")]
    DeviceFiveCodeNotFound = 640,
    
    ///<summary>Bluetooth device operate with wrong step</summary>
    [Description("Bluetooth device operate with wrong step")]
    BluetoothDeviceOperateWithWrongStep = 641,
    
    ///<summary>Bluetooth device validate wrong</summary>
    [Description("Bluetooth device validate wrong")]
    BluetoothDeviceValidateWrong = 642,
    
    ///<summary>Bluetooth info not exist</summary>
    [Description("Bluetooth info not exist")]
    BluetoothInfoNotExist = 643,
    
    ///<summary>Failed validate security code</summary>
    [Description("Failed validate security code")]
    FailedValidateSecurityCode = 644,
    
    ///<summary>App bluetooth device register wrong step</summary>
    [Description("App bluetooth device register wrong step")]
    AppBluetoothDeviceRegisterWrongStep = 645,
    
    ///<summary>Gateway not exists</summary>
    [Description("Gateway not exists")]
    GatewayNotExists = 651,
    
    ///<summary>Gateway limit</summary>
    [Description("Gateway limit")]
    GatewayLimit = 652,
    
    ///<summary>dynamic sequence run failed</summary>
    [Description("dynamic sequence run failed")]
    DynamicSequenceRunFailed = 655,
    
    ///<summary>Device not allow bind</summary>
    [Description("Device not allow bind")]
    DeviceNotAllowBind = 656,
    
    ///<summary>Device group config exist</summary>
    [Description("Device group config exist")]
    DeviceGroupConfigExist = 657,
    
    ///<summary>This ir device not support copy</summary>
    [Description("This ir device not support copy")]
    ThisIrDeviceNotSupportCopy = 658,
    
    ///<summary>Device function not support</summary>
    [Description("Device function not support")]
    DeviceFunctionNotSupport = 664,
    
    ///<summary>Position not exist</summary>
    [Description("Position not exist")]
    PositionNotExist = 701,
    
    ///<summary>Position cannot deleted</summary>
    [Description("Position cannot deleted")]
    PositionCannotDeleted = 702,
    
    ///<summary>Position name duplication</summary>
    [Description("Position name duplication")]
    PositionNameDuplication = 703,
    
    ///<summary>Default position create duplication</summary>
    [Description("Default position create duplication")]
    DefaultPositionCreateDuplication = 704,
    
    ///<summary>Device name duplication</summary>
    [Description("Device name duplication")]
    DeviceNameDuplication = 705,
    
    ///<summary>Device permission denied</summary>
    [Description("Device permission denied")]
    DevicePermissionDenied1 = 706,
    
    ///<summary>Ifttt permission denied</summary>
    [Description("Ifttt permission denied")]
    IftttPermissionDenied = 707,
    
    ///<summary>Scene permission denied</summary>
    [Description("Scene permission denied")]
    ScenePermissionDenied = 708,
    
    ///<summary>Service permission denied</summary>
    [Description("Service permission denied")]
    ServicePermissionDenied = 709,
    
    ///<summary>Position permission denied</summary>
    [Description("Position permission denied")]
    PositionPermissionDenied = 710,
    
    ///<summary>Parent position error</summary>
    [Description("Parent position error")]
    ParentPositionError = 712,
    
    ///<summary>Position not real position</summary>
    [Description("Position not real position")]
    PositionNotRealPosition = 713,
    
    ///<summary>Position is not allowed to be deleted</summary>
    [Description("Position is not allowed to be deleted")]
    PositionIsNotAllowedToBeDeleted = 714,
    
    ///<summary>Position error</summary>
    [Description("Position error")]
    PositionError = 715,
    
    ///<summary>Scene not exist</summary>
    [Description("Scene not exist")]
    SceneNotExist = 716,
    
    ///<summary>Device does not belong to this user</summary>
    [Description("Device does not belong to this user")]
    DeviceDoesNotBelongToThisUser = 717,
    
    ///<summary>Data error</summary>
    [Description("Data error")]
    DataError = 718,
    
    ///<summary>Device no bind user</summary>
    [Description("Device no bind user")]
    DeviceNoBindUser = 719,
    
    ///<summary>Out of position layer</summary>
    [Description("Out of position layer")]
    OutOfPositionLayer = 722,
    
    ///<summary>Device size beyond</summary>
    [Description("Device size beyond")]
    DeviceSizeBeyond = 726,
    
    ///<summary>Position size beyond</summary>
    [Description("Position size beyond")]
    PositionSizeBeyond = 727,
    
    ///<summary>Start or end time cannot be empty</summary>
    [Description("Start or end time cannot be empty")]
    StartOrEndTimeCannotBeEmpty = 728,
    
    ///<summary>The start time must not be greater than the end time</summary>
    [Description("The start time must not be greater than the end time")]
    TheStartTimeMustNotBeGreaterThanTheEndTime = 729,
    
    ///<summary>Start or end time not a timestamp</summary>
    [Description("Start or end time not a timestamp")]
    StartOrEndTimeNotATimestamp = 730,
    
    ///<summary>Ifttt not exists</summary>
    [Description("Ifttt not exists")]
    IftttNotExists = 731,
    
    ///<summary>BindKey not exists</summary>
    [Description("BindKey not exists")]
    BindKeyNotExists = 745,
    
    ///<summary>Gateway not connect cloud</summary>
    [Description("Gateway not connect cloud")]
    GatewayNotConnectCloud = 746,
    
    ///<summary>Device unsupported</summary>
    [Description("Device unsupported")]
    DeviceUnsupported = 747,
    
    ///<summary>Category model not exists</summary>
    [Description("Category model not exists")]
    CategoryModelNotExists = 748,
    
    ///<summary>Custom Action name duplicate</summary>
    [Description("Custom Action name duplicate")]
    CustomActionNameDuplicate = 749,
    
    ///<summary>Ircode key not exists</summary>
    [Description("Ircode key not exists")]
    IrcodeKeyNotExists = 750,
    
    ///<summary>BindKey has been used</summary>
    [Description("BindKey has been used")]
    BindKeyHasBeenUsed = 751,
    
    ///<summary>ir device limit</summary>
    [Description("ir device limit")]
    IrDeviceLimit = 753,
    
    ///<summary>Custom Action not exist</summary>
    [Description("Custom Action not exist")]
    CustomActionNotExist = 754,
    
    ///<summary>subject permission denied</summary>
    [Description("subject permission denied")]
    SubjectPermissionDenied = 755,
    
    ///<summary>no permissions</summary>
    [Description("no permissions")]
    NoPermissions = 756,
    
    ///<summary>Device not bind user</summary>
    [Description("Device not bind user")]
    DeviceNotBindUser = 757,
    
    ///<summary>Param length limit</summary>
    [Description("Param length limit")]
    ParamLengthLimit = 758,
    
    ///<summary>Action not support</summary>
    [Description("Action not support")]
    ActionNotSupport = 760,
    
    ///<summary>Trigger not support</summary>
    [Description("Trigger not support")]
    TriggerNotSupport = 763,
    
    ///<summary>Ifttt name has exists</summary>
    [Description("Ifttt name has exists")]
    IftttNameHasExists = 768,
    
    ///<summary>Scene name has exists</summary>
    [Description("Scene name has exists")]
    SceneNameHasExists = 769,
    
    ///<summary>Device name has exists</summary>
    [Description("Device name has exists")]
    DeviceNameHasExists = 770,
    
    ///<summary>The device can not unbind</summary>
    [Description("The device can not unbind")]
    TheDeviceCanNotUnbind = 778,
    
    ///<summary>Condition event Permission denied</summary>
    [Description("Condition event Permission denied")]
    ConditionEventPermissionDenied = 788,
    
    ///<summary>Condition event duplicate name</summary>
    [Description("Condition event duplicate name")]
    ConditionEventDuplicateName = 789,
    
    ///<summary>Account not register</summary>
    [Description("Account not register")]
    AccountNotRegister = 801,
    
    ///<summary>Account not login</summary>
    [Description("Account not login")]
    AccountNotLogin = 802,
    
    ///<summary>User permission denied</summary>
    [Description("User permission denied")]
    UserPermissionDenied = 803,
    
    ///<summary>Token failed</summary>
    [Description("Token failed")]
    TokenFailed = 804,
    
    ///<summary>Account has register</summary>
    [Description("Account has register")]
    AccountHasRegister = 805,
    
    ///<summary>Account format error</summary>
    [Description("Account format error")]
    AccountFormatError = 807,
    
    ///<summary>Password incorrect</summary>
    [Description("Password incorrect")]
    PasswordIncorrect = 810,
    
    ///<summary>AuthCode incorrect</summary>
    [Description("AuthCode incorrect")]
    AuthCodeIncorrect = 811,
    
    ///<summary>Account type unsupport</summary>
    [Description("Account type unsupport")]
    AccountTypeUnsupport = 812,
    
    ///<summary>AuthCode incorrect</summary>
    [Description("AuthCode incorrect")]
    AuthCodeIncorrect1 = 816,
    
    ///<summary>AuthCode send all too often</summary>
    [Description("AuthCode send all too often")]
    AuthCodeSendAllTooOften = 817,
    
    ///<summary>AuthCode is invalid</summary>
    [Description("AuthCode is invalid")]
    AuthCodeIsInvalid = 820,
    
    ///<summary>Upgrade error</summary>
    [Description("Upgrade error")]
    UpgradeError = 901,
    
    ///<summary>Firmware not exist</summary>
    [Description("Firmware not exist")]
    FirmwareNotExist = 902,
    
    ///<summary>Package not exist</summary>
    [Description("Package not exist")]
    PackageNotExist = 903,
    
    ///<summary>Firmware already up to date</summary>
    [Description("Firmware already up to date")]
    FirmwareAlreadyUpToDate = 904,
    
    ///<summary>Firmware query is empty</summary>
    [Description("Firmware query is empty")]
    FirmwareQueryIsEmpty = 905,
    
    ///<summary>No updatable firmware</summary>
    [Description("No updatable firmware")]
    NoUpdatableFirmware = 906,
    
    ///<summary>firmware upgrade failed</summary>
    [Description("firmware upgrade failed")]
    FirmwareUpgradeFailed = 907,
    
    ///<summary>Device is being upgrade</summary>
    [Description("Device is being upgrade")]
    DeviceIsBeingUpgrade = 908,
    
    ///<summary>The sub device's gateway is being upgraded</summary>
    [Description("The sub device's gateway is being upgraded")]
    TheSubDeviceGatewayIsBeingUpgraded = 909,
    
    ///<summary>The gateway's sub device are being upgraded</summary>
    [Description("The gateway's sub device are being upgraded")]
    TheGatewaySubDeviceAreBeingUpgraded = 910,
    
    ///<summary>The sub device's gateway is also upgraded</summary>
    [Description("The sub device's gateway is also upgraded")]
    TheSubDeviceGatewayIsAlsoUpgraded = 911,
    
    ///<summary>Model can't be upgraded with firmware</summary>
    [Description("Model can't be upgraded with firmware")]
    ModelCanNotBeUpgradedWithFirmware = 912,
    
    ///<summary>Resource attr illegal</summary>
    [Description("Resource attr illegal")]
    ResourceAttrIllegal = 1003,
    
    ///<summary>Resource value illegal</summary>
    [Description("Resource value illegal")]
    ResourceValueIllegal = 1004,
    
    ///<summary>Subject type not support</summary>
    [Description("Subject type not support")]
    SubjectTypeNotSupport = 1006,
    
    ///<summary>Resource write not support</summary>
    [Description("Resource write not support")]
    ResourceWriteNotSupport = 1007,
    
    ///<summary>Resource attr not exist</summary>
    [Description("Resource attr not exist")]
    ResourceAttrNotExist = 1008,
    
    ///<summary>Report attr error</summary>
    [Description("Report attr error")]
    ReportAttrError = 1009,
    
    ///<summary>Report resourceId error</summary>
    [Description("Report resourceId error")]
    ReportResourceIdError = 1010,
    
    ///<summary>Linkage not exist</summary>
    [Description("Linkage not exist")]
    LinkageNotExist = 1201,
    
    ///<summary>Scene not exist</summary>
    [Description("Scene not exist")]
    SceneNotExist1 = 1202,
    
    ///<summary>Ifttt execute condition not satisfied</summary>
    [Description("Ifttt execute condition not satisfied")]
    IftttExecuteConditionNotSatisfied = 1203,
    
    ///<summary>linkage no device</summary>
    [Description("linkage no device")]
    LinkageNoDevice = 1204,
    
    ///<summary>Scene no device</summary>
    [Description("Scene no device")]
    SceneNoDevice = 1205,
    
    ///<summary>Delete local linkage failed</summary>
    [Description("Delete local linkage failed")]
    DeleteLocalLinkageFailed = 1206,
    
    ///<summary>Operation failed</summary>
    [Description("Operation failed")]
    OperationFailed = 1207,
    
    ///<summary>Ifttt parameter error</summary>
    [Description("Ifttt parameter error")]
    IftttParameterError = 1208,
    
    ///<summary>This action not definition</summary>
    [Description("This action not definition")]
    ThisActionNotDefinition = 1210,
    
    ///<summary>This trigger not definition</summary>
    [Description("This trigger not definition")]
    ThisTriggerNotDefinition = 1211,
    
    ///<summary>Action is empty</summary>
    [Description("Action is empty")]
    ActionIsEmpty = 1212,
    
    ///<summary>Ifttt execute failed</summary>
    [Description("Ifttt execute failed")]
    IftttExecuteFailed = 1221,
    
    ///<summary>Ifttt same name</summary>
    [Description("Ifttt same name")]
    IftttSameName = 1223,
    
    ///<summary>scene same name</summary>
    [Description("scene same name")]
    SceneSameName = 1224,
    
    ///<summary>Conditions of configuration is not correct</summary>
    [Description("Conditions of configuration is not correct")]
    ConditionsOfConfigurationIsNotCorrect = 1226,
    
    ///<summary>Conditions of configuration is not correct</summary>
    [Description("Conditions of configuration is not correct")]
    ConditionsOfConfigurationIsNotCorrect1 = 1227,
    
    ///<summary>Condition is repeated</summary>
    [Description("Condition is repeated")]
    ConditionIsRepeated = 1228,
    
    ///<summary>Conditions of configuration is not correct</summary>
    [Description("Conditions of configuration is not correct")]
    ConditionsOfConfigurationIsNotCorrect2 = 1229,
    
    ///<summary>Action is repeated</summary>
    [Description("Action is repeated")]
    ActionIsRepeated = 1230,
    
    ///<summary>Actions of configuration is not correct</summary>
    [Description("Actions of configuration is not correct")]
    ActionsOfConfigurationIsNotCorrect = 1231,
    
    ///<summary>Conditions and Actions of configuration is not correct</summary>
    [Description("Conditions and Actions of configuration is not correct")]
    ConditionsAndActionsOfConfigurationIsNotCorrect = 1232,
    
    ///<summary>ifttt is abnormal</summary>
    [Description("ifttt is abnormal")]
    IftttIsAbnormal = 1238,
    
    ///<summary>Conditions of configuration is not correct</summary>
    [Description("Conditions of configuration is not correct")]
    ConditionsOfConfigurationIsNotCorrect3 = 1239,
    
    ///<summary>The data is in operation</summary>
    [Description("The data is in operation")]
    TheDataIsInOperation = 1300,
    
    ///<summary>Get developer list error</summary>
    [Description("Get developer list error")]
    GetDeveloperListError = 2001,
    
    ///<summary>Appid or Appkey illegal</summary>
    [Description("Appid or Appkey illegal")]
    AppidOrAppkeyIllegal = 2002,
    
    ///<summary>AuthCode incorrect</summary>
    [Description("AuthCode incorrect")]
    AuthCodeIncorrect2 = 2003,
    
    ///<summary>AccessToken incorrect</summary>
    [Description("AccessToken incorrect")]
    AccessTokenIncorrect = 2004,
    
    ///<summary>AccessToken expired</summary>
    [Description("AccessToken expired")]
    AccessTokenExpired = 2005,
    
    ///<summary>RefreshToken incorrect</summary>
    [Description("RefreshToken incorrect")]
    RefreshTokenIncorrect = 2006,
    
    ///<summary>RefreshToken expired</summary>
    [Description("RefreshToken expired")]
    RefreshTokenExpired = 2007,
    
    ///<summary>Permission denied</summary>
    [Description("Permission denied")]
    PermissionDenied = 2008,
    
    ///<summary>Invalid OpenId</summary>
    [Description("Invalid OpenId")]
    InvalidOpenId = 2009,
    
    ///<summary>Unauthorized user</summary>
    [Description("Unauthorized user")]
    UnauthorizedUser = 2010,
    
    ///<summary>The query result is empty</summary>
    [Description("The query result is empty")]
    TheQueryResultIsEmpty = 2011,
    
    ///<summary>Invalid apply</summary>
    [Description("Invalid apply")]
    InvalidApply = 2012,
    
    ///<summary>Developer Permission denied</summary>
    [Description("Developer Permission denied")]
    DeveloperPermissionDenied = 2013,
    
    ///<summary>Resource Permission denied</summary>
    [Description("Resource Permission denied")]
    ResourcePermissionDenied = 2014,
    
    ///<summary>subscriber faild</summary>
    [Description("subscriber faild")]
    SubscriberFaild = 2015,
    
    ///<summary>AccountId has exist</summary>
    [Description("AccountId has exist")]
    AccountIdHasExist = 2016,
    
    ///<summary>Appkey exceeds the limit</summary>
    [Description("Appkey exceeds the limit")]
    AppkeyExceedsTheLimit = 2017,
    
    ///<summary>IP config exceed the limit</summary>
    [Description("IP config exceed the limit")]
    IPConfigExceedTheLimit = 2018,
    
    ///<summary>Application not activated</summary>
    [Description("Application not activated")]
    ApplicationNotActivated = 2022,
}
