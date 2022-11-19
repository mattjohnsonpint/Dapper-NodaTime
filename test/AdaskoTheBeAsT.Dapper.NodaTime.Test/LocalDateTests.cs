using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using NodaTime;
using Xunit;

namespace Dapper.NodaTime.Tests
{
    [Collection("DBTests")]
    public class LocalDateTests
    {
        private readonly string _connectionString;

        private class TestObject
        {
            public LocalDate? Value { get; set; }
        }

        public LocalDateTests()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
            SqlMapper.AddTypeHandler(LocalDateHandler.Default);
        }

        [Fact]
        public void Can_Write_And_Read_LocalDate_Stored_As_Date()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject { Value = new LocalDate(1234, 12, 31) };

                const string sql = @"CREATE TABLE #T ([Value] date); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Fact]
        public void Can_Write_And_Read_LocalDate_Stored_As_DateTime()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject { Value = new LocalDate(1753, 12, 31) };

                const string sql = @"CREATE TABLE #T ([Value] datetime); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Fact]
        public void Can_Write_And_Read_LocalDate_Stored_As_DateTime2()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject { Value = new LocalDate(1234, 12, 31) };

                const string sql = @"CREATE TABLE #T ([Value] datetime2); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Fact]
        public void Can_Write_And_Read_LocalDate_With_Null_Value()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var o = new TestObject();

                const string sql = @"CREATE TABLE #T ([Value] date); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
                Assert.Null(t.Value);
            }
        }
    }
}
