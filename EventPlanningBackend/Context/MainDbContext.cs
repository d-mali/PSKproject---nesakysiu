using EventBackend.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventBackend
{
    public class MainDbContext : IdentityDbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventTask> Tasks { get; set; }
        public DbSet<Participant> Participants { get; set; }

        //public DbSet<User> Users { get; set; 

        public string DbPath { get; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "main.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Tasks)
                .WithOne(t => t.Event)
                .OnDelete(DeleteBehavior.Cascade);
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
