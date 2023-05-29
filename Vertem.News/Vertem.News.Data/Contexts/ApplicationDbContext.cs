using Microsoft.EntityFrameworkCore;
using Vertem.News.Domain.Entities;

namespace Vertem.News.Infra.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Noticia> Noticias { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
