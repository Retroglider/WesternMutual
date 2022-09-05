using Microsoft.EntityFrameworkCore;

using WesternMutual_RhyssLeary.Domain;

namespace WesternMutual_RhyssLeary.Repository;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    #region Entities that need to be persisted
    public DbSet<Entry> Entries { get; set; } = default!;
    #endregion
}
