using ApiCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiCQRS.Infrastructure.Context
{
    public class AppDbMysqlContext : DbContext
    {
        public AppDbMysqlContext(DbContextOptions<AppDbMysqlContext> options) : base(options)
        { }

        public DbSet<User> User { get; set; }
    }
}
