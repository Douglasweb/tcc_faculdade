using System;

namespace ApiCQRS.Domain.Entities
{
    public class User
    {
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime UserCreatedAt { get; set; }
        public DateTime? UserUpdatedAt { get; set; }
        public Boolean UserStatus { get; set; }        
        public string UserPassword { get; set; }
        public Boolean CanBeUpdated { get; set; }

    }
}
