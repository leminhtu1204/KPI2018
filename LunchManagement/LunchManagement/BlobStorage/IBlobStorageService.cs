using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchManagement.BlobStorage
{
    public interface IBlobStorageService
    {
        Task<CloudBlobContainer> GetCloudBlobContainerAsync();

        Task<CloudBlockBlob> GetCloudBlockBlobAsync(string blobName);
    }
}
