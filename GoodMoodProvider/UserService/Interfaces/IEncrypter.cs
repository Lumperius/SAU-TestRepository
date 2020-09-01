using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Interfaces
{
    public interface IEncrypter
    {
        public string EncryptString(string inputString);
    }
}
