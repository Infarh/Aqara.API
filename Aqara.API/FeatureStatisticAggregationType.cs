using System.ComponentModel.DataAnnotations;
using EnumFastToStringGenerated;

namespace Aqara.API;

/// <summary>Вид значения</summary>
[Flags]
[EnumGenerator]
public enum FeatureStatisticAggregationType : short
{
    /// <summary>Разностное</summary>
    [Display(Name = "Разностное значение")]
    Difference = 0b0_0001,
    
    /// <summary>Минимум</summary>
    [Display(Name = "Минимум")]
    Min = 0b0_0010,
    
    /// <summary>Максимум</summary>
    [Display(Name = "Максимум")]
    Max = 0b0_0100,
    
    /// <summary>Среднее</summary>
    [Display(Name = "Среднее")]
    Average = 0b0_1000,
    
    /// <summary>Частота</summary>
    [Display(Name = "Частота")]
    Frequency = 0b1_0000,
    
    /// <summary>Все</summary>
    [Display(Name = "Все")]
    All = 0b1_1111,
    
    /// <summary>Все</summary>
    [Display(Name = "Все2")]
    All2 = 0
}