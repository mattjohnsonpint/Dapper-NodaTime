using System;
using System.Data;
using Dapper;
using NodaTime;

namespace AdaskoTheBeAsT.Dapper.NodaTime
{
    public sealed class LocalDateHandler
        : SqlMapper.TypeHandler<LocalDate>
    {
        public static readonly LocalDateHandler Default = new();

        private LocalDateHandler()
        {
        }

        public override void SetValue(IDbDataParameter parameter, LocalDate value)
        {
            parameter.Value = value.AtMidnight().ToDateTimeUnspecified();
            parameter.SetSqlDbType(SqlDbType.Date);
        }

        public override LocalDate Parse(object value)
        {
            if (value is LocalDate localDate)
            {
                return localDate;
            }

            if (value is DateTime dateTime)
            {
                return LocalDateTime.FromDateTime(dateTime).Date;
            }

            throw new DataException($"Cannot convert {value.GetType()} to NodaTime.LocalDate");
        }
    }
}
