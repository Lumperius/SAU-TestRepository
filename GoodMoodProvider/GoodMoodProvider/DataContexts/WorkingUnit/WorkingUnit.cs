using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodMoodProvider.DataContexts.WorkingUnit
{
    public class WorkingUnit 
    {
        private readonly DataContext _context;
        
   /*
    public async Task<int> SaveDB()
        {
            await _context.SaveChanges();
        }
    */    
        
        public WorkingUnit(DataContext context)
        {
            _context = context;
        }
    }
}
