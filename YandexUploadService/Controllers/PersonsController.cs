using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using YandexUploadService.Models.Requests;
using Microsoft.Extensions.Logging;
using YandexUploadService.Services.Base;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using System.Net;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using YandexUploadService.Data.Entities;
using Amazon.Runtime.Internal;
using System.Buffers.Text;
using YandexUploadService.Data.Repository.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using System.Text;
using Key = YandexUploadService.Data.Entities.Key;
using YandexUploadService.Helpers;

namespace YandexUploadService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ILogger<PersonsController> logger;
        private readonly IS3Service _s3Service;
        private readonly IFilesService _filesService;
        private readonly IDataRepository<Person> _repository;

        public PersonsController(ILogger<PersonsController> logger, IS3Service s3Service, IFilesService filesService, IConfiguration configuration, IDataRepository<Person> repository)
        {
            this.logger = logger;
            _s3Service = s3Service;
            _repository = repository;
            _filesService = filesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _s3Service.GetListObjectsAsync();

            if (response == null) return BadRequest();

            return Ok(response);
        }

        [HttpGet]
        [Route("byKey")]
        public async Task<IActionResult> GetByKey(String key)
        {
            var response = await _s3Service.GetObjectAsync(key);

            if (response.ResponseStream == null)
                return NotFound(); // returns a NotFoundResult with Status404NotFound response.

            String contentType = response.Headers.ContentType.Replace("application_", ".");
            String fileName = string.Concat(key, contentType);

            return File(response.ResponseStream, "application/octet-stream", fileName); // returns a FileStreamResult
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] FilesRequest request)
        {
            var keysFiles = _filesService.GenerateDictionaryKeyFiles(request.Files);

            if(keysFiles == null) return BadRequest();

            var response = await _s3Service.PutObjectAsync(keysFiles);

            if (response == null) return BadRequest();

            IEnumerable<Person> persons = PersonsHelper.GeneratePersons(keysFiles.Keys, request.Email);

            _repository.AddRange(persons);

            return Ok(response);
        }
    }
}
