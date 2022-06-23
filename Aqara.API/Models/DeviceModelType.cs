using System.ComponentModel.DataAnnotations;
using EnumFastToStringGenerated;

namespace Aqara.API.Models;

/// <summary>Вид устройства</summary>
[EnumGenerator]
public enum DeviceModelType
{
    /// <summary>Шлюз с дочерними устройствами</summary>
    [Display(Name = "Шлюз с дочерними устройствами")]
    GatewayWithChilds = 1,

    /// <summary>Шлюз без дочерних устройств</summary>
    [Display(Name = "Шлюз без дочерних устройств")]
    GatewayWithoutChilds = 2,

    /// <summary>Обычное устройство</summary>
    [Display(Name = "Обычное устройство")]
    SubDevice = 3,
}
