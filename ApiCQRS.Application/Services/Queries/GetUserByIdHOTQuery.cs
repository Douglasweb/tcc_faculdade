using ApiCQRS.Domain.Entities;
using ApiCQRS.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace ApiCQRS.Application.Queries
{
    public class GetUserByIdHOTQuery : IRequest<User>
    {
        public Guid Id { get; set; }
        public class GetProductByIdHOTQueryHandler : IRequestHandler<GetUserByIdHOTQuery, User>
        {
            private readonly IUserReadHOTRepository _context;
            private readonly IMediator _mediator;
            public GetProductByIdHOTQueryHandler(IUserReadHOTRepository context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<User> Handle(GetUserByIdHOTQuery query, CancellationToken cancellationToken)
            {
                var User = await _context.GetByHOTId(query.Id);

                if (User == null || string.IsNullOrWhiteSpace(User.UserName)) return default;

                return User;
            }
        }
    }
}
