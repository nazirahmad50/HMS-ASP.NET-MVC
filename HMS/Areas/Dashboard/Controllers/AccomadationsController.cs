using HMS.Areas.Dashboard.ViewModels;
using HMS.Entities;
using HMS.Services;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccomadationsController : Controller
    {
        public ActionResult Index(string searchTerm, int? accomadationPackageID, int? page)
        {
            int pageSize = 3; //TODO: Set page size in Configuration

            page = page ?? 1; // The ?? operator returns the left-hand operand if it is not null, or else it returns the right operand

            int totalRecords = AccomadationsServices.Instance.SearchAccomadationsCount(searchTerm, accomadationPackageID); // get total accomadations based on search criteria

            AccomadationsListingViewModel model = new AccomadationsListingViewModel
            {

                Accomadation = AccomadationsServices.Instance.SearchAccomadations(searchTerm, accomadationPackageID, pageSize, page.Value),
                AccomadationPackage = AccomadationPackagesService.Instance.GetAllAccomadationPackages(), // get all accomadation Packages
                AccomadationPackageID = accomadationPackageID,
                Pager = new Pager(totalRecords, page, pageSize),
                TotalRecords = totalRecords
            };


            return View(model);
        }


        #region CREATE & EDIT


        [HttpGet]
        // Both 'Action' methods are used for Create and Edit
        public ActionResult Action(int? ID) // ID can be nullable
        {

            AccomadationsActionModel model = new AccomadationsActionModel();


            if (ID.HasValue) // Editing record
            {
                var accomadations = AccomadationsServices.Instance.GetAccomadationsByID(ID.Value);

                model.ID = accomadations.ID;
                model.Name = accomadations.Name;
                model.AccomadationPackageID = accomadations.AccomadationPackageID;

            }
            model.AccomadationPackages = AccomadationPackagesService.Instance.GetAllAccomadationPackages();


            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccomadationsActionModel model)
        {

            JsonResult json = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            bool result;
            if (model.ID > 0) // Editing record
            {

                Accomadation accomadation = new Accomadation
                {
                    ID = model.ID,
                    Name = model.Name,
                    AccomadationPackageID = model.AccomadationPackageID
                };

                result = AccomadationsServices.Instance.UpdateAccomadations(accomadation); // update accomadations in databse

            }
            else // Saving record
            {
                Accomadation accomadation = new Accomadation
                {
                    ID = model.ID,
                    Name = model.Name,
                    AccomadationPackageID = model.AccomadationPackageID
                };

                result = AccomadationsServices.Instance.SaveAccomadations(accomadation); // save accomadations in database
            }


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable perform action on accomadations" };

            }

            return json;
        }

        #endregion

        #region DELETE
        [HttpGet]
        public ActionResult Delete(int ID)
        {

            AccomadationsActionModel model = new AccomadationsActionModel();

            var accomadations = AccomadationsServices.Instance.GetAccomadationsByID(ID); // get accomadations based on ID

            model.ID = accomadations.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomadationsActionModel model)
        {

            JsonResult json = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            var accomadations = AccomadationsServices.Instance.GetAccomadationsByID(model.ID); // get accomadations based on ID passed from model

            var result = AccomadationsServices.Instance.DeleteAccomadations(accomadations); // delete from database


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on accomadations" };

            }

            return json;
        }
        #endregion

    }
}