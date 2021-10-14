using ApiCQRS.Application.Services.Notifications;
using ApiCQRS.Domain.Interfaces;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ApiCQRS.Application.Commands
{
    public class UpdateUserCommand : IRequest<Guid>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool Status { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
        {
            private readonly IUserWriteRepository _context;
            private readonly IMediator _mediator;
            private readonly IUserReadRepository _contextR;
            public UpdateProductCommandHandler(IUserWriteRepository context, IMediator mediator, IUserReadRepository contextR)
            {
                _context = context;
                _mediator = mediator;
                _contextR = contextR;
            }
            public async Task<Guid> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
            {
                var user = await _contextR.GetById(command.Id);
                if (user == null || string.IsNullOrWhiteSpace(user.UserName)) {
                    await _mediator.Publish(new ErrorNotification
                    {
                        Error = "User not found",
                        Stack = "user is null"
                    }, cancellationToken);
                    return default;
                }
                user.UserName = command.Name;
                user.UserEmail = command.Email;
                user.UserStatus = command.Status;
                user.UserUpdatedAt = DateTime.Now;
                user.CanBeUpdated = true;
                _context.Update(user);

                await _mediator.Publish(new UserActionNotification
                {
                    Name = command.Name,
                    Email = command.Email,
                    Action = ActionNotification.Updated
                }, cancellationToken);

                return user.UserId.Value;
            }
        }
    }
}

