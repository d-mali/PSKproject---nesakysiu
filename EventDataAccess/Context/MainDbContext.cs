using EventDataAccess.Context;
using EventDomain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventPlanningBackend
{
    public class MainDbContext : IdentityDbContext, IMainDbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventTask> Tasks { get; set; }
        public DbSet<Participant> Participants { get; set; }

        //public DbSet<User> Users { get; set; }
        public MainDbContext Instance => this;

        public string DbPath { get; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "main.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
