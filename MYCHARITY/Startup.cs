using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MYCHARITY.Startup))]
namespace MYCHARITY
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
