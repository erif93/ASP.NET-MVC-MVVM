using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bootcamp20ASPNET.Startup))]
namespace Bootcamp20ASPNET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
