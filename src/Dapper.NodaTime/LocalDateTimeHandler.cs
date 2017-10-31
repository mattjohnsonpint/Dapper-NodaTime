using System;
using System.Data;
using System.Data.SqlClient;
using NodaTime;

#if NETSTANDARD1_3
using DataException = System.InvalidOperationException;
#endif

namespace Dapper.NodaTime
{
    public class LocalDateTimeHandler : SqlMapper.TypeHandler<LocalDateTime>
    {
        private LocalDateTimeHandler()
        {
        }

        public static readonly LocalDateTimeHandler Default = new LocalDateTimeHandler();

        public override void SetValue(IDbDataParameter parameter, LocalDateTime value)
        {
            parameter.Value = value.ToDateTimeUnspecified();

            if (parameter is SqlParameter sqlParameter)
            {
                sqlParameter.SqlDbType = SqlDbType.DateTime2;
            }
        }

        public override LocalDateTime Parse(object value)
        {
            if (value is DateTime dateTime)
            {
                return LocalDateTime.FromDateTime(dateTime);
            }

            throw new DataException("Cannot convert " + value.GetType() + " to NodaTime.LocalDateTime");
        }
    }
}
