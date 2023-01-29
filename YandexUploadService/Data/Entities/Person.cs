using System;

namespace YandexUploadService.Data.Entities
{
    public class Person : EntityBase
    {
        public String Email { get; set; }

        public Key Key { get; set; }
    }
}
