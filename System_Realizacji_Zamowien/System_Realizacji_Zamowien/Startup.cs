using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(System_Realizacji_Zamowien.Startup))]
namespace System_Realizacji_Zamowien
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
