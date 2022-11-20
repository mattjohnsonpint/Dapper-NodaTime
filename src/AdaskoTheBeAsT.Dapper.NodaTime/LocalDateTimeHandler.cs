using System;
using System.Data;
using Dapper;
using NodaTime;

namespace AdaskoTheBeAsT.Dapper.NodaTime
{
    public sealed class LocalDateTimeHandler
        : SqlMapper.TypeHandler<LocalDateTime>
    {
        public static readonly LocalDateTimeHandler Default = new();

        private LocalDateTimeHandler()
        {
        }

        public override void SetValue(IDbDataParameter parameter, LocalDateTime value)
        {
            parameter.Value = value.ToDateTimeUnspecified();
            parameter.SetSqlDbType(SqlDbType.DateTime2);
        }

        public override LocalDateTime Parse(object value)
        {
            if (value is LocalDateTime localDateTime)
            {
                return localDateTime;
            }

            if (value is DateTime dateTime)
            {
                return LocalDateTime.FromDateTime(dateTime);
            }

            throw new DataException($"Cannot convert {value.GetType()} to NodaTime.LocalDateTime");
        }
    }
}
