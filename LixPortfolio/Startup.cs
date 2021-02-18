using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LixPortfolio.Startup))]
namespace LixPortfolio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
