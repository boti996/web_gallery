namespace web_gallery.Models
{
    public interface IBlobStorageSettings
    {
        string ConnectionString { get; set; }
        string ImageContainerName { get; set; }
        string VideoContainerName { get; set; }
        string CdnRoot { get; set; }
    }
    public class BlobStorageSettings : IBlobStorageSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string ImageContainerName { get; set; } = null!;
        public string VideoContainerName { get; set; } = null!;
        public string CdnRoot { get; set; } = null!;
    }
}