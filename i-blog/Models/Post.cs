using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace i_blog.Models
{
    public class Post
    {

        public Post()
        {
            Tags = new List<Tag>();
        }

        public int PostId { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }

        public virtual IList<Tag> Tags { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool IsDeleted => DeletedAt != null;
    }
}