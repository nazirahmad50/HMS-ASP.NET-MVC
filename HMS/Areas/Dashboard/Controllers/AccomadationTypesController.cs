using HMS.Areas.Dashboard.ViewModels;
using HMS.Entities;
using HMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccomadationTypesController : Controller
    {

        public ActionResult Index(string searchTerm)
        {
            AccomadationTypesListingModel model = new AccomadationTypesListingModel
            {
                AccomadationType = AccomadationTypesService.Instance.SearchAccomadationTypes(searchTerm), // find accomadation types based on searchTerm
                SearchTerm = searchTerm
            };


            return View(model);
        }

  

        #region CREATE & EDIT


        [HttpGet]
        // Both 'Action' methods are used for Create and Edit
        public ActionResult Action(int? ID) // ID can be nullable
        {

            AccomadationTypesActionModel model = new AccomadationTypesActionModel();


            if (ID.HasValue) // Editing record
            {
                var accomadationType = AccomadationTypesService.Instance.GetAccomadationTypesByID(ID.Value);

                model.ID = accomadationType.ID;
                model.Name = accomadationType.Name;
                model.Description = accomadationType.Description;
            }


            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccomadationTypesActionModel model)
        {

            JsonResult json = new JsonResult{ JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            var result = false;

            if (model.ID > 0) // Editing record
            {

                AccomadationType accomadationType = new AccomadationType { ID = model.ID, Name = model.Name, Description = model.Description };

                result = AccomadationTypesService.Instance.UpdateAccomadationTypes(accomadationType); // update accomadation type in databse

            }
            else // Saving record
            {
                AccomadationType accomadationTypes = new AccomadationType { Name = model.Name, Description = model.Description}; // create AccomadationType object and set its props

                result = AccomadationTypesService.Instance.SaveAccomadationTypes(accomadationTypes); // save AccomadationType in database
            }

         
            if (result) 
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable perform action on accomadation Type" };

            }

            return json;
        }

        #endregion

        #region DELETE
        [HttpGet]
        public ActionResult Delete(int ID) 
        {

            AccomadationTypesActionModel model = new AccomadationTypesActionModel();

            var accomadationtype = AccomadationTypesService.Instance.GetAccomadationTypesByID(ID); // get accomadation type based on ID

            model.ID = accomadationtype.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomadationTypesActionModel model)
        {

            JsonResult json = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            var accomadationType = AccomadationTypesService.Instance.GetAccomadationTypesByID(model.ID); // get accomadation type based on ID passed from model

            var result = AccomadationTypesService.Instance.DeleteAccomadationTypes(accomadationType); // delete from database
        

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on accomadation Type" };

            }

            return json;
        }
        #endregion

    }
}