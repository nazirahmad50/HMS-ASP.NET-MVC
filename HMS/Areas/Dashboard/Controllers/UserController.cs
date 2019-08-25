using HMS.Areas.Dashboard.ViewModels;
using HMS.Entities;
using HMS.Services;
using HMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class UserController : Controller
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

        public UserController()
        {
        }
        // constructor dependancy injection
        public UserController(UserManager userManager, SignInManager signInManager, RolesManager rolesManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RolesManager = rolesManager;
        }

        #region SearchUsers Service
        public IEnumerable<HMSUser> SearchUsers(string searchTerm, string roleID, int pageSize, int page)
        {

            var users = UserManager.Users;


            // find based on searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // check if the searchterm exist in the database column Name
                users = users.Where(x => x.Email != null && x.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleID))
            {
                // check if the searchterm exist in the database column Name
                //users = users.Where(x => x.Email != null && x.Email.ToLower().Contains(searchTerm.ToLower()));
            }


            // skip = (1 - 1) * 3 = 0 * 3 = 0
            // skip = (2 - 1) * 3 = 1 * 3 = 3
            // skip = (3 - 1) * 3 = 2 * 3 = 6
            var skip = (page - 1) * pageSize;

            return users.OrderBy(x => x.Email).Skip(skip).Take(pageSize).AsEnumerable(); // we have to use the 'sortBy' if we are going to use 'Skip'


        }
        public int SearchUsersCount(string searchTerm, string roleID)
        {

            var users = UserManager.Users;


            // find based on searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // check if the searchterm exist in the database column Name
                users = users.Where(x => x.Email != null && x.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleID))
            {
                // check if the searchterm exist in the database column Name
                //users = users.Where(x => x.Email != null && x.Email.ToLower().Contains(searchTerm.ToLower()));
            }



            return users.Count();


        }
        #endregion

        public ActionResult Index(string searchTerm, string roleID, int? page)
        {
            int pageSize = 1; //TODO: Set page size in Configuration

            page = page ?? 1; // The ?? operator returns the left-hand operand if it is not null, or else it returns the right operand

            int totalRecords = SearchUsersCount(searchTerm,roleID); // get users count based on the params

            UserListingViewModel model = new UserListingViewModel
            {

                Users = SearchUsers(searchTerm, roleID, pageSize, page.Value), // get users  based on the params
                Roles = RolesManager.Roles, // get all roles
                RoleID = roleID,
                Pager = new Pager(totalRecords, page, pageSize),
                TotalRecords = totalRecords,
                SearchTerm = searchTerm
            };


            return View(model);
        }





        #region CREATE & EDIT


        [HttpGet]
        // Both 'Action' methods are used for Create and Edit
        public async Task<ActionResult> Action(string ID) // have to make method async as we are finding users async
        {

            UserActionViewModel model = new UserActionViewModel();


            if (!string.IsNullOrEmpty(ID)) // Editing record
            {
                var users = await UserManager.FindByIdAsync(ID); // find users based on param ID

                model.ID = users.Id;
                model.FullName = users.FullName;
                model.Email = users.Email;
                model.Username = users.UserName;
                model.Country = users.Country;
                model.City = users.City;
                model.Address = users.Address;
                //model.AccomadationPackageID = accomadations.AccomadationPackageID;

            }
            model.Roles = RolesManager.Roles;


            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<JsonResult> Action(UserActionViewModel model)
        {

            JsonResult json = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            IdentityResult result; // decalre IdentityResult

            if (!string.IsNullOrEmpty(model.ID)) // Editing record
            {
                var user = await UserManager.FindByIdAsync(model.ID); // find users based on param ID


                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Username;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;
                //model.AccomadationPackageID = accomadations.AccomadationPackageID;


                result = await UserManager.UpdateAsync(user); // update HMSUser table in databse

            }
            else // Saving record
            {
                HMSUser user = new HMSUser
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    UserName = model.Username,
                    Country = model.Country,
                    City = model.City,
                    Address = model.Address,
                    //model.AccomadationPackageID = accomadations.AccomadationPackageID;

                };
                result = await UserManager.CreateAsync(user); // create user in HMSUser table in databse without a password
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

            UserActionViewModel model = new UserActionViewModel();

            var user = await UserManager.FindByIdAsync(ID); // find users based on param ID

            model.ID = user.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(UserActionViewModel model)
        {

            JsonResult json = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            if (!string.IsNullOrEmpty(model.ID)) // Editing record
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                IdentityResult result = await UserManager.DeleteAsync(user);

                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid User" };

            }



            return json;
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult> UserRoles(string ID)
        {

            UserRolesViewModel model = new UserRolesViewModel();

            var user = await UserManager.FindByIdAsync(ID); // find users based on param ID
            var userRoleIds = user.Roles.Select(x => x.RoleId).ToList(); // get all users that have role ids
            model.UserRoles = RolesManager.Roles.Where(x => userRoleIds.Contains(x.Id)); // check if the roles manager roles contain those user role ids

            model.Roles = RolesManager.Roles; // show all the roles

            return PartialView("_UserRoles", model);
        }

        [HttpPost]
        public async Task<JsonResult> UserRoles(UserActionViewModel model)
        {

            JsonResult json = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            if (!string.IsNullOrEmpty(model.ID)) // Editing record
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                IdentityResult result = await UserManager.DeleteAsync(user);

                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid User" };

            }



            return json;
        }
    }
}