using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiCiCd.Models;

namespace WebApiCiCd
{
    // Контекст БД для демонстрации
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
