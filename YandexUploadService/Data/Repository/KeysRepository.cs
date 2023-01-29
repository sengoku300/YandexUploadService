using Microsoft.Extensions.Logging;
using YandexUploadService.Data.Entities;
using YandexUploadService.Data.Repository.Base;

namespace YandexUploadService.Data.Repository
{
    public class KeysRepository
    : BaseRepository<Key, ApplicationDbContext>
    {
        public KeysRepository(
            ApplicationDbContext context,
            ILogger<Key> logger
        ) : base(context, logger)
        { }
    }
}
