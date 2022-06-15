using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Sas;
using Azure.Storage.Blobs.Specialized;

namespace GenerateReferences.Services
{
    public class Service
    {
        string conn = "DefaultEndpointsProtocol=https;AccountName=backupmasterdevversion1;AccountKey=NnRCXPcqzUiWhu/Exe4WAXJ20f0THwMdr8yblgR2WNs68FRq/KYhUHvdj4vBYkYTmk6+W79Vkgkv6R41zytjAw==;EndpointSuffix=core.windows.net";

        public string Commands(string command)
        {
            switch (command)
            {
                case "new reference":
                    return GetServiceSasUriForBlob(newblob());
            }

            return "Введена неверная команда";
        }






        private BlobClient newblob()
        {
            BlobClient blb = new BlobClient(conn, "backupexport", "SuperTest");
            return blb;
        }

        private static string GetServiceSasUriForBlob(BlobClient blobClient, string storedPolicyName = null)
        {
            if (blobClient.CanGenerateSasUri)
            {
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                    BlobName = blobClient.Name,
                    Resource = "b"
                };

                if (storedPolicyName == null)
                {
                    sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(1);
                    sasBuilder.SetPermissions(BlobSasPermissions.Read |
                                   BlobSasPermissions.Write);
                }
                else
                {
                    sasBuilder.Identifier = storedPolicyName;
                }

                Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
                Console.WriteLine("SAS URI для blob is: {0}", sasUri);
                Console.WriteLine();

                return sasUri.ToString();
            }
            else
            {
                return  @"BlobClient должен быть авторизован с помощью учетных данных общего ключа для создания службы SAS";
            }
        }
    }
}
