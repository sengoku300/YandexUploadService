using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YandexUploadService.Services.Base
{
    public interface IS3Service
    {
        public Task<ListObjectsResponse> GetListObjectsAsync();

        public Task<GetObjectResponse> GetObjectAsync(string key);
        
        public Task<PutObjectResponse> PutObjectAsync(IFormFile file, string key);

        public Task<List<PutObjectResponse>> PutObjectAsync(Dictionary<string, IFormFile> files);
    }
}
