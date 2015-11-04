using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Class_Manager.Startup))]
namespace Class_Manager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
