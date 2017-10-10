using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestaurantWebSite.Startup))]
namespace RestaurantWebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
