using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fitness_Club2.Startup))]
namespace Fitness_Club2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
