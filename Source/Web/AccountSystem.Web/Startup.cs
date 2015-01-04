using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AccountSystem.Web.Startup))]
namespace AccountSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
