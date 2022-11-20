using System;
using System.Data;

namespace AdaskoTheBeAsT.Dapper.NodaTime.Test
{
    public static class DbVendorLibraryConnectionProvider
    {
        public static IDbConnection Provide(DbVendorLibrary vendorLibrary, string connectionString)
        {
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
