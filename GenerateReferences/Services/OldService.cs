using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Shared.Protocol;
using Microsoft.WindowsAzure.Storage.File;

namespace GenerateReferences.Services
{
    class OldService
    {
        string conn = @"";
        string namecontainer = "";
        //string namecontainer = "backupexport";
        string localfile = "";


        public string Commands(string command)
        {
            switch (command)
            {
                case "new reference":
                    return "REFERENCE: " + GetBlobSasUri(getblob(), "SuperTest");

                case "create":
                    var container = CreatedContainer();
                    return "REFERENCE: ";
            }

            return "Введена неверная команда";
        }


        private static string GetBlobSasUri(CloudBlobContainer container, string blobName, string policyName = null)
        {
            string sasBlobToken;
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);

            if (policyName == null)
            {
                SharedAccessBlobPolicy adHocSAS = new SharedAccessBlobPolicy()
                {
                    SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                    Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Create
                };
                sasBlobToken = blob.GetSharedAccessSignature(adHocSAS);

                Console.WriteLine("SAS for blob (ad hoc): {0}", sasBlobToken);
                Console.WriteLine();
            }
            else
            {
                sasBlobToken = blob.GetSharedAccessSignature(null, policyName);

                Console.WriteLine("SAS for blob (stored access policy): {0}", sasBlobToken);
                Console.WriteLine();
            }

            return blob.Uri + sasBlobToken;
        }



        private string CreatedContainer()
        {
            try
            {
                // Create Reference to Azure Storage Account
                CloudStorageAccount storageacc = CloudStorageAccount.Parse(conn);
                //Create Reference to Azure Blob
                CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();
                blobClient.AuthenticationScheme = AuthenticationScheme.Token;
                //The next 2 lines create if not exists a container named "democontainer"
                CloudBlobContainer container = blobClient.GetContainerReference(namecontainer);
                container.CreateIfNotExists();
                //The next 7 lines upload the file test.txt with the name DemoBlob on the container "democontainer"
                CloudBlockBlob blockBlob = container.GetBlockBlobReference("SuperTest");
                using (var filestream = System.IO.File.OpenRead(localfile))
                {
                    blockBlob.UploadFromStream(filestream);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        private CloudBlobContainer getblob()
        {
            var blobClient = CloudStorageAccount.Parse(conn).CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(namecontainer);
            var containerPermissions = new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob };
            container.SetPermissions(containerPermissions);
            return container;
        }

    }
}
