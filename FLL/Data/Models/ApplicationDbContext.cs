using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace FLL.Data.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Contest> Contests { get; set; } = null!;
        public DbSet<Chrono> Chronos { get; set; } = null!;
    }
}
