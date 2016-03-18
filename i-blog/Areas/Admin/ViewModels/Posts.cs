using i_blog.Infrastructure;
using i_blog.Models;

namespace i_blog.Areas.Admin.ViewModels
{
    public class PostsIndex
    {
        public PagedData<Post> Posts { get; set; }
    }
}
