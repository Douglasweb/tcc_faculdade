using ApiCQRS.Domain.Entities;
using ApiCQRS.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiCQRS.Application.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public Guid Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
        {
            private readonly IUserReadRepository _context;
            private readonly IMediator _mediator;
            public GetProductByIdQueryHandler(IUserReadRepository context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<User> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
            {
                var User = await _context.GetById(query.Id);

                if (User == null || string.IsNullOrWhiteSpace(User.UserName)) return default;

                return User;
            }
        }
    }
}
