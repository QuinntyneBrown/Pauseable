using Pauseable.Api.Models;
using Pauseable.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Pauseable.Api.Data
{
    public class PauseableDbContext: DbContext, IPauseableDbContext
    {
        public DbSet<Notification> Notifications { get; private set; }
        public PauseableDbContext(DbContextOptions options)
            :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PauseableDbContext).Assembly);
        }
        
    }
}
