using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateReferences.Services
{
    class AttachmentFileRepository
    {
        private static long MegaByte = 1024 * 1024;
        private readonly CloudBlobClient _blobClient;

        public AttachmentFileRepository()
        {
            var connectionstring = "";
            var storageAccount = CloudStorageAccount.Parse(connectionstring);
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        //public async Task<string> GetTemporyDownloadUrl(string filename, string containerName, TimeSpan linkDuration)
        //{
        //    var container = await GetOrCreateContainerReferenceAsync(containerName);
        //    var blob = container.GetBlockBlobReference(filename);

        //    var blobExists = await blob.ExistsAsyns();
        //    if (!blobExists)
        //    {
        //        return string.Empty;
        //    }
        //    return GetContainerSharedAccessUrl(blob, linkDuration);
        //}

        private string GetContainerSharedAccessUrl(CloudBlockBlob blob, TimeSpan timeToLive)
        {
            SharedAccessBlobPolicy sharedAccessBlobPolicy = new SharedAccessBlobPolicy();
            sharedAccessBlobPolicy.SharedAccessExpiryTime = DateTime.UtcNow.Add(timeToLive);
            sharedAccessBlobPolicy.Permissions = SharedAccessBlobPermissions.Read;

            var sharedAccessSignature = blob.GetSharedAccessSignature(sharedAccessBlobPolicy);

            return blob.Uri + sharedAccessSignature;
        }

    }
}
