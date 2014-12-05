using System;
using System.Data;
using System.Data.SqlClient;
using NodaTime;

namespace Dapper.NodaTime
{
    public class InstantHandler : SqlMapper.TypeHandler<Instant>
    {
        protected InstantHandler()
        {
        }

        public static readonly InstantHandler Default = new InstantHandler();

        public override void SetValue(IDbDataParameter parameter, Instant value)
        {
            parameter.Value = value.ToDateTimeUtc();

            var sqlParameter = parameter as SqlParameter;
            if (sqlParameter != null)
            {
                sqlParameter.SqlDbType = SqlDbType.DateTime2;
            }
        }

        public override Instant Parse(object value)
        {
            if (value is DateTime)
            {
                var dt = DateTime.SpecifyKind((DateTime) value, DateTimeKind.Utc);
                return Instant.FromDateTimeUtc(dt);
            }

            if (value is DateTimeOffset)
            {
                return Instant.FromDateTimeOffset((DateTimeOffset)value);
            }

            throw new DataException("Cannot convert " + value.GetType() + " to NodaTime.Instant");
        }
    }
}
