using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MSC.ConferenceMate.Web.Startup))]
namespace MSC.ConferenceMate.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
