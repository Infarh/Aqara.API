using System.ComponentModel;
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

    ///<summary>Success</summary>
    [Display(Name = "Успех")]
    Success = 0,
    
    ///<summary>Timeout</summary>
    [Display(Name = "Таймаут")]
    Timeout = 100,
    
    ///<summary>Invalid data package</summary>
    [Display(Name = "Invalid data package")]
    InvalidDataPackage = 101,
    
    ///<summary>Data package has altered</summary>
    [Display(Name = "Data package has altered")]
    DataPackageHasAltered = 102,
    
    ///<summary>Data package may lose</summary>
    [Display(Name = "Data package may lose")]
    DataPackageMayLose = 103,
    
    ///<summary>Server busy</summary>
    [Display(Name = "Server busy")]
    ServerBusy = 104,
    
    ///<summary>Data package has expired</summary>
    [Display(Name = "Data package has expired")]
    DataPackageHasExpired = 105,
    
    ///<summary>Invalid sign</summary>
    [Display(Name = "Invalid sign")]
    InvalidSign = 106,
    
    ///<summary>Illegal appKey</summary>
    [Display(Name = "Illegal appKey")]
    IllegalAppKey = 107,
    
    ///<summary>Token has expired</summary>
    [Display(Name = "Token has expired")]
    TokenHasExpired = 108,
    
    ///<summary>Token is absence</summary>
    [Display(Name = "Token is absence")]
    TokenIsAbsence = 109,
    
    ///<summary>Params error</summary>
    [Display(Name = "Params error")]
    ParamsError = 302,
    
    ///<summary>Request params type error</summary>
    [Display(Name = "Request params type error")]
    RequestParamsTypeError = 303,
    
    ///<summary>Request method not support</summary>
    [Display(Name = "Request method not support")]
    RequestMethodNotSupport = 304,
    
    ///<summary>Header Params error</summary>
    [Display(Name = "Header Params error")]
    HeaderParamsError = 305,
    
    ///<summary>Request path not open</summary>
    [Display(Name = "Request path not open")]
    RequestPathNotOpen = 306,
    
    ///<summary>Request forbidden</summary>
    [Display(Name = "Request forbidden")]
    RequestForbidden = 403,
    
    ///<summary>Too Many Requests</summary>
    [Display(Name = "Too Many Requests")]
    TooManyRequests = 429,
    
    ///<summary>Service impl error</summary>
    [Display(Name = "Service impl error")]
    ServiceImplError = 500,
    
    ///<summary>Service proxy error</summary>
    [Display(Name = "Service proxy error")]
    ServiceProxyError = 501,
    
    ///<summary>Device not register</summary>
    [Display(Name = "Device not register")]
    DeviceNotRegister = 601,
    
    ///<summary>Device is offline</summary>
    [Display(Name = "Device is offline")]
    DeviceIsOffline = 602,
    
    ///<summary>Device permission denied</summary>
    [Display(Name = "Device permission denied")]
    DevicePermissionDenied = 603,
    
    ///<summary>Illegal device id</summary>
    [Display(Name = "Illegal device id")]
    IllegalDeviceId = 604,
    
    ///<summary>Device info inconsistent</summary>
    [Display(Name = "Device info inconsistent")]
    DeviceInfoInconsistent = 605,
    
    ///<summary>Device request not support</summary>
    [Display(Name = "Device request not support")]
    DeviceRequestNotSupport = 606,
    
    ///<summary>Gateway has been bind</summary>
    [Display(Name = "Gateway has been bind")]
    GatewayHasBeenBind = 607,
    
    ///<summary>Sub device bind error</summary>
    [Display(Name = "Sub device bind error")]
    SubDeviceBindError = 608,
    
    ///<summary>Gateway unbind error</summary>
    [Display(Name = "Gateway unbind error")]
    GatewayUnbindError = 609,
    
    ///<summary>Subdevice unbind error</summary>
    [Display(Name = "Subdevice unbind error")]
    SubdeviceUnbindError = 610,
    
    ///<summary>Subdevice not bind</summary>
    [Display(Name = "Subdevice not bind")]
    SubdeviceNotBind = 611,
    
    ///<summary>Gateway request not response</summary>
    [Display(Name = "Gateway request not response")]
    GatewayRequestNotResponse = 612,
    
    ///<summary>Not find parent device</summary>
    [Display(Name = "Not find parent device")]
    NotFindParentDevice = 615,
    
    ///<summary>BindKey time out</summary>
    [Display(Name = "BindKey time out")]
    BindKeyTimeOut = 636,
    
    ///<summary>Irid not exists</summary>
    [Display(Name = "Irid not exists")]
    IridNotExists = 637,
    
    ///<summary>Sub device not support this operation</summary>
    [Display(Name = "Sub device not support this operation")]
    SubDeviceNotSupportThisOperation = 638,
    
    ///<summary>Device cannot mount sub device</summary>
    [Display(Name = "Device cannot mount sub device")]
    DeviceCannotMountSubDevice = 639,
    
    ///<summary>Device five code not found</summary>
    [Display(Name = "Device five code not found")]
    DeviceFiveCodeNotFound = 640,
    
    ///<summary>Bluetooth device operate with wrong step</summary>
    [Display(Name = "Bluetooth device operate with wrong step")]
    BluetoothDeviceOperateWithWrongStep = 641,
    
    ///<summary>Bluetooth device validate wrong</summary>
    [Display(Name = "Bluetooth device validate wrong")]
    BluetoothDeviceValidateWrong = 642,
    
    ///<summary>Bluetooth info not exist</summary>
    [Display(Name = "Bluetooth info not exist")]
    BluetoothInfoNotExist = 643,
    
    ///<summary>Failed validate security code</summary>
    [Display(Name = "Failed validate security code")]
    FailedValidateSecurityCode = 644,
    
    ///<summary>App bluetooth device register wrong step</summary>
    [Display(Name = "App bluetooth device register wrong step")]
    AppBluetoothDeviceRegisterWrongStep = 645,
    
    ///<summary>Gateway not exists</summary>
    [Display(Name = "Gateway not exists")]
    GatewayNotExists = 651,
    
    ///<summary>Gateway limit</summary>
    [Display(Name = "Gateway limit")]
    GatewayLimit = 652,
    
    ///<summary>dynamic sequence run failed</summary>
    [Display(Name = "dynamic sequence run failed")]
    DynamicSequenceRunFailed = 655,
    
    ///<summary>Device not allow bind</summary>
    [Display(Name = "Device not allow bind")]
    DeviceNotAllowBind = 656,
    
    ///<summary>Device group config exist</summary>
    [Display(Name = "Device group config exist")]
    DeviceGroupConfigExist = 657,
    
    ///<summary>This ir device not support copy</summary>
    [Display(Name = "This ir device not support copy")]
    ThisIrDeviceNotSupportCopy = 658,
    
    ///<summary>Device function not support</summary>
    [Display(Name = "Device function not support")]
    DeviceFunctionNotSupport = 664,
    
    ///<summary>Position not exist</summary>
    [Display(Name = "Position not exist")]
    PositionNotExist = 701,
    
    ///<summary>Position cannot deleted</summary>
    [Display(Name = "Position cannot deleted")]
    PositionCannotDeleted = 702,
    
    ///<summary>Position name duplication</summary>
    [Display(Name = "Position name duplication")]
    PositionNameDuplication = 703,
    
    ///<summary>Default position create duplication</summary>
    [Display(Name = "Default position create duplication")]
    DefaultPositionCreateDuplication = 704,
    
    ///<summary>Device name duplication</summary>
    [Display(Name = "Device name duplication")]
    DeviceNameDuplication = 705,
    
    ///<summary>Device permission denied</summary>
    [Display(Name = "Device permission denied")]
    DevicePermissionDenied1 = 706,
    
    ///<summary>Ifttt permission denied</summary>
    [Display(Name = "Ifttt permission denied")]
    IftttPermissionDenied = 707,
    
    ///<summary>Scene permission denied</summary>
    [Display(Name = "Scene permission denied")]
    ScenePermissionDenied = 708,
    
    ///<summary>Service permission denied</summary>
    [Display(Name = "Service permission denied")]
    ServicePermissionDenied = 709,
    
    ///<summary>Position permission denied</summary>
    [Display(Name = "Position permission denied")]
    PositionPermissionDenied = 710,
    
    ///<summary>Parent position error</summary>
    [Display(Name = "Parent position error")]
    ParentPositionError = 712,
    
    ///<summary>Position not real position</summary>
    [Display(Name = "Position not real position")]
    PositionNotRealPosition = 713,
    
    ///<summary>Position is not allowed to be deleted</summary>
    [Display(Name = "Position is not allowed to be deleted")]
    PositionIsNotAllowedToBeDeleted = 714,
    
    ///<summary>Position error</summary>
    [Display(Name = "Position error")]
    PositionError = 715,
    
    ///<summary>Scene not exist</summary>
    [Display(Name = "Scene not exist")]
    SceneNotExist = 716,
    
    ///<summary>Device does not belong to this user</summary>
    [Display(Name = "Device does not belong to this user")]
    DeviceDoesNotBelongToThisUser = 717,
    
    ///<summary>Data error</summary>
    [Display(Name = "Data error")]
    DataError = 718,
    
    ///<summary>Device no bind user</summary>
    [Display(Name = "Device no bind user")]
    DeviceNoBindUser = 719,
    
    ///<summary>Out of position layer</summary>
    [Display(Name = "Out of position layer")]
    OutOfPositionLayer = 722,
    
    ///<summary>Device size beyond</summary>
    [Display(Name = "Device size beyond")]
    DeviceSizeBeyond = 726,
    
    ///<summary>Position size beyond</summary>
    [Display(Name = "Position size beyond")]
    PositionSizeBeyond = 727,
    
    ///<summary>Start or end time cannot be empty</summary>
    [Display(Name = "Start or end time cannot be empty")]
    StartOrEndTimeCannotBeEmpty = 728,
    
    ///<summary>The start time must not be greater than the end time</summary>
    [Display(Name = "The start time must not be greater than the end time")]
    TheStartTimeMustNotBeGreaterThanTheEndTime = 729,
    
    ///<summary>Start or end time not a timestamp</summary>
    [Display(Name = "Start or end time not a timestamp")]
    StartOrEndTimeNotATimestamp = 730,
    
    ///<summary>Ifttt not exists</summary>
    [Display(Name = "Ifttt not exists")]
    IftttNotExists = 731,
    
    ///<summary>BindKey not exists</summary>
    [Display(Name = "BindKey not exists")]
    BindKeyNotExists = 745,
    
    ///<summary>Gateway not connect cloud</summary>
    [Display(Name = "Gateway not connect cloud")]
    GatewayNotConnectCloud = 746,
    
    ///<summary>Device unsupported</summary>
    [Display(Name = "Device unsupported")]
    DeviceUnsupported = 747,
    
    ///<summary>Category model not exists</summary>
    [Display(Name = "Category model not exists")]
    CategoryModelNotExists = 748,
    
    ///<summary>Custom Action name duplicate</summary>
    [Display(Name = "Custom Action name duplicate")]
    CustomActionNameDuplicate = 749,
    
    ///<summary>Ircode key not exists</summary>
    [Display(Name = "Ircode key not exists")]
    IrcodeKeyNotExists = 750,
    
    ///<summary>BindKey has been used</summary>
    [Display(Name = "BindKey has been used")]
    BindKeyHasBeenUsed = 751,
    
    ///<summary>ir device limit</summary>
    [Display(Name = "ir device limit")]
    IrDeviceLimit = 753,
    
    ///<summary>Custom Action not exist</summary>
    [Display(Name = "Custom Action not exist")]
    CustomActionNotExist = 754,
    
    ///<summary>subject permission denied</summary>
    [Display(Name = "subject permission denied")]
    SubjectPermissionDenied = 755,
    
    ///<summary>no permissions</summary>
    [Display(Name = "no permissions")]
    NoPermissions = 756,
    
    ///<summary>Device not bind user</summary>
    [Display(Name = "Device not bind user")]
    DeviceNotBindUser = 757,
    
    ///<summary>Param length limit</summary>
    [Display(Name = "Param length limit")]
    ParamLengthLimit = 758,
    
    ///<summary>Action not support</summary>
    [Display(Name = "Action not support")]
    ActionNotSupport = 760,
    
    ///<summary>Trigger not support</summary>
    [Display(Name = "Trigger not support")]
    TriggerNotSupport = 763,
    
    ///<summary>Ifttt name has exists</summary>
    [Display(Name = "Ifttt name has exists")]
    IftttNameHasExists = 768,
    
    ///<summary>Scene name has exists</summary>
    [Display(Name = "Scene name has exists")]
    SceneNameHasExists = 769,
    
    ///<summary>Device name has exists</summary>
    [Display(Name = "Device name has exists")]
    DeviceNameHasExists = 770,
    
    ///<summary>The device can not unbind</summary>
    [Display(Name = "The device can not unbind")]
    TheDeviceCanNotUnbind = 778,
    
    ///<summary>Condition event Permission denied</summary>
    [Display(Name = "Condition event Permission denied")]
    ConditionEventPermissionDenied = 788,
    
    ///<summary>Condition event duplicate name</summary>
    [Display(Name = "Condition event duplicate name")]
    ConditionEventDuplicateName = 789,
    
    ///<summary>Account not register</summary>
    [Display(Name = "Account not register")]
    AccountNotRegister = 801,
    
    ///<summary>Account not login</summary>
    [Display(Name = "Account not login")]
    AccountNotLogin = 802,
    
    ///<summary>User permission denied</summary>
    [Display(Name = "User permission denied")]
    UserPermissionDenied = 803,
    
    ///<summary>Token failed</summary>
    [Display(Name = "Token failed")]
    TokenFailed = 804,
    
    ///<summary>Account has register</summary>
    [Display(Name = "Account has register")]
    AccountHasRegister = 805,
    
    ///<summary>Account format error</summary>
    [Display(Name = "Account format error")]
    AccountFormatError = 807,
    
    ///<summary>Password incorrect</summary>
    [Display(Name = "Password incorrect")]
    PasswordIncorrect = 810,
    
    ///<summary>AuthCode incorrect</summary>
    [Display(Name = "AuthCode incorrect")]
    AuthCodeIncorrect = 811,
    
    ///<summary>Account type unsupport</summary>
    [Display(Name = "Account type unsupport")]
    AccountTypeUnsupport = 812,
    
    ///<summary>AuthCode incorrect</summary>
    [Display(Name = "AuthCode incorrect")]
    AuthCodeIncorrect1 = 816,
    
    ///<summary>AuthCode send all too often</summary>
    [Display(Name = "AuthCode send all too often")]
    AuthCodeSendAllTooOften = 817,
    
    ///<summary>AuthCode is invalid</summary>
    [Display(Name = "AuthCode is invalid")]
    AuthCodeIsInvalid = 820,
    
    ///<summary>Upgrade error</summary>
    [Display(Name = "Upgrade error")]
    UpgradeError = 901,
    
    ///<summary>Firmware not exist</summary>
    [Display(Name = "Firmware not exist")]
    FirmwareNotExist = 902,
    
    ///<summary>Package not exist</summary>
    [Display(Name = "Package not exist")]
    PackageNotExist = 903,
    
    ///<summary>Firmware already up to date</summary>
    [Display(Name = "Firmware already up to date")]
    FirmwareAlreadyUpToDate = 904,
    
    ///<summary>Firmware query is empty</summary>
    [Display(Name = "Firmware query is empty")]
    FirmwareQueryIsEmpty = 905,
    
    ///<summary>No updatable firmware</summary>
    [Display(Name = "No updatable firmware")]
    NoUpdatableFirmware = 906,
    
    ///<summary>firmware upgrade failed</summary>
    [Display(Name = "firmware upgrade failed")]
    FirmwareUpgradeFailed = 907,
    
    ///<summary>Device is being upgrade</summary>
    [Display(Name = "Device is being upgrade")]
    DeviceIsBeingUpgrade = 908,
    
    ///<summary>The sub device's gateway is being upgraded</summary>
    [Display(Name = "The sub device's gateway is being upgraded")]
    TheSubDeviceGatewayIsBeingUpgraded = 909,
    
    ///<summary>The gateway's sub device are being upgraded</summary>
    [Display(Name = "The gateway's sub device are being upgraded")]
    TheGatewaySubDeviceAreBeingUpgraded = 910,
    
    ///<summary>The sub device's gateway is also upgraded</summary>
    [Display(Name = "The sub device's gateway is also upgraded")]
    TheSubDeviceGatewayIsAlsoUpgraded = 911,
    
    ///<summary>Model can't be upgraded with firmware</summary>
    [Display(Name = "Model can't be upgraded with firmware")]
    ModelCanNotBeUpgradedWithFirmware = 912,
    
    ///<summary>Resource attr illegal</summary>
    [Display(Name = "Resource attr illegal")]
    ResourceAttrIllegal = 1003,
    
    ///<summary>Resource value illegal</summary>
    [Display(Name = "Resource value illegal")]
    ResourceValueIllegal = 1004,
    
    ///<summary>Subject type not support</summary>
    [Display(Name = "Subject type not support")]
    SubjectTypeNotSupport = 1006,
    
    ///<summary>Resource write not support</summary>
    [Display(Name = "Resource write not support")]
    ResourceWriteNotSupport = 1007,
    
    ///<summary>Resource attr not exist</summary>
    [Display(Name = "Resource attr not exist")]
    ResourceAttrNotExist = 1008,
    
    ///<summary>Report attr error</summary>
    [Display(Name = "Report attr error")]
    ReportAttrError = 1009,
    
    ///<summary>Report resourceId error</summary>
    [Display(Name = "Report resourceId error")]
    ReportResourceIdError = 1010,
    
    ///<summary>Linkage not exist</summary>
    [Display(Name = "Linkage not exist")]
    LinkageNotExist = 1201,
    
    ///<summary>Scene not exist</summary>
    [Display(Name = "Scene not exist")]
    SceneNotExist1 = 1202,
    
    ///<summary>Ifttt execute condition not satisfied</summary>
    [Display(Name = "Ifttt execute condition not satisfied")]
    IftttExecuteConditionNotSatisfied = 1203,
    
    ///<summary>linkage no device</summary>
    [Display(Name = "linkage no device")]
    LinkageNoDevice = 1204,
    
    ///<summary>Scene no device</summary>
    [Display(Name = "Scene no device")]
    SceneNoDevice = 1205,
    
    ///<summary>Delete local linkage failed</summary>
    [Display(Name = "Delete local linkage failed")]
    DeleteLocalLinkageFailed = 1206,
    
    ///<summary>Operation failed</summary>
    [Display(Name = "Operation failed")]
    OperationFailed = 1207,
    
    ///<summary>Ifttt parameter error</summary>
    [Display(Name = "Ifttt parameter error")]
    IftttParameterError = 1208,
    
    ///<summary>This action not definition</summary>
    [Display(Name = "This action not definition")]
    ThisActionNotDefinition = 1210,
    
    ///<summary>This trigger not definition</summary>
    [Display(Name = "This trigger not definition")]
    ThisTriggerNotDefinition = 1211,
    
    ///<summary>Action is empty</summary>
    [Display(Name = "Action is empty")]
    ActionIsEmpty = 1212,
    
    ///<summary>Ifttt execute failed</summary>
    [Display(Name = "Ifttt execute failed")]
    IftttExecuteFailed = 1221,
    
    ///<summary>Ifttt same name</summary>
    [Display(Name = "Ifttt same name")]
    IftttSameName = 1223,
    
    ///<summary>scene same name</summary>
    [Display(Name = "scene same name")]
    SceneSameName = 1224,
    
    ///<summary>Conditions of configuration is not correct</summary>
    [Display(Name = "Conditions of configuration is not correct")]
    ConditionsOfConfigurationIsNotCorrect = 1226,
    
    ///<summary>Conditions of configuration is not correct</summary>
    [Display(Name = "Conditions of configuration is not correct")]
    ConditionsOfConfigurationIsNotCorrect1 = 1227,
    
    ///<summary>Condition is repeated</summary>
    [Display(Name = "Condition is repeated")]
    ConditionIsRepeated = 1228,
    
    ///<summary>Conditions of configuration is not correct</summary>
    [Display(Name = "Conditions of configuration is not correct")]
    ConditionsOfConfigurationIsNotCorrect2 = 1229,
    
    ///<summary>Action is repeated</summary>
    [Display(Name = "Action is repeated")]
    ActionIsRepeated = 1230,
    
    ///<summary>Actions of configuration is not correct</summary>
    [Display(Name = "Actions of configuration is not correct")]
    ActionsOfConfigurationIsNotCorrect = 1231,
    
    ///<summary>Conditions and Actions of configuration is not correct</summary>
    [Display(Name = "Conditions and Actions of configuration is not correct")]
    ConditionsAndActionsOfConfigurationIsNotCorrect = 1232,
    
    ///<summary>ifttt is abnormal</summary>
    [Display(Name = "ifttt is abnormal")]
    IftttIsAbnormal = 1238,
    
    ///<summary>Conditions of configuration is not correct</summary>
    [Display(Name = "Conditions of configuration is not correct")]
    ConditionsOfConfigurationIsNotCorrect3 = 1239,
    
    ///<summary>The data is in operation</summary>
    [Display(Name = "The data is in operation")]
    TheDataIsInOperation = 1300,
    
    ///<summary>Get developer list error</summary>
    [Display(Name = "Get developer list error")]
    GetDeveloperListError = 2001,
    
    ///<summary>Appid or Appkey illegal</summary>
    [Display(Name = "Appid or Appkey illegal")]
    AppidOrAppkeyIllegal = 2002,
    
    ///<summary>AuthCode incorrect</summary>
    [Display(Name = "AuthCode incorrect")]
    AuthCodeIncorrect2 = 2003,
    
    ///<summary>AccessToken incorrect</summary>
    [Display(Name = "AccessToken incorrect")]
    AccessTokenIncorrect = 2004,
    
    ///<summary>AccessToken expired</summary>
    [Display(Name = "AccessToken expired")]
    AccessTokenExpired = 2005,
    
    ///<summary>RefreshToken incorrect</summary>
    [Display(Name = "RefreshToken incorrect")]
    RefreshTokenIncorrect = 2006,
    
    ///<summary>RefreshToken expired</summary>
    [Display(Name = "RefreshToken expired")]
    RefreshTokenExpired = 2007,
    
    ///<summary>Permission denied</summary>
    [Display(Name = "Permission denied")]
    PermissionDenied = 2008,
    
    ///<summary>Invalid OpenId</summary>
    [Display(Name = "Invalid OpenId")]
    InvalidOpenId = 2009,
    
    ///<summary>Unauthorized user</summary>
    [Display(Name = "Unauthorized user")]
    UnauthorizedUser = 2010,
    
    ///<summary>The query result is empty</summary>
    [Display(Name = "The query result is empty")]
    TheQueryResultIsEmpty = 2011,
    
    ///<summary>Invalid apply</summary>
    [Display(Name = "Invalid apply")]
    InvalidApply = 2012,
    
    ///<summary>Developer Permission denied</summary>
    [Display(Name = "Developer Permission denied")]
    DeveloperPermissionDenied = 2013,
    
    ///<summary>Resource Permission denied</summary>
    [Display(Name = "Resource Permission denied")]
    ResourcePermissionDenied = 2014,
    
    ///<summary>subscriber faild</summary>
    [Display(Name = "subscriber faild")]
    SubscriberFaild = 2015,
    
    ///<summary>AccountId has exist</summary>
    [Display(Name = "AccountId has exist")]
    AccountIdHasExist = 2016,
    
    ///<summary>Appkey exceeds the limit</summary>
    [Display(Name = "Appkey exceeds the limit")]
    AppkeyExceedsTheLimit = 2017,
    
    ///<summary>IP config exceed the limit</summary>
    [Display(Name = "IP config exceed the limit")]
    IPConfigExceedTheLimit = 2018,
    
    ///<summary>Application not activated</summary>
    [Display(Name = "Application not activated")]
    ApplicationNotActivated = 2022,
}
