using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace i_blog.Models
{
    public class User
    {
        private const int workFactor = 13;

        public User() { }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        public void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, workFactor);
        }

        public bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

    }
}