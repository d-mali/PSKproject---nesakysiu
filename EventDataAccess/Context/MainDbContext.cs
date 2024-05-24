using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using EventDataAccess.Context;
using EventDomain.Entities;

namespace EventPlanningBackend
{
    public class MainDbContext : DbContext, IMainDbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventDomain.Entities.Task> Tasks { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbContext Instance => this;

        public string DbPath { get; }

        public MainDbContext()
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
