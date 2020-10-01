using ContextLibrary.DataContexts;
using CqsLibrary.Interfaces;
using CqsLibrary.Queries.NewsQueries;
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
    public class GetNewsByIdHandler :IRequestHandler<GetNewsById, News>
    {
        private readonly DataContext _context;

        public GetNewsByIdHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<News> Handle(GetNewsById query, CancellationToken token)
        {
            return await _context.News.Where(n => n.ID == query.ID).FirstOrDefaultAsync();
        }
    }
}
