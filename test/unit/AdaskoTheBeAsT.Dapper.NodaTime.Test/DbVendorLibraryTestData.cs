using System.Collections;
using System.Collections.Generic;

namespace AdaskoTheBeAsT.Dapper.NodaTime.Test
{
    public sealed class DbVendorLibraryTestData
        : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { DbVendorLibrary.MicrosoftSqlServer };
#if NET462_OR_GREATER
            yield return new object[] { DbVendorLibrary.SystemSqlServer };
#endif
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
