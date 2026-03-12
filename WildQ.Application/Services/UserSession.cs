using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildQ.Application.Services
{
    public class UserSession
    {
        // Designpattern: Singleton
        // To not make a new instance of the class on every page.
        // Used to keep track of who is logged in.
        
        private static readonly UserSession _instance = new UserSession(); // Can now only make one instance of this class

        public static UserSession GetUserSession()
        {
            return _instance; // Getting the instance of this class
        }

        private UserSession() // Private constructor so we can't make an instans of this class
        {

        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }

        public void LogIn(string username, string password, bool isAdmin)
        {
            UserName = username;
            Password = password;
            IsLoggedIn = true;
            IsAdmin = isAdmin;
        }

        public void LogOut()
        {
            UserName = null;
            Password = null;
            IsLoggedIn = false;
            IsAdmin = false;
        }
    }
}
