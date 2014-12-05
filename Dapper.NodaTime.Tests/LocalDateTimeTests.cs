using System.Data.SqlClient;
using System.Linq;
using NodaTime;
using Xunit;

namespace Dapper.NodaTime.Tests
{
    public class LocalDateTimeTests
    {

        public const string ConnectionString = @"Data Source=(LocalDB)\v11.0;Integrated Security=true;AttachDbFileName=|DataDirectory|\TestDB.mdf";

        public class TestObject
        {
            public LocalDateTime? Value { get; set; }
        }

        public LocalDateTimeTests()
        {
            SqlMapper.AddTypeHandler(LocalDateTimeHandler.Default);
        }

        

        [Fact]
        public void Can_Write_And_Read_LocalDateTime_Stored_As_DateTime2()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var o = new TestObject { Value = new LocalDateTime(1234, 12, 31, 1, 2, 3, 4, 5) };

                const string sql = @"CREATE TABLE #T ([Value] datetime2); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Fact]
        public void Can_Write_And_Read_LocalDateTime_Stored_As_DateTime()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var o = new TestObject { Value = new LocalDateTime(1753, 12, 31, 1, 2, 3, 333) };

                const string sql = @"CREATE TABLE #T ([Value] datetime); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Fact]
        public void Can_Write_And_Read_LocalDateTime_With_Null_Value()
        {
            using (var connection = new SqlConnection(ConnectionString))
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
