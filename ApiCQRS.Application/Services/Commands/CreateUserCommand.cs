using ApiCQRS.Application.Cripto;
using ApiCQRS.Application.Services.Notifications;
using ApiCQRS.Domain.Entities;
using ApiCQRS.Domain.Interfaces;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ApiCQRS.Application.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Boolean Status { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateUserCommand, Guid>
        {
            private readonly IUserWriteRepository _context;
            private readonly IMediator _mediator;
            

            public CreateProductCommandHandler(IUserWriteRepository context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(CreateUserCommand command,
                CancellationToken cancellationToken)
            {
                var User = new User();
                User.UserName = command.Name;
                User.UserEmail = command.Email;
                User.UserId = Guid.NewGuid();
                User.UserCreatedAt = DateTime.Now;
                User.UserStatus = command.Status;
                User.CanBeUpdated = true;               
                var hashing = new HashingManager();                
                var hashpass = hashing.HashToString(command.Password);
                User.UserPassword = hashpass;
                _context.Add(User);

                await _mediator.Publish(new UserActionNotification
                {
                    Name = command.Name,
                    Email = command.Email,
                    Action = ActionNotification.Created
                }, cancellationToken);

                return User.UserId.Value;
            }


            
        }
    }
}
