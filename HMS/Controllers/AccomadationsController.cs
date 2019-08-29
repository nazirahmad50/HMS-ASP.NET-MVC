using HMS.Services;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class AccomadationsController : Controller
    {
        public ActionResult Index(int accomadationTypeID, int? accomadationPackageID)
        {
            AccomadationsViewModel model = new AccomadationsViewModel();

            //model.AccomadationType = AccomadationTypesService.Instance.GetAccomadationTypesByID(accomadationTypeID); // get accomadation types based on param accomadationTypeID

            // get AccomadationPackage based on param accomadationTypeID
            model.AccomadationPackages = AccomadationPackagesService.Instance.GetAllAccomadationPackagesByAccomadationType(accomadationTypeID);

            // if param accomadationPackageID is null then get the first AccomadationPackage id otherwise get the param accomadationPackageID value
            model.SelectedAccomadationPackageID = accomadationPackageID ?? model.AccomadationPackages.First().ID;

            // get Accomadations based on the SelectedAccomadationPackageID
            model.Accomadations = AccomadationsServices.Instance.GetAllAccomadationsByAccomadationPackage(model.SelectedAccomadationPackageID);

       
            return View(model);
        }
    }
}