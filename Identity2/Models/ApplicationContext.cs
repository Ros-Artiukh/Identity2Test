using Identity2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity2.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Developer> Developers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                 .Entity<Developer>()
                 .HasData(new Developer { Id = 1, Name = "Billy" }, new Developer { Id = 2, Name = "Van" });

            modelBuilder
                .Entity<Project>()
                .HasData(new Project { Id = 1, Client = "Dungeon Master", Name = "Gym" });

            modelBuilder
                .Entity<Developer>()
                .HasMany(p => p.Projects)
                .WithMany(p => p.Developers)
                .UsingEntity(j => j.HasData(new { DevelopersId = 1, ProjectsId = 1 }, new { DevelopersId = 2, ProjectsId = 1 }));
        }
    }
}
