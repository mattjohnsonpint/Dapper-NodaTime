using System;
using System.Data;
using System.Data.SqlClient;
using NodaTime;

#if NETSTANDARD1_3
using DataException = System.InvalidOperationException;
#endif

namespace Dapper.NodaTime
{
    public class InstantHandler : SqlMapper.TypeHandler<Instant>
    {
        private InstantHandler()
        {
        }

        public static readonly InstantHandler Default = new InstantHandler();

        public override void SetValue(IDbDataParameter parameter, Instant value)
        {
            parameter.Value = value.ToDateTimeUtc();

            if (parameter is SqlParameter sqlParameter)
            {
                sqlParameter.SqlDbType = SqlDbType.DateTime2;
            }
        }

        public override Instant Parse(object value)
        {
            if (value is DateTime dateTime)
            {
                var dt = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                return Instant.FromDateTimeUtc(dt);
            }

            if (value is DateTimeOffset dateTimeOffset)
            {
                return Instant.FromDateTimeOffset(dateTimeOffset);
            }

            throw new DataException("Cannot convert " + value.GetType() + " to NodaTime.Instant");
        }
    }
}
