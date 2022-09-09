using MAUIMiniAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MAUIMiniAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {

        }
        public DbSet<ToDo> ToDos => Set<ToDo>();
    }
}
