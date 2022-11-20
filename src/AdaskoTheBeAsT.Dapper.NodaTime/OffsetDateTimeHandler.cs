using System;
using System.Data;
using Dapper;
using NodaTime;

namespace AdaskoTheBeAsT.Dapper.NodaTime
{
    public sealed class OffsetDateTimeHandler
        : SqlMapper.TypeHandler<OffsetDateTime>
    {
        public static readonly OffsetDateTimeHandler Default = new();

        private OffsetDateTimeHandler()
        {
        }

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

            throw new DataException($"Cannot convert {value.GetType()} to NodaTime.OffsetDateTime");
        }
    }
}
