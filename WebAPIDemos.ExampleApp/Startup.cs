using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAPIDemos.ExampleApp.Startup))]
namespace WebAPIDemos.ExampleApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
