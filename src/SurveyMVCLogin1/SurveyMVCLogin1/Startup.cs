using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SurveyMVCLogin1.Startup))]
namespace SurveyMVCLogin1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
