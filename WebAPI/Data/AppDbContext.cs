using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;

namespace WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}