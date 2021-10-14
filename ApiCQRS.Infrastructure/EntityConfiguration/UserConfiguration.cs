using System;
using ApiCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiCQRS.Infrastructure.EntityConfiguration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(b => b.UserName).HasMaxLength(100);
            builder.Property(b => b.UserEmail).HasMaxLength(150);

            //Iniciar com dados

            builder.HasData(
                new User
                {
                    UserId = Guid.NewGuid(),
                    UserUpdatedAt = null,
                    UserCreatedAt = DateTime.Now,
                    UserName = "Douglas Eduardo",
                    UserEmail = "webdouglasti@gmail.com",
                    UserPassword = "0xB123E9E19D217169B981A61188920F9D28638709A5132201684D792B9264271B7F09157ED4321B1C097F7A4ABECFC0977D40A7EE599C845883BD1074CA23C4AF", // hash bytes 512
                    CanBeUpdated = true,
                    UserStatus = true
                }
               );

        }
    }
}

