using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodMoodProvider.DbInitializer.Interfaces
{
    public interface IAdminInitializer
    { 
        public Task InitializeAsync();
    }
}
