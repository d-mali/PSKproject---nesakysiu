namespace EventPlanningBackend
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using Models;

    public class MainDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Task> Tasks { get; set; }

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
