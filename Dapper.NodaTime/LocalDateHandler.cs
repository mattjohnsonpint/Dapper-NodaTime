using System;
using System.Data;
using System.Data.SqlClient;
using NodaTime;

namespace Dapper.NodaTime
{
    public class LocalDateHandler : SqlMapper.TypeHandler<LocalDate>
    {
        protected LocalDateHandler()
        {
        }

        public static readonly LocalDateHandler Default = new LocalDateHandler();

        public override void SetValue(IDbDataParameter parameter, LocalDate value)
        {
            parameter.Value = value.AtMidnight().ToDateTimeUnspecified();

            var sqlParameter = parameter as SqlParameter;
            if (sqlParameter != null)
            {
                sqlParameter.SqlDbType = SqlDbType.Date;
            }
        }

        public override LocalDate Parse(object value)
        {
            if (value is DateTime)
            {
                return LocalDateTime.FromDateTime((DateTime) value).Date;
            }

            throw new DataException("Cannot convert " + value.GetType() + " to NodaTime.LocalDate");
        }
    }
}
