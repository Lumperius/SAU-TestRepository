using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Interfaces;

namespace UserService
{
    public class Encrypter : IEncrypter
    {
        public string EncryptString(string inputString)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(inputString + "salt");
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hash = System.Text.Encoding.ASCII.GetString(data);

            return hash;
        }
    }
}
