using ContextLibrary.DataContexts;
using CqsLibrary.Queries.UserQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CqsLibrary.Handlers.QueryHandlers
{
    class GetUserByIdWithRolesHandler : IRequestHandler<GetUserById, User>
    {
        private readonly DataContext _context;

        public GetUserByIdWithRolesHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(GetUserById query, CancellationToken token)
        {
            return await _context.User.Include("UserRoles").Where(n => n.ID == query.Id).FirstOrDefaultAsync();
        }

    }
}
