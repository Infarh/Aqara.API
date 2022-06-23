using System.ComponentModel.DataAnnotations;
using EnumFastToStringGenerated;

namespace Aqara.API.Models;

/// <summary>Тип значения статистического значения параметра</summary>
[EnumGenerator]
public enum StatisticValueType
{
    /// <summary>Разностное значение</summary>
    [Display(Name = "Разностное значение")]
    Difference,

    /// <summary>Минимум значения</summary>
    [Display(Name = "Минимум значения")]
    Min,

    /// <summary>Максимум значения</summary>
    [Display(Name = "Максимум значения")]
    Max,

    /// <summary>Среднее значение</summary>
    [Display(Name = "Среднее значение")]
    Average,

    /// <summary>Частота</summary>
    [Display(Name = "Частота")]
    Frequency
}
