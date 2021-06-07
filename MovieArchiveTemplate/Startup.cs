using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieArchiveTemplate.Startup))]
namespace MovieArchiveTemplate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
