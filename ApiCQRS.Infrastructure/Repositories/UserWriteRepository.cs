using ApiCQRS.Domain.Entities;
using ApiCQRS.Domain.Interfaces;
using ApiCQRS.Infrastructure.Context;

namespace ApiCQRS.Infrastructure.Repositories
{
    public class UserWriteRepository : IUserWriteRepository
    {
        protected readonly AppDbContext db;
        public UserWriteRepository(AppDbContext context)
        {
            db = context;
        }
        
        public void Add(User user)
        {
            db.User.Add(user);
            db.SaveChanges();
        }
        public void Update(User user)
        {
            db.User.Update(user);
            db.SaveChanges();
        }
        public void Remove(User user)
        {
            db.User.Remove(user);
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
