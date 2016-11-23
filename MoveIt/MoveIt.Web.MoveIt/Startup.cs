using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoveIt.Web.MoveIt.Startup))]
namespace MoveIt.Web.MoveIt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
