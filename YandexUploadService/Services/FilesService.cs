using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using YandexUploadService.Helpers;
using YandexUploadService.Services.Base;

namespace YandexUploadService.Services
{
    public class FilesService : IFilesService
    {
        public Dictionary<string, IFormFile> GenerateDictionaryKeyFiles(IEnumerable<IFormFile> files)
        {
            if (files == null) return null;
            if (files.Count() == 0) return null;

            Dictionary<string, IFormFile> keysFiles = new Dictionary<string, IFormFile>();

            foreach (var f in files)
            {
                String key = KeyHelper.GenerateKey(f.FileName);
                keysFiles.Add(key, f);
            }

            return keysFiles;
        }
    }
}
