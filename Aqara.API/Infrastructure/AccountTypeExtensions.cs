using System.ComponentModel;

namespace Aqara.API.Infrastructure;

public static class AccountTypeExtensions
{
    public static int ToInt(this AccountType AccountType) => AccountType switch
    {
        AccountType.Aqara => 0,
        AccountType.Project => 1,
        AccountType.VirtualAccount => 2,
        _ => throw new InvalidEnumArgumentException(nameof(AccountType), (int)AccountType, typeof(AccountType))
    };
}
