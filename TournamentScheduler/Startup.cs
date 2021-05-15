using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TournamentScheduler.Startup))]
namespace TournamentScheduler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
