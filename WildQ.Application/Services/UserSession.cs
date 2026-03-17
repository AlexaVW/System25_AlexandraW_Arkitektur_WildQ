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
        // Used to keep track of who is logged in.
        // Only have to make an instance of this class once
        
        private static readonly UserSession _instance = new UserSession(); 

        public static UserSession GetUserSession()
        {
            return _instance; 
        }

        private UserSession() 
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
