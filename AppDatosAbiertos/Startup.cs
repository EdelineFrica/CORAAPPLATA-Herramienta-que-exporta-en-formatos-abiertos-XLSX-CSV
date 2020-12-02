using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppDatosAbiertos.Startup))]
namespace AppDatosAbiertos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
