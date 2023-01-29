using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using YandexUploadService.Data.Entities;
using YandexUploadService.Services.Base;

namespace YandexUploadService.Services
{
    public class S3Service : IS3Service
    {
        private readonly S3Settings _config;

        private readonly AmazonS3Config _amazonConfig;
        private readonly AWSCredentials _amazonCreds;
        private readonly AmazonS3Client _amazonClient;

        public S3Service(IConfiguration config)
        {
            _config = config.GetSection("S3Settings").Get<S3Settings>();

            _amazonConfig = new AmazonS3Config()
            {
                ServiceURL = _config.ServiceUrl,
                UseHttp = true,
                ForcePathStyle = true
            };

            _amazonCreds = new BasicAWSCredentials(_config.AccessKey, _config.SecretKey);
            _amazonClient = new AmazonS3Client(_amazonCreds, _amazonConfig);
        }

        public async Task<ListObjectsResponse> GetListObjectsAsync()
        {
            try
            {
                return await _amazonClient.ListObjectsAsync(_config.BucketName);
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public async Task<GetObjectResponse> GetObjectAsync(string key)
        {

            try
            {
                return await _amazonClient.GetObjectAsync(_config.BucketName, key);
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public async Task<PutObjectResponse> PutObjectAsync(IFormFile file, String key)
        {
            try
            {
                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = _config.BucketName,
                    Key = key,
                    InputStream = file.OpenReadStream(),
                    ContentType = file.ContentType,
                    CalculateContentMD5Header = true,
                };

                PutObjectResponse response = await _amazonClient.PutObjectAsync(request);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                    return response;
            }
            catch (Exception)
            {

            }

            return null;
        }

        public async Task<List<PutObjectResponse>> PutObjectAsync(Dictionary<String,IFormFile> files)
        {
            try
            {
                List<PutObjectResponse> responses = new List<PutObjectResponse>();

                foreach (var file in files)
                {
                    PutObjectRequest request = new PutObjectRequest
                    {
                        BucketName = _config.BucketName,
                        Key = file.Key,
                        InputStream = file.Value.OpenReadStream(),
                        ContentType = file.Value.ContentType,
                        CalculateContentMD5Header = true,
                    };

                    PutObjectResponse response = await _amazonClient.PutObjectAsync(request);
                    responses.Add(response);
                }

                return responses;
            }
            catch (Exception)
            {

            }

            return null;
        }
    }
}
