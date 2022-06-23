using System.ComponentModel.DataAnnotations;

using EnumFastToStringGenerated;

namespace Aqara.API.Models;

/// <summary>Информация о возможности устройства</summary>
public class DeviceFeatureInfo
{
    /// <summary>Идентификатор возможности устройства</summary>
    public string FeatureId { get; init; } = null!;

    /// <summary>Название возможности</summary>
    public string Name { get; init; } = null!;

    /// <summary>Английское название возможности</summary>
    public string NameEn { get; init; } = null!;

    /// <summary>Описание возможности устройства</summary>
    public string Description { get; init; } = null!;

    /// <summary>Английское описание возможности устройства</summary>
    public string DescriptionEn { get; init; } = null!;

    /// <summary>Минимальное значение параметра</summary>
    public long MinValue { get; init; }

    /// <summary>Максимальное значение параметра</summary>
    public long MaxValue { get; init; }

    /// <summary>Единица измерения</summary>
    public int? Unit { get; init; }

    /// <summary>Значение по умолчанию</summary>
    public string DefaultValue { get; init; } = null!;

    public string SubjectModel { get; init; } = null!;

    /// <summary>Режим доступа к значению (чтение/запись/чтение-запись)</summary>
    public DeviceFeatureAccess Access { get; init; }

    public string Server { get; init; } = null!;

    public string Enums { get; init; } = null!;

    public override string ToString() => $"{FeatureId}:{Name}[{MinValue}:{MaxValue}]:{Access}";
}

/// <summary>Режим доступа к значению</summary>
[EnumGenerator]
public enum DeviceFeatureAccess
{
    /// <summary>Чтение</summary>
    [Display(Name = "Чтение")]
    Read,

    /// <summary>Запись</summary>
    [Display(Name = "Запись")]
    Write,

    /// <summary>Чтение и запись</summary>
    [Display(Name = "Чтение и запись")]
    ReadWrite,
}