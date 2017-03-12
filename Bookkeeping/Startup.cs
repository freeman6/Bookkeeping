using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bookkeeping.Startup))]
namespace Bookkeeping
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
