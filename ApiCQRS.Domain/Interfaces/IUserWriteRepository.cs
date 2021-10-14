using ApiCQRS.Domain.Entities;

namespace ApiCQRS.Domain.Interfaces
{
    public interface IUserWriteRepository
    {       
        void Add(User user);
        void Update(User user);
        void Remove(User user);
    }
}
