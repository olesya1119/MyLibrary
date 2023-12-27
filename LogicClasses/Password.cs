using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.LogicClasses
{
    internal class Password
    {
        private string password;

        public string HeshPassword { get
            {
                return CreateSHA512(password);
            } 
        }

        public string NotHeshPassword { get { return password; } }

        public Password()
        {
            password = "";
            var r = new Random();
            while (password.Length < 8)
            {
                char c = (char)r.Next(33, 125);
                if (char.IsLetterOrDigit(c))
                    password += c;
            }
        }

        public Password(string password)
        {
            this.password = password;
        }


        public static string CreateSHA512(string source)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                return hash;
            }
        }

    }
}
