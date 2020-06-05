using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LAB456.Startup))]
namespace LAB456
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
