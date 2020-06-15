using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HastaneYonetim.Startup))]
namespace HastaneYonetim
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
