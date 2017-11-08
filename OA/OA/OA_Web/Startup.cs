using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OA_Web.Startup))]
namespace OA_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
