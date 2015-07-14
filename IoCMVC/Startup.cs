using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IoCMVC.Startup))]
namespace IoCMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
