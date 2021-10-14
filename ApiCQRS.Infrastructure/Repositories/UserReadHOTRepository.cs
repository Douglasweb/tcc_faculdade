using ApiCQRS.Domain.Entities;
using ApiCQRS.Domain.Interfaces;
using ApiCQRS.Infrastructure.Context;
using System;
using System.Threading.Tasks;

namespace ApiCQRS.Infrastructure.Repositories
{
    public class UserReadHOTRepository : IUserReadHOTRepository
    {
        protected readonly AppDbContext db;
        public UserReadHOTRepository(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<User> GetByHOTId(Guid id)
        {                        
            return await db.User.FindAsync(id);
        }
    }
}
