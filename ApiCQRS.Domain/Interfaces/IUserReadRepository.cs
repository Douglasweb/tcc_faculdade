using ApiCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCQRS.Domain.Interfaces
{
    public interface IUserReadRepository
    {
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
        Task<IEnumerable<User>> GetAll();
    }
}
