using $safeprojectname$.Models;
using Microsoft.EntityFrameworkCore;

namespace $safeprojectname$.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
        : base(options)
        {
        }
        public DbSet<GridModel> Grid { get; set; }
    }
}
