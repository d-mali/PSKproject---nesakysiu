using EventDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDataAccess.Context
{
    public interface IMainDbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventDomain.Entities.Task> Tasks { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<User> Users { get; set; }
        DbContext Instance { get; }
    }
}
