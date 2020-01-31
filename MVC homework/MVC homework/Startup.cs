using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_homework.Startup))]
namespace MVC_homework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
