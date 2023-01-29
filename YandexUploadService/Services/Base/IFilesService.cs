using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YandexUploadService.Services.Base
{
    public interface IFilesService
    {
        public Dictionary<string, IFormFile> GenerateDictionaryKeyFiles(IEnumerable<IFormFile> files);
    }
}
