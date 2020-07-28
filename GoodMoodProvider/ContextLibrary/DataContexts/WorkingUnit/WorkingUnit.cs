using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodMoodProvider.DataContexts.WorkingUnit
{
    public class WorkingUnit 
    {
        private readonly DataContext _context;
        private readonly Logger _logger;

        public async Task SaveDBAsync()
        {
            try
            {
                if (_context.ChangeTracker.HasChanges())
                    await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw ex;            
            }
        }

        public WorkingUnit(DataContext context)
        {         
            _context = context;        
        }

    }
}
