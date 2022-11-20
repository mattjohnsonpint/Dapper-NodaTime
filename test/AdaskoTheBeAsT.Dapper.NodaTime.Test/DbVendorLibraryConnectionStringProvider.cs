using System;
using Microsoft.Extensions.Configuration;

namespace AdaskoTheBeAsT.Dapper.NodaTime.Test
{
    public static class DbVendorLibraryConnectionStringProvider
    {
        public static string Provide(IConfiguration configuration, DbVendorLibrary vendorLibrary) =>
            vendorLibrary switch
            {
                DbVendorLibrary.MicrosoftSqlServer => configuration.GetConnectionString("TestDBMicrosoftSqlServer") ?? string.Empty,
#if NET462_OR_GREATER
                DbVendorLibrary.SystemSqlServer => configuration.GetConnectionString("TestDBSystemSqlServer") ?? string.Empty,
#endif
                _ => throw new ArgumentOutOfRangeException(nameof(vendorLibrary), "Vendor library unknown"),
            };
    }
}
