using System;
using System.Data;
using System.Data.SqlClient;
using NodaTime;

#if NETSTANDARD1_3
using DataException = System.InvalidOperationException;
#endif

namespace Dapper.NodaTime
{
    public class OffsetDateTimeHandler : SqlMapper.TypeHandler<OffsetDateTime>
    {
        protected OffsetDateTimeHandler()
        {
        }

        public static readonly OffsetDateTimeHandler Default = new OffsetDateTimeHandler();

        public override void SetValue(IDbDataParameter parameter, OffsetDateTime value)
        {
            parameter.Value = value.ToDateTimeOffset();

            var sqlParameter = parameter as SqlParameter;
            if (sqlParameter != null)
            {
                sqlParameter.SqlDbType = SqlDbType.DateTimeOffset;
            }
        }

        public override OffsetDateTime Parse(object value)
        {
            if (value is DateTimeOffset)
            {
                return OffsetDateTime.FromDateTimeOffset((DateTimeOffset)value);
            }

            throw new DataException("Cannot convert " + value.GetType() + " to NodaTime.OffsetDateTime");
        }
    }
}
