using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projet2_onlinebanking.Startup))]
namespace Projet2_onlinebanking
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
