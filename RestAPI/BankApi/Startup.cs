using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartup(typeof(JoinUsAPI.Startup))]

namespace JoinUsAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Models.ApplicationDbContext>());
        }
    }
}
