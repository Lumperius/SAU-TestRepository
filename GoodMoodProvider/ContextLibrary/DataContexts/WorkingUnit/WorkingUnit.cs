using ContextLibrary.DataContexts;
using ContextLibrary.Interfaces;
using Serilog;
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

        public async Task SaveDBAsync()
        {
            int changesNumber = 0;
            try
            {
                if (_context.ChangeTracker.HasChanges())
                { 
                     changesNumber = await _context.SaveChangesAsync();
                    Log.Information($"{changesNumber} changes were applied.");
                }
            }

            catch (Exception ex)
            {
                Log.Error(ex.Message);
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
                    Log.Information($"{changesNumber} changes were applied.");
                }
            }

            catch (Exception ex)
            {
                Log.Error($"Error occured while saving changes to database:{ex.Message}");
                throw ex;
            }
        }


        public WorkingUnit(DataContext context)
        {         
            _context = context;        
        }

    }
}
