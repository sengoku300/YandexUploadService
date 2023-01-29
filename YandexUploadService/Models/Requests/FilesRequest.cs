using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace YandexUploadService.Models.Requests
{
    public class FilesRequest
    {
        public string Email { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
