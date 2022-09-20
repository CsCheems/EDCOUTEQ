using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EDCOUTEQ.StartupOwin))]

namespace EDCOUTEQ
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
