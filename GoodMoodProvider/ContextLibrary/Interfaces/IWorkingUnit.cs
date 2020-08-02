using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContextLibrary.Interfaces
{
    public interface IWorkingUnit
    {
        public void SaveDB();
        public Task SaveDBAsync();
    }
}
