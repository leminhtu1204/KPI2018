using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;

namespace LunchManagement.BlobStorage
{
    public class BlobStorageService : IBlobStorageService
    {
        private IConfiguration configuration;
        private readonly string blob_ContainerName;
        private readonly string blob_StorageAccount;
        private readonly string blob_StorageKey;

        public BlobStorageService(IConfiguration configuration)
        {
            this.configuration = configuration;
            blob_ContainerName = configuration.GetValue<string>("Blob_ContainerName");
            blob_StorageAccount = configuration.GetValue<string>("Blob_StorageAccount");
            blob_StorageKey = configuration.GetValue<string>("Blob_StorageKey");
        }
        public async Task<CloudBlobContainer> GetCloudBlobContainerAsync()
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(blob_StorageAccount, blob_StorageKey), false);

            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(blob_ContainerName);
            try
            {
                await container.CreateIfNotExistsAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
            

            return container;
        }

        public async Task<CloudBlockBlob> GetCloudBlockBlobAsync(string blobName)
        {
            var blobContainer = await this.GetCloudBlobContainerAsync();

            //Blob
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);

            return blockBlob;
        }
    }
}
