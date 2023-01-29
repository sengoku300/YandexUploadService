using System.ComponentModel.DataAnnotations;

namespace YandexUploadService.Data.Entities
{

    public class EntityBase
    {
        [Key]
        public long Id { get; set; }

        public virtual void Deatach()
        {
            this.Id = 0;
        }
    }
}
