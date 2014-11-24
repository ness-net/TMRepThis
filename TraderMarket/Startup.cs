using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TraderMarket.Startup))]
namespace TraderMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
