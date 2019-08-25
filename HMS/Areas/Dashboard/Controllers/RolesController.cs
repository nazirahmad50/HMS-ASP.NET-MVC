using HMS.Areas.Dashboard.ViewModels;
using HMS.Services;
using HMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class RolesController : Controller
    {
        // private fields for signin manager, user manager and role manager
        // comoon practise of private fields is to used '_' before the field name
        private SignInManager _signInManager;
        private UserManager _userManager;
        private RolesManager _rolesManager;

        // properties for signin manager, user manager and role manager
        public SignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public UserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public RolesManager RolesManager
        {
            get
            {
                return _rolesManager ?? HttpContext.GetOwinContext().Get<RolesManager>();
            }
            private set
            {
                _rolesManager = value;
            }
        }

        public RolesController()
        {
        }
        // constructor dependancy injection
        public RolesController(UserManager userManager, SignInManager signInManager, RolesManager rolesManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            SignInManager = signInManager;
            RolesManager = rolesManager;
        }

        #region SearchUsers Service
        public IEnumerable<IdentityRole> SearchRoles(string searchTerm, int pageSize, int page)
        {

            var roles = RolesManager.Roles;


            // find based on searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // check if the searchterm exist in the database column Name
                roles = roles.Where(x => x.Name != null && x.Name.ToLower().Contains(searchTerm.ToLower()));
            }
          
            // skip = (1 - 1) * 3 = 0 * 3 = 0
            // skip = (2 - 1) * 3 = 1 * 3 = 3
            // skip = (3 - 1) * 3 = 2 * 3 = 6
            var skip = (page - 1) * pageSize;

            return roles.OrderBy(x => x.Name).Skip(skip).Take(pageSize).AsEnumerable(); // we have to use the 'sortBy' if we are going to use 'Skip'


        }
        public int SearchRolesCount(string searchTerm)
        {

            var roles = RolesManager.Roles;

            // find based on searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // check if the searchterm exist in the database column Name
                roles = roles.Where(x => x.Name != null && x.Name.ToLower().Contains(searchTerm.ToLower()));
            }
          

            return roles.Count();


        }
        #endregion

        public ActionResult Index(string searchTerm, int? page)
        {
            int pageSize = 1; //TODO: Set page size in Configuration

            page = page ?? 1; // The ?? operator returns the left-hand operand if it is not null, or else it returns the right operand

            int totalRecords = SearchRolesCount(searchTerm); // get roles count based on the params

            RolesListingViewModel model = new RolesListingViewModel
            {

                Roles = SearchRoles(searchTerm, pageSize, page.Value), // get roles  based on the params
                Pager = new Pager(totalRecords, page, pageSize),
                TotalRecords = totalRecords,
                SearchTerm = searchTerm
            };


            return View(model);
        }





        #region CREATE & EDIT


        [HttpGet]
        // Both 'Action' methods are used for Create and Edit
        public async Task<ActionResult> Action(string ID) // have to make method async as we are finding roles async
        {

            RolesActionViewModel model = new RolesActionViewModel();


            if (!string.IsNullOrEmpty(ID)) // Editing record
            {
                var roles = await RolesManager.FindByIdAsync(ID); // find roles based on param ID

                model.ID = roles.Id;
                model.Name = roles.Name;
          
            }


            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<JsonResult> Action(RolesActionViewModel model)
        {

            JsonResult json = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            IdentityResult result; // decalre IdentityResult

            if (!string.IsNullOrEmpty(model.ID)) // Editing record
            {
                var roles = await RolesManager.FindByIdAsync(model.ID); // find roles based on param ID

                roles.Id = model.ID;
                roles.Name = model.Name;
         
                result = await RolesManager.UpdateAsync(roles); // update roles manager

            }
            else // Saving record
            {
                IdentityRole role = new IdentityRole{Name = model.Name};

                result = await RolesManager.CreateAsync(role); // create role async
            }

            // show true if result is success otherwise false and show the result error
            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };



            return json;
        }

        #endregion

        #region DELETE
        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {

            RolesActionViewModel model = new RolesActionViewModel();

            var role = await RolesManager.FindByIdAsync(ID); // find roles based on param ID

            model.ID = role.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(RolesActionViewModel model)
        {

            JsonResult json = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            if (!string.IsNullOrEmpty(model.ID)) // Editing record
            {
                var role = await RolesManager.FindByIdAsync(model.ID);

                IdentityResult result = await RolesManager.DeleteAsync(role);

                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid User" };

            }



            return json;
        }
        #endregion
    
}
}