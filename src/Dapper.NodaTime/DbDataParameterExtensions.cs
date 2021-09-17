using System.Data;

namespace Dapper.NodaTime
{
    internal static class DbDataParameterExtensions
    {
        public static void SetSqlDbType(this IDbDataParameter parameter, SqlDbType sqlDbType)
        {
            // Using reflection so as not to impose a choice on the SqlClient library, letting the consumer choose
            // between System.Data.SqlClient or Microsoft.Data.SqlClient which both support the SqlDbType property
            var property = parameter.GetType().GetProperty("SqlDbType");
            if (property != null && property.CanWrite && property.PropertyType == typeof(SqlDbType))
            {
                property.SetValue(parameter, sqlDbType);
            }
        }
    }
}