using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebStepBlog.Startup))]
namespace WebStepBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
