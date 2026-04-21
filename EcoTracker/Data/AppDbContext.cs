using EcoTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoTracker.AppDbContext;

public class AppDbContext:DbContext
{
    public DbSet<Badges> Badges { get; set; }
    public DbSet<CommuteLogs> CommuteLogs { get; set; }
    public DbSet<Groups> Groups { get; set; }
    public DbSet<TransportModes> TransportModes { get; set; }
    public DbSet<UserBadges> UserBadges { get; set; }
    public DbSet<Users> Users { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }
}