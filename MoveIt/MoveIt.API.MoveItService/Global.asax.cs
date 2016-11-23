using System.Data.Entity;
using System.Web;
using System.Web.Http;
using MoveIt.Core.Data;

namespace MoveIt.API.MoveItService
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer(new MoveItSeed());
            AutoMapperConfigurartion.BootStrap();
            BootStrapper.Run();
        }
    }
}
