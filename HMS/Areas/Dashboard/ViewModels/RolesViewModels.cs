using HMS.Entities;
using HMS.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class RolesListingViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; } // didnt make identityRole entity, instead im using already supplied entity frameworks
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
        public int TotalRecords { get; set; }
    }

    public class RolesActionViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }



    }
}