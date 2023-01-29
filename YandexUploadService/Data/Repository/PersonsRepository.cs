using Microsoft.Extensions.Logging;
using YandexUploadService.Data.Entities;
using YandexUploadService.Data.Repository.Base;

namespace YandexUploadService.Data.Repository
{
    public class PersonsRepository
    : BaseRepository<Person, ApplicationDbContext>
    {
        public PersonsRepository(
            ApplicationDbContext context,
            ILogger<Person> logger
        ) : base(context, logger)
        { }
    }
}
