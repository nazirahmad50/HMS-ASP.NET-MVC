using HMS.Entities;
using HMS.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class UserListingViewModel
    {
        public IEnumerable<HMSUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public string RoleID { get; set; } // have to make it string as the role id in identity framework is of string type
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
        public int TotalRecords { get; set; }
    }

    public class UserActionViewModel
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public string RoleID { get; set; }

        public IdentityRole Role { get; set; } 
        public IEnumerable<IdentityRole> Roles { get; set; }


    }
    public class UserRolesViewModel
    {

        public string ID { get; set; }

        public IEnumerable<IdentityRole> UserRoles { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }



    }
}