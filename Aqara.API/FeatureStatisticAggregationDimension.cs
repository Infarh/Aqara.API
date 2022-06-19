using System.ComponentModel.DataAnnotations;

namespace Aqara.API;

/// <summary>Варианты сбора статистических данных</summary>
public enum FeatureStatisticAggregationDimension
{
    /// <summary>30 минут</summary>
    [Display(Name = "30 минут")]
    Interval30m,
    
    /// <summary>Пол часа</summary>
    [Display(Name = "Пол часа")]
    Interval05h = Interval30m,
    
    /// <summary>Час</summary>
    [Display(Name = "Час")]
    Interval1h,
    
    /// <summary>2 часа</summary>
    [Display(Name = "2 часа")]
    Interval2h,
    
    /// <summary>3 часа</summary>
    [Display(Name = "3 часа")]
    Interval3h,
    
    /// <summary>4 часа</summary>
    [Display(Name = "4 часа")]
    Interval4h,
    
    /// <summary>5 часов</summary>
    [Display(Name = "5 часов")]
    Interval5h,
    
    /// <summary>6 часов</summary>
    [Display(Name = "6 часов")]
    Interval6h,
    
    /// <summary>12 часов</summary>
    [Display(Name = "12 часов")]
    Interval12h,
    
    /// <summary>1 день</summary>
    [Display(Name = "1 день")]
    Interval1d,
    
    /// <summary>24 часа</summary>
    [Display(Name = "24 часа")]
    Interval24h = Interval1d,
    
    /// <summary>7 дней</summary>
    [Display(Name = "7 дней")]
    Interval7d,
    
    /// <summary>30 дней</summary>
    [Display(Name = "30 дней")]
    Interval30d,

    /// <summary>Месяц</summary>
    [Display(Name = "Месяц")]
    Interval1M
}