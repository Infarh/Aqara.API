#nullable enable

namespace Aqara.API;

/// <summary>Содержит базовые URL-адреса API для поддерживаемых регионов</summary>
public static class Servers
{
    /// <summary>API-сервер для Европы</summary>
    public const string Europe = "https://open-ger.aqara.com/v3.0/open/api"; // URL API для региона Европа

    /// <summary>API-сервер для России</summary>
    public const string Russia = "https://open-ru.aqara.com/v3.0/open/api"; // URL API для региона Россия

    /// <summary>API-сервер для Кореи</summary>
    public const string Korea = "https://open-kr.aqara.com/v3.0/open/api"; // URL API для региона Корея

    /// <summary>API-сервер для США</summary>
    public const string USA = "https://open-usa.aqara.com/v3.0/open/api"; // URL API для региона США

    /// <summary>API-сервер для Китая</summary>
    public const string China = "https://open-cn.aqara.com/v3.0/open/api"; // URL API для региона Китай
}