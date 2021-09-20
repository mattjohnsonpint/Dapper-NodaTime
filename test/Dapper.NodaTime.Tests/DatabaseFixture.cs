using System;
using System.Threading.Tasks;
using LocalDb;
using Xunit;

namespace Dapper.NodaTime.Tests
{
    [CollectionDefinition("DBTests")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
        // See https://xunit.net/docs/shared-context#collection-fixture
    }

    public class DatabaseFixture : IDisposable
    {
        private readonly SqlInstance _sqlInstance;
        private readonly SqlDatabase _database;

        public DatabaseFixture()
        {
            _sqlInstance = new SqlInstance(name: "Dapper.NodaTime.Tests", buildTemplate: _ => Task.CompletedTask);
            _database = _sqlInstance.Build("DapperNodaTimeTests").Result;
        }

        public void Dispose()
        {
            _database.Dispose();
            _sqlInstance.Cleanup();
        }

        public string ConnectionString => _database.ConnectionString;
    }
}