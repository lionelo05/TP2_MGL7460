using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TemplateTesting.Startup))]
namespace TemplateTesting
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
