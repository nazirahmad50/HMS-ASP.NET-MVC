﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    // Have Moved the identity user class from the web to entities
    public class HMSUser : IdentityUser
    {
        // addional props for User
        public string FullName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<HMSUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}

