using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using NodaTime;
using Xunit;

namespace Dapper.NodaTime.Tests
{
    public class LocalTimeTests
    {
        private readonly string _connectionString;

        private class TestObject
        {
            public LocalTime? Value { get; set; }
        }

        public LocalTimeTests()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
            SqlMapper.AddTypeHandler(LocalTimeHandler.Default);
        }

        [Fact]
        public void Can_Write_And_Read_LocalTime_Stored_As_Time()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject {Value = new LocalTime(23, 59, 59, 999)};

                const string sql = @"CREATE TABLE #T ([Value] time); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Fact]
        public void Can_Write_And_Read_LocalTime_Stored_As_DateTime()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject { Value = new LocalTime(23, 59, 59, 333) };

                const string sql = @"CREATE TABLE #T ([Value] datetime); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Fact]
        public void Can_Write_And_Read_LocalTime_Stored_As_DateTime2()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject { Value = new LocalTime(23, 59, 59, 999) };

                const string sql = @"CREATE TABLE #T ([Value] datetime2); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Fact]
        public void Can_Write_And_Read_LocalTime_With_Null_Value()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject();

                const string sql = @"CREATE TABLE #T ([Value] time); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
                Assert.Null(t.Value);
            }
        }
    }
}
