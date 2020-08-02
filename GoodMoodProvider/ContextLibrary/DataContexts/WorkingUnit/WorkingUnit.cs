using ContextLibrary.DataContexts;
using ContextLibrary.Interfaces;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingLibrary.DataContexts.WorkingUnit
{
    public class WorkingUnit : IWorkingUnit
    {
        private readonly DataContext _context;
        private readonly Logger _logger;

        public async Task SaveDBAsync()
        {
            int changesNumber = 0;
            try
            {
                if (_context.ChangeTracker.HasChanges())
                { 
                     changesNumber = await _context.SaveChangesAsync();
                    _logger.Information($"{changesNumber} changes were applied.");
                }
            }

            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw ex;            
            }
        }

        public void SaveDB()
        {
            int changesNumber = 0;
            try
            {
                if (_context.ChangeTracker.HasChanges())
                {
                    changesNumber = _context.SaveChanges();
                    _logger.Information($"{changesNumber} changes were applied.");
                }
            }

            catch (Exception ex)
            {
                _logger.Error($"Error occured while saving changes to database:{ex.Message}");
                throw ex;
            }
        }


        public WorkingUnit(DataContext context)
        {         
            _context = context;        
        }

    }
}
