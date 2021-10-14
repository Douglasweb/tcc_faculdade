using ApiCQRS.Domain.Entities;
using ApiCQRS.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ApiCQRS.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
