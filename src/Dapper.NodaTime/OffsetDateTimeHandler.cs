using System;
using System.Data;
using NodaTime;

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
            parameter.SetSqlDbType(SqlDbType.DateTimeOffset);
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
