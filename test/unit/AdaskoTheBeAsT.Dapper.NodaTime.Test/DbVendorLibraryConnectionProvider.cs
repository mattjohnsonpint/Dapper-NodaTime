using System;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace AdaskoTheBeAsT.Dapper.NodaTime.Test
{
    public static class DbVendorLibraryConnectionProvider
    {
        public static IDbConnection Provide(DbVendorLibrary vendorLibrary, IConfiguration configuration)
        {
            var connectionString = DbVendorLibraryConnectionStringProvider.Provide(configuration, vendorLibrary);
            return vendorLibrary switch
            {
                DbVendorLibrary.MicrosoftSqlServer => new Microsoft.Data.SqlClient.SqlConnection(connectionString),
#if NET462_OR_GREATER
                DbVendorLibrary.SystemSqlServer => new System.Data.SqlClient.SqlConnection(connectionString),
#endif
                _ => throw new ArgumentOutOfRangeException(nameof(vendorLibrary), "Vendor library unknown"),
            };
        }
    }
}
