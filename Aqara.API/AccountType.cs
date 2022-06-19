using System.ComponentModel.DataAnnotations;
using EnumFastToStringGenerated;

namespace Aqara.API;

[EnumGenerator]
public enum AccountType
{
    /// <summary>Аккаунт Aqara</summary>
    [Display(Name = "Аккаунт Aqara")]
    Aqara,

    /// <summary>Аккаунт проекта</summary>
    [Display(Name = "Аккаунт проекта")]
    Project,

    /// <summary>Виртуальный аккаунт</summary>
    [Display(Name = "Виртуальный аккаунт")]
    VirtualAccount
}
