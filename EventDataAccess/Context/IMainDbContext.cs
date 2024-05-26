using EventDomain.Entities;
using EventPlanningBackend;
using Microsoft.EntityFrameworkCore;

namespace EventDataAccess.Context
{
    public interface IMainDbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventTask> Tasks { get; set; }
        public DbSet<Participant> Participants { get; set; }
        //public DbSet<User> Users { get; set; }
        MainDbContext Instance { get; }
    }
}
