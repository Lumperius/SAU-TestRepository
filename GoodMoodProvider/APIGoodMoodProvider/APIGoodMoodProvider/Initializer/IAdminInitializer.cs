using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGoodMoodProvider.Initializer
{
    public interface IAdminInitializer
    {
        public Task InitializeAsync();

    }
}
