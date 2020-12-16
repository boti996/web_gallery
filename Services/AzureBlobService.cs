
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Flurl;
using web_gallery.Models;
using web_gallery.Models.Media;

namespace web_gallery.Services
{
    public class AzureBlobService<DbElement, IDbSettings>
        where DbElement : Models.Model<string>
        where IDbSettings : IBlobStorageSettings
    {
        private readonly string _cdnRootUrl;
        protected readonly BlobContainerClient _container;
        protected readonly BlobClient _blobs;
        public AzureBlobService(IBlobStorageSettings settings, string containerName)
        {
            Debug.WriteLine(
                $"Azure Blob Storage Service getting database: {containerName}"
                + $"from connection: {settings.ConnectionString}"
            );
            _cdnRootUrl = Url.Combine(settings.CdnRoot, containerName);

            var service = new BlobServiceClient(settings.ConnectionString);
            _container = service.GetBlobContainerClient(containerName);
            _blobs = _container.GetBlobClient("sample-blob");

            Debug.WriteLine($"Getting database was successful.");
        }
        public AsyncPageable<BlobItem> GetBlobCatalog()
            => _container.GetBlobsAsync();
        public AsyncPageable<BlobHierarchyItem> GetBlobCatalog(string folderName)
            => _container.GetBlobsByHierarchyAsync(prefix: folderName);
        public async Task<Response<BlobDownloadInfo>> GetBlobs()
            => await _blobs.DownloadAsync();
        public string getCdnUrl(string blobName)
            => Url.Combine(_cdnRootUrl, blobName);

        public async Task<Response<BlobContentInfo>> CreateBlob(MemoryStream file, string fileName, string folderName)
            => await _container.UploadBlobAsync(
                    Url.Combine(folderName, fileName),
                    file
            );
    }

    public class ImageBlobService : AzureBlobService<BlobAlbum, IBlobStorageSettings> {
        public ImageBlobService(IBlobStorageSettings settings) : base(settings, settings.ImageContainerName) { }
    }

    public class VideoBlobService : AzureBlobService<BlobAlbum, IBlobStorageSettings> {
        public VideoBlobService(IBlobStorageSettings settings) : base(settings, settings.VideoContainerName) { }
    }
}