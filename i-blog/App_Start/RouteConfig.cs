using System.Web.Mvc;
using System.Web.Routing;
using i_blog.Controllers;

namespace i_blog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new[] { typeof(PostsController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("TagForReaThisTime", "tag/{idAndSlug}", new { Controller = "Posts", action = "Tag" }, namespaces);

            routes.MapRoute("Tag", "tag/{tagid}-{slug}", new { Controller = "Tags", action = "Show" }, namespaces);

            routes.MapRoute("PostForReaThisTime", "post/{idAndSlug}", new { Controller = "Posts", action = "Show" },namespaces);

            routes.MapRoute("Post", "post/{postid}-{slug}", new { Controller = "Posts", action = "Show" }, namespaces);

            routes.MapRoute("Login", "login", new { Controller = "Auth", action = "Login" }, namespaces);

            routes.MapRoute("Logout", "logout", new { Controller = "Auth", action = "Logout" }, namespaces);

            routes.MapRoute("Home", "", new { controller = "Posts", action = "Index" }, namespaces);
        }
    }
}
