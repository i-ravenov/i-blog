using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace i_blog.Models
{
    public class Tag
    {
        public Tag()
        {
            Posts = new List<Post>();
        }

        public int TagId { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
                     
        public virtual ICollection<Post> Posts { get; set; }
    }
}