using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Infrastructure.Persistence.Context;

public class SozlukDboContext : DbContext
{
    public const string DEFAULT_SCHEMA = "dbo";

    public SozlukDboContext()
    {

    }
    public SozlukDboContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connStr = @"Server=SAMETSPC;Database=SozlukDbo;Trusted_Connection=true;TrustServerCertificate=True";

            optionsBuilder.UseSqlServer(connStr);
        }
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Entry> Entries { get; set; }

    public DbSet<EntryVote> EntryVotes { get; set; }
    public DbSet<EntryFavorite> EntryFavorites { get; set; }



    public DbSet<EntryComment> EntryComments { get; set; }
    public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }
    public DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }

    public DbSet<EmailConfirmation> EmailConfirmations { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }

    public override int SaveChanges()
    {
        OnBeforeSave();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSave()
    {
        var addedEntites = ChangeTracker.Entries()
        .Where(i => i.State == EntityState.Added)
        .Select(i => (BaseEntity)i.Entity);

        PrepareAddedEntites(addedEntites);
    }

    private void PrepareAddedEntites(IEnumerable<BaseEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.CreateDate == DateTime.MinValue)
                entity.CreateDate = DateTime.Now;
        }
    }
}
