using ContextLibrary.DataContexts;
using CqsLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqsLibrary.QueryHandlers
{
    public class GetByIdHandler : IQueryHandler
    {
        private readonly DataContext _context;
        public GetByIdHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<News> Handle(Guid id)
        {
            return await _context.News.FirstOrDefaultAsync(n => n.ID == id);
        }
    }
}
