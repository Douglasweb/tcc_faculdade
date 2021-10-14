using ApiCQRS.Domain.Entities;
using ApiCQRS.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiCQRS.Application.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<User>>
    {
        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery,
            IEnumerable<User>>
        {
            private readonly IUserReadRepository _context;
            private readonly IMediator _mediator;
            public GetAllUsersQueryHandler(IUserReadRepository context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query,
                CancellationToken cancellationToken)
            {
                var UserList = await _context.GetAll();
                if (UserList == null || UserList?.Count() == 0) return default;

                return UserList;
            }
        }
    }
}
