namespace AdaskoTheBeAsT.Dapper.NodaTime.Test
{
    public enum DbVendorLibrary
    {
        MicrosoftSqlServer,
#if NET462_OR_GREATER
        SystemSqlServer,
#endif
    }
}
