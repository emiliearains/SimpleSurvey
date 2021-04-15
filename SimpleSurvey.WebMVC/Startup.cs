using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleSurvey.WebMVC.Startup))]
namespace SimpleSurvey.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
