using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsUploader.Interfaces
{
    public interface INewsService
    {
        public Task LoadNewsInDb();
    }
}
