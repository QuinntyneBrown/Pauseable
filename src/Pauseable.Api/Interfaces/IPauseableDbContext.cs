using Pauseable.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Pauseable.Api.Interfaces
{
    public interface IPauseableDbContext
    {
        DbSet<Notification> Notifications { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
