using System.Collections.Generic;

namespace i_blog.Models
{
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
        }

        public Role(string name)
        {
            Name = name;
        }

        public int RoleID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}