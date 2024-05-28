using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;

namespace Scheduler.Infra;

public class RoomsContext : DbContext
{
    public RoomsContext(DbContextOptions<RoomsContext> options)
        : base(options)
    { }

    public DbSet<Room> Rooms { get; set; }
}