namespace YandexUploadService.Data.Entities
{
    public class S3Settings
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string ServiceUrl { get; set; }
        public string BucketName { get; set; }
    }
}
