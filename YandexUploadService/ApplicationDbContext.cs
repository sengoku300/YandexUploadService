using Microsoft.EntityFrameworkCore;
using YandexUploadService.Data.Entities;

namespace YandexUploadService
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Key> Keys { get; set; }

        public ApplicationDbContext()
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=158.160.13.5;Port=5432;Database=testdb;Username=testuser;Password=Jfz5B6KcsV");
        }
    }
}
