using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace GenerateReferences.Services
{
    class OldService
    {
        string connstr = "";
        string namelbob = "";
        public string Commands(string command)
        {
            switch (command)
            {
                case "test":
                    return "REFERENCE: " + GetSasForBlob(GetCloudBlockBlob(namelbob, "test1", "Read"), DateTime.UtcNow, SharedAccessBlobPermissions.None);
            }

            return "Введена неверная команда";
        }

        private string GetSasForBlob(CloudBlob blob, DateTime expiry, SharedAccessBlobPermissions permissions = SharedAccessBlobPermissions.None)
        {
            var offset = TimeSpan.FromMinutes(1);
            var policy = new SharedAccessBlobPolicy
            {
                SharedAccessStartTime = DateTime.UtcNow.Subtract(offset),
                SharedAccessExpiryTime = expiry.Add(offset),
                Permissions = permissions
            };
            var sas = blob.GetSharedAccessSignature(policy);

            return blob.Uri.ToString() + sas;
        }


        private CloudBlockBlob GetCloudBlockBlob(string containerName, string blobName, string contentType)
        {
            var blobClient = CloudStorageAccount.Parse(connstr).CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            var containerPermissions = new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob };
            container.SetPermissions(containerPermissions);
            var blockBlob = container.GetBlockBlobReference(blobName);
            if (!string.IsNullOrEmpty(contentType)) blockBlob.Properties.ContentType = contentType;
            return blockBlob;
        }

    }
}
