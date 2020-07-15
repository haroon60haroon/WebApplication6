using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication6.Models;

[assembly: OwinStartup(typeof(WebApplication6.Startup))]

namespace WebApplication6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        //  CreateRoles();

        }
        private void CreateRoles()
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                if (!roleManager.RoleExists("Coordinator"))
                {
                    role.Name = "Coordinator";
                    roleManager.Create(role);
                }
                if (!roleManager.RoleExists("Supervisor"))
                {
                    role.Name = "Supervisor";
                    roleManager.Create(role);
                }
                if (!roleManager.RoleExists("Student"))
                {
                    role.Name = "Student";
                    roleManager.Create(role);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

    }
}
