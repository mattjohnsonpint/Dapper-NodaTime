using System.Data.SqlClient;
using System.Linq;
using NodaTime;
using Xunit;

namespace Dapper.NodaTime.Tests
{
    [Collection("DBTests")]
    public class OffsetDateTimeTests
    {
        private readonly string _connectionString;

        public class TestObject
        {
            public OffsetDateTime? Value { get; set; }
        }

        public OffsetDateTimeTests(DatabaseFixture databaseFixture)
        {
            _connectionString = databaseFixture.ConnectionString;
            SqlMapper.AddTypeHandler(OffsetDateTimeHandler.Default);
        }

        [Fact]
        public void Can_Write_And_Read_OffsetDateTime_Stored_As_DateTimeOffset()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject
                {
                    Value = new OffsetDateTime(
                        new LocalDateTime(1234, 1, 2, 3, 4, 5, 6),
                        Offset.FromHoursAndMinutes(-5, -30))
                };

                const string sql = @"CREATE TABLE #T ([Value] datetimeoffset); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Fact]
        public void Can_Write_And_Read_OffsetDateTime_With_Null_Value()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject();

                const string sql = @"CREATE TABLE #T ([Value] datetimeoffset); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
                Assert.Null(t.Value);
            }
        }
    }
}
