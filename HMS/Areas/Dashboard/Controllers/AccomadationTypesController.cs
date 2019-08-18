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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listing()
        {
            AccomadationTypesListingModel model = new AccomadationTypesListingModel{

                AccomadationType = AccomadationTypesService.Instance.GetAllAccomadationTypes()

            };

            return PartialView("_Listing", model);
        }

        #region CREATE & DELETE


        [HttpGet]
        // Both 'Action' methods are used for Create and Edit
        public ActionResult Action()
        {

            AccomadationTypesActionModel model = new AccomadationTypesActionModel();
          

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccomadationTypesActionModel model)
        {

            JsonResult json = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };


            AccomadationType accomadationTypes = new AccomadationType // create AccomadationType object
            {
                Name = model.Name,
                Description = model.Description
            };

            var result = AccomadationTypesService.Instance.SaveAccomadationTypes(accomadationTypes); // save AccomadationType in database

            if (result) 
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to add accomadation types" };

            }

            return json;
        }

        #endregion
    }
}