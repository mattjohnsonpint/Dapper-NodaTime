using System;
using System.Data;
using Dapper;
using NodaTime;

namespace AdaskoTheBeAsT.Dapper.NodaTime
{
    public sealed class LocalTimeHandler
        : SqlMapper.TypeHandler<LocalTime>
    {
        public static readonly LocalTimeHandler Default = new();

        private LocalTimeHandler()
        {
        }

        public override void SetValue(IDbDataParameter parameter, LocalTime value)
        {
            parameter.Value = TimeSpan.FromTicks(value.TickOfDay);
            parameter.SetSqlDbType(SqlDbType.Time);
        }

        public override LocalTime Parse(object value)
        {
            if (value is LocalTime localTime)
            {
                return localTime;
            }

            if (value is TimeSpan timeSpan)
            {
                return LocalTime.FromTicksSinceMidnight(timeSpan.Ticks);
            }

            if (value is DateTime dateTime)
            {
                return LocalTime.FromTicksSinceMidnight(dateTime.TimeOfDay.Ticks);
            }

            throw new DataException($"Cannot convert {value.GetType()} to NodaTime.LocalTime");
        }
    }
}
