using FIAPCloudGames.Promotions.Api.Features.Promotions.Models;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Promotions.Api.Infrastructure.Persistence.Context;

public sealed class PromotionsDbContext : DbContext
{
    public DbSet<Promotion> Promotions { get; set; }

    public PromotionsDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PromotionsDbContext).Assembly);
    }
}
