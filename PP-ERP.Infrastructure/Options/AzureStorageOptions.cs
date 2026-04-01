namespace PP_ERP.Infrastructure.Options
{
    public class AzureStorageOptions
    {
        public const string SectionName = "AzureStorage";

        public string ConnectionString { get; set; } = string.Empty;
        public string PublicContainer { get; set; } = string.Empty;
        public string PrivateContainer { get; set; } = string.Empty;
        public string StorageUrl { get; set; } = string.Empty;
        public string CdnUrl { get; set; } = string.Empty;
        public bool UseCdn { get; set; }
    }
}
