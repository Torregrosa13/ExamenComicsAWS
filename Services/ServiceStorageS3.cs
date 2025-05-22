using Amazon.S3;
using Amazon.S3.Model;
using System.Net;

namespace ExamenComicsAWS.Services
{
    public class ServiceStorageS3
    {
        private string BucketName;

        private IAmazonS3 ClientS3;

        public ServiceStorageS3(IConfiguration configuration
        , IAmazonS3 clientS3) {
            this.BucketName = configuration.GetValue<string>
            ("AWS:BucketName");
            this.ClientS3 = clientS3;
        }

        public async Task<bool> UploadFileAsync
        (string fileName, Stream stream) {
            PutObjectRequest request = new PutObjectRequest
            {
                Key = fileName,
                BucketName = this.BucketName,
                InputStream = stream
            };
            //PARA TRABAJAR SE UTILIZA LA CLASE IAmazonS3 
            //CON UNA PETICION DE PUTOBJECT
            PutObjectResponse response = await
    this.ClientS3.PutObjectAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK) {
                return true;
            }
            else {
                return false;
            }
        }

    }
}
