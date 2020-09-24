using ContextLibrary.DataContexts;
using CqsLibrary.Interfaces;
using CqsLibrary.Queries.NewsQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CqsLibrary.Handlers.QueryHandlers
{
    class GetNewsAllHandler : IRequestHandler< GetNewsAll, IEnumerable<News>>
    {
        private readonly DataContext _context;

        public GetNewsAllHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<News>> Handle(GetNewsAll query, CancellationToken token)
        {
            return await _context.News.ToListAsync();
        }

    }
}
