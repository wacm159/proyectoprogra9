using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoP9.Startup))]
namespace ProyectoP9
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
