using ApiCQRS.Application.Services.Notifications;
using ApiCQRS.Domain.Entities;
using ApiCQRS.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiCQRS.Application.Commands
{
    public class DeleteUserByIdCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, Guid>
        {
            private readonly IUserWriteRepository _context;
            private readonly IUserReadRepository _contextR;
            private readonly IMediator _mediator;
            public DeleteProductByIdCommandHandler(IUserWriteRepository context, IMediator mediator, IUserReadRepository contextR)
            {
                _context = context;
                _mediator = mediator;
                _contextR = contextR;
            }

            public async Task<Guid> Handle(DeleteUserByIdCommand command, CancellationToken cancellationToken)
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
                _context.Remove(user);

                await _mediator.Publish(new UserActionNotification
                {                   
                    Action = ActionNotification.Deleted
                }, cancellationToken);

                return user.UserId.Value;
            }
        }
    }
}
