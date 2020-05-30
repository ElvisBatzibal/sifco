
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace webapiapp.Class
{
    public class StorageService
    {
        private static StorageService _instance;
        public static StorageService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StorageService();
                return _instance;
            }
        }

        /// <summary>
        /// Gets a reference to the container for storing the images
        /// </summary>
        /// <returns></returns>
        private static CloudBlobContainer GetContainer()
        {
            // Parses the connection string for the WindowS Azure Storage Account
            var account = CloudStorageAccount.Parse(StorageConfiguration.StorageConnectionString);
            var client = account.CreateCloudBlobClient();

            // Gets a reference to the images container
            var container = client.GetContainerReference("appbatzimages");

            return container;
        }
        /// <summary>
        /// Uploads a new file to a blob container.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> UploadFile(Stream file, string MediaType)
        {
            var container = GetContainer();

            // Creates the container if it does not exist
            //await container.CreateIfNotExistsAsync();

            // Uses a random name for the new images
            // var name = RandomString(10);
            var name = Guid.NewGuid().ToString() + "" + MediaType;
            try
            {        // Uploads the image the blob storage
                var imageBlob = container.GetBlockBlobReference(name);
                switch (MediaType.ToLower())
                {
                    case ".jpg":
                        imageBlob.Properties.ContentType = "image/jpeg";
                        break;
                    case ".jpeg":
                        imageBlob.Properties.ContentType = "image/jpeg";
                        break;
                    case ".png":
                        imageBlob.Properties.ContentType = "image/png";
                        break;
                    case ".gif":
                        imageBlob.Properties.ContentType = "image/gif";
                        break;
                    case ".pdf":
                        imageBlob.Properties.ContentType = "application/pdf";
                        break;
                    case ".json":
                        imageBlob.Properties.ContentType = "application/json";
                        break;
                    case ".js":
                        imageBlob.Properties.ContentType = "application/javascript";
                        break;
                    case ".svg":
                        imageBlob.Properties.ContentType = "image/svg+xml";
                        break;             
                    default:
                        break;
                }


                await imageBlob.UploadFromStreamAsync(file);
            }
            catch (Exception ex)
            {
                var exeption = ex.Message;
                throw;
            }


            return name;
        }
        /// <summary>
        /// Gets an image from the blob container using the name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<byte[]> Getfile(string name)
        {
            var container = GetContainer();

            //Gets the block blob representing the image
            var blob = container.GetBlobReference(name);

            if (await blob.ExistsAsync())
            {
                // Gets the block blob length to initialize the array in memory
                await blob.FetchAttributesAsync();

                byte[] blobBytes = new byte[blob.Properties.Length];

                // Downloads the block blob and stores the content in an array in memory
                await blob.DownloadToByteArrayAsync(blobBytes, 0);

                return blobBytes;
            }

            return null;
        }

        /// <summary>
        /// Generates a random string
        /// </summary>
        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
