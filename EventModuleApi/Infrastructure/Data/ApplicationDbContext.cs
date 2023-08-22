using Microsoft.EntityFrameworkCore;

namespace EventModuleApi.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Event>? Events { get; set; }

}
