using ApiCQRS.Application.Cripto;
using ApiCQRS.Application.Services.Notifications;
using ApiCQRS.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiCQRS.Application.Commands
{
    public class UpdatePasswordUserCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }       
        public string NewPassword { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdatePasswordUserCommand, Guid>
        {
            private readonly IUserWriteRepository _context;
            private readonly IUserReadRepository _contextR;
            private readonly IMediator _mediator;
            public UpdateProductCommandHandler(IUserWriteRepository context, IMediator mediator, IUserReadRepository contextR)
            {
                _context = context;
                _mediator = mediator;
                _contextR = contextR;
            }
            public async Task<Guid> Handle(UpdatePasswordUserCommand command, CancellationToken cancellationToken)
            {
                var user = await _contextR.GetById(command.Id);
                if (user == null)
                {
                    await _mediator.Publish(new ErrorNotification
                    {
                        Error = "User not found",
                        Stack = "user is null"
                    }, cancellationToken);
                    return default;
                }

                var hashing = new HashingManager();
                var hashpass = hashing.HashToString(command.NewPassword);
                user.UserPassword = hashpass;              
                user.UserUpdatedAt = DateTime.Now;
                user.CanBeUpdated = true;
                _context.Update(user);

                await _mediator.Publish(new UserActionNotification
                {
                    Name = user.UserName,
                    Email = user.UserEmail,
                    Action = ActionNotification.Updated
                }, cancellationToken);

                return user.UserId.Value;
            }
        }
    }
}

