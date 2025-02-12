using AdR.Models;
using Microsoft.EntityFrameworkCore;

namespace AdR.DatabaseContext
{
    public class AdrDbContext(DbContextOptions<AdrDbContext> options): DbContext(options)
    {
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Nota> Nota { get; set; }
    }
}
