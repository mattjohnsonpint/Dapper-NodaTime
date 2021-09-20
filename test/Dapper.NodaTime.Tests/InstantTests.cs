using System.Data.SqlClient;
using System.Linq;
using NodaTime;
using Xunit;

namespace Dapper.NodaTime.Tests
{
    [Collection("DBTests")]
    public class InstantTests
    {
        private readonly string _connectionString;

        private class TestObject
        {
            public Instant? Value { get; set; }
        }

        public InstantTests(DatabaseFixture databaseFixture)
        {
            _connectionString = databaseFixture.ConnectionString;
            SqlMapper.AddTypeHandler(InstantHandler.Default);
        }

        [Fact]
        public void Can_Write_And_Read_Instant_Stored_As_DateTimeOffset()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject { Value = Instant.FromUtc(1234, 12, 31, 1, 2, 3).PlusTicks(1234567) };

                const string sql = @"CREATE TABLE #T ([Value] datetimeoffset); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Fact]
        public void Can_Write_And_Read_Instant_Stored_As_DateTime2()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject { Value = Instant.FromUtc(1234, 12, 31, 1, 2, 3).PlusTicks(1234567) };

                const string sql = @"CREATE TABLE #T ([Value] datetime2); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Fact]
        public void Can_Write_And_Read_Instant_Stored_As_DateTime()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject { Value = Instant.FromUtc(1753, 12, 31, 1, 2, 3).PlusTicks(3330000) };

                const string sql = @"CREATE TABLE #T ([Value] datetime); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Fact]
        public void Can_Write_And_Read_Instant_With_Null_Value()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject();

                const string sql = @"CREATE TABLE #T ([Value] datetime2); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
                Assert.Null(t.Value);
            }
        }
    }
}
