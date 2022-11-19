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
        private OffsetDateTimeHandler()
        {
        }

        public static readonly OffsetDateTimeHandler Default = new OffsetDateTimeHandler();

        public override void SetValue(IDbDataParameter parameter, OffsetDateTime value)
        {
            parameter.Value = value.ToDateTimeOffset();

            if (parameter is SqlParameter sqlParameter)
            {
                sqlParameter.SqlDbType = SqlDbType.DateTimeOffset;
            }
        }

        public override OffsetDateTime Parse(object value)
        {
            if (value is DateTimeOffset dateTimeOffset)
            {
                return OffsetDateTime.FromDateTimeOffset(dateTimeOffset);
            }

            throw new DataException("Cannot convert " + value.GetType() + " to NodaTime.OffsetDateTime");
        }
    }
}
