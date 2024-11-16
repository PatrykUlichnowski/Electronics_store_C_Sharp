using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    internal class User
    {
        //Class for each user in the system including an administrator
        public string Login { get; set; }
        public string Password { get; set; }
        public User(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }
    }
}
