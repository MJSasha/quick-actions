using Microsoft.EntityFrameworkCore;
using Sample.Common.Models;

namespace Sample.Api
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);

            modelBuilder.Entity<User>().HasData
            (
                new User { Id = 1, Name = "Andriy", Email = "Andriy@mail", Phone = "123" },
                new User { Id = 2, Name = "Sam", Email = "Sam@mail", Phone = "456" },
                new User { Id = 3, Name = "Sasha", Email = "Sasha@mail", Phone = "789" },
                new User { Id = 4, Name = "Nick", Email = "Nick@mail", Phone = "791" }
            );
        }
    }
}
