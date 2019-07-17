using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdministradorCanales.Startup))]
namespace AdministradorCanales
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
