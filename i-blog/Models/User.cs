using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace i_blog.Models
{
    public class User
    {
        public User() { }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        public void SetPassword(string password)
        {
            PasswordHash = "Ignore me !";
        }

    }
}