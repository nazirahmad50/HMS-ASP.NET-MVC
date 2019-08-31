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
    public class AccomadationPackagesController : Controller
    {
        public ActionResult Index(string searchTerm, int? accomadationTypeID, int? page)
        {
            int pageSize = 3; //TODO: Set page size in Configuration
            
            page = page ?? 1; // The ?? operator returns the left-hand operand if it is not null, or else it returns the right operand

            int totalRecords = AccomadationPackagesService.Instance.SearchAccomadationPackagesCount(searchTerm, accomadationTypeID); // get total accomadation Packages based on search criteria

            AccomadationPackagesListingModel model = new AccomadationPackagesListingModel
            {
                
                AccomadationPackage = AccomadationPackagesService.Instance.SearchAccomadationPackages(searchTerm, accomadationTypeID, pageSize, page.Value), 
                AccomadationType = AccomadationTypesService.Instance.GetAllAccomadationTypes(), // get all accomadation types
                AccomadationtypeID = accomadationTypeID,
                Pager = new Pager(totalRecords,page,pageSize),
                TotalRecords = totalRecords
            };
           

            return View(model);
        }


        #region CREATE & EDIT


        [HttpGet]
        // Both 'Action' methods are used for Create and Edit
        public ActionResult Action(int? ID) // ID can be nullable
        {

            AccomadationPackagesActionModel model = new AccomadationPackagesActionModel();


            if (ID.HasValue) // Editing record
            {
                var accomadationPackage = AccomadationPackagesService.Instance.GetAccomadationPackagesByID(ID.Value);

                model.ID = accomadationPackage.ID;
                model.Name = accomadationPackage.Name;
                model.NoOfRoom = accomadationPackage.NoOfRoom;
                model.FeePerNight = accomadationPackage.FeePerNight;
                model.AccomadationTypeID = accomadationPackage.AccomadationTypeID;

                // get 'AccomadationPackagePictures' by accomadationPackage id
                model.AccomadationPackagePictures = AccomadationPackagesService.Instance.GetPicturesByAccomadationPackageID(accomadationPackage.ID);

            }
            model.AccomadationTypes = AccomadationTypesService.Instance.GetAllAccomadationTypes();




            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccomadationPackagesActionModel model)
        {

            JsonResult json = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            bool result;
            if (model.ID > 0) // Editing record
            {

                AccomadationPackage accomadationPackage = new AccomadationPackage {
                    ID = model.ID,
                    Name = model.Name,
                    NoOfRoom = model.NoOfRoom,
                    FeePerNight = model.FeePerNight,
                    AccomadationTypeID = model.AccomadationTypeID,
                  
                };

                result = AccomadationPackagesService.Instance.UpdateAccomadationPackages(accomadationPackage); // update accomadation packages in databse

            }
            else // Saving record
            {
                AccomadationPackage accomadationPackage = new AccomadationPackage
                {
                    ID = model.ID,
                    Name = model.Name,
                    NoOfRoom = model.NoOfRoom,
                    FeePerNight = model.FeePerNight,
                    AccomadationTypeID = model.AccomadationTypeID,
                }; // create AccomadationType object and set its props

                //--------------------Saving pictures to database AccomadationPackagePictures----------------
                List<int> picIds = model.PictureIds.Split(',').Select(x => int.Parse(x)).ToList(); // split the pictureIds and convert them all to int and store in a list

                var pictures = SharedDashboardService.Instance.getPicturesByIds(picIds); // get pictures from 'Picture' database based on the list picIds

                accomadationPackage.AccomadationPackagePictures = new List<AccomadationPackagePicture>(); // instantiate new 'AccomadationPackagePicture' list
                // add each picture id from Picture database on the AccomadationPackagePicture prop called PictureID
                accomadationPackage.AccomadationPackagePictures.AddRange(pictures.Select(x => new AccomadationPackagePicture {PictureID = x.ID }));

                result = AccomadationPackagesService.Instance.SaveAccomadationPackages(accomadationPackage); // save accomadationPackage in database
            }


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable perform action on accomadation package" };

            }

            return json;
        }

        #endregion

        #region DELETE
        [HttpGet]
        public ActionResult Delete(int ID)
        {

            AccomadationPackagesActionModel model = new AccomadationPackagesActionModel();

            var accomadationPackage = AccomadationPackagesService.Instance.GetAccomadationPackagesByID(ID); // get accomadation package based on ID

            model.ID = accomadationPackage.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomadationPackagesActionModel model)
        {

            JsonResult json = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            var accomadationPackage = AccomadationPackagesService.Instance.GetAccomadationPackagesByID(model.ID); // get accomadation package based on ID passed from model

            var result = AccomadationPackagesService.Instance.DeleteAccomadationPackages(accomadationPackage); // delete from database


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on accomadation package" };

            }

            return json;
        }
        #endregion



    }
}