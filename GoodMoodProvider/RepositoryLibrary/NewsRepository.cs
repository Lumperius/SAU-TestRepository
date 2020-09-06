using ContextLibrary.DataContexts;
using ContextLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;
using RepositoryLibrary.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingLibrary.DataContexts.WorkingUnit;

namespace RepositoryLibrary
{
    public class NewsRepository : IRepository<News>
    {
        private readonly DataContext _context;

        public NewsRepository(DataContext context)
        {
            _context = context;
        }


        public async Task AddAsync(News news)
        {
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<News> news)
        {
            await _context.News.AddRangeAsync(news);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (_context.News.Any(N => N.ID == id))
                _context.News.Remove(_context.News.FirstOrDefault(N => N.ID == id));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<Guid> id)
        {
            if (_context.News.Any(N => N.ID == id.FirstOrDefault(i => i == N.ID)))
                _context.News.RemoveRange(_context.News.Where(n => n.ID == id.FirstOrDefault(i => i == n.ID)));
            await _context.SaveChangesAsync();
        }

        public async Task Clear()
        {
            _context.News.RemoveRange(_context.News.Where( n => n.ID != null));
            await _context.SaveChangesAsync();
        }

        public async Task<News> GetByIdAsync(Guid id)
        {
           return await _context.News.FirstOrDefaultAsync(n => n.ID == id);
        }

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.News.ToListAsync();
        }
    }
}
