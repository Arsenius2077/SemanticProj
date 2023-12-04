using Microsoft.EntityFrameworkCore;
using NastyaWebApi.Model;

namespace NastyaWebApi.Data
{
    public class DBContext : DbContext
    {
        public DBContext()
        {

        }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<UniverityModel> univerityModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UniverityModel>().Property(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }

    }
}
