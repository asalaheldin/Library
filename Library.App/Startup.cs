using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Library.App.Startup))]
namespace Library.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
