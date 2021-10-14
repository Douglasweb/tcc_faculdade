using ApiCQRS.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ApiCQRS.Domain.Interfaces
{
    public interface IUserReadHOTRepository
    {
        Task<User> GetByHOTId(Guid id);       
    }
}
