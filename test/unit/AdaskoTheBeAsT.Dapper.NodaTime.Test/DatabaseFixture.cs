using System.Threading.Tasks;
using LocalDb;
using Xunit;

namespace AdaskoTheBeAsT.Dapper.NodaTime.Test
{
    public sealed class DatabaseFixture
        : IAsyncLifetime
    {
#pragma warning disable IDISP006 // Implement IDisposable
        private SqlInstance? _sqlInstance;
        private SqlDatabase? _database;
#pragma warning restore IDISP006 // Implement IDisposable

        public string ConnectionString => _database?.ConnectionString ?? string.Empty;

        public async Task InitializeAsync()
        {
            _sqlInstance = new SqlInstance(name: "Dapper.NodaTime.Tests", buildTemplate: _ => Task.CompletedTask);

#if NET6_0_OR_GREATER
            if (_database is not null)
            {
                await _database.DisposeAsync().ConfigureAwait(false);
            }
#else
            _database?.Dispose();
#endif
            _database = await _sqlInstance.Build("DapperNodaTimeTests").ConfigureAwait(false);
        }

#if NET6_0_OR_GREATER
        public async Task DisposeAsync()
#else
        public Task DisposeAsync()
#endif
        {
#if NET6_0_OR_GREATER
            if (_database is not null)
            {
                await _database.DisposeAsync().ConfigureAwait(false);
            }
#else
            _database?.Dispose();
#endif
            _sqlInstance?.Cleanup();
#if NET6_0_OR_GREATER

#else
            return Task.CompletedTask;
#endif
        }
    }
}
