using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartup(typeof(SPA_Template_ASP.NET_.NET_Framework_4._7._2.Startup))]

namespace SPA_Template_ASP.NET_.NET_Framework_4._7._2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
