using HMS.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class RolesManager : RoleManager<IdentityRole>
    {
        public RolesManager(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        {

        }

        public static RolesManager Create(IdentityFactoryOptions<RolesManager> options, IOwinContext context)
        {
            return new RolesManager(new RoleStore<IdentityRole>(context.Get<HMSContext>()));
        }
    }
}
