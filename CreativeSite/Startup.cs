using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreativeSite.Startup))]
namespace CreativeSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
