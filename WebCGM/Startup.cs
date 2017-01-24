using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebCGM.App_Start.Startup))]
namespace WebCGM.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
