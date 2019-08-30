using HMS.Entities;
using HMS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    // this controller will contain shared methods, props and others between controllers
    public class SharedDashboardController : Controller
    {
        // GET: Dashboard/SharedDashboard
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UplaodPicture()
        {
            JsonResult result = new JsonResult {JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            List<Picture> picList = new List<Picture>(); // create empty list of Picture type

            var files = Request.Files; // get all the files sent to this method

            for (int i = 0; i < files.Count; i++)
            {
                var picture = files[i]; // store each file in var 'picture' through each loop iteration

                // 'Guid.NewGuid()' creates a random string which will be the name for the file
                // 'Path.GetExtension(picture.FileName)' will get that file extension and add it to the end of the file name
                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);

                // get the folder path where we want to store the images and add fileName to be saved in the file path
                // so instead of giving the whole path our self such as C:\Users\Nazir\Documents\Visual Studio\HMS\HMS\images\site. 'Server.MapPath' will do that for us based on '~/images/site'
                var filePath = Server.MapPath("~/images/site/") + fileName;

                picture.SaveAs(filePath); // save the picture in the file path

                //--------Add Pic to DB--------
                // create Picture object and set fileName to prop URL
                var dbPic = new Picture
                {
                    URL = fileName
                };


                if (SharedDashboardService.Instance.SavePicture(dbPic)) // if a pic gets upladoed to database then return true
                {
                    picList.Add(dbPic); // add each dbPic object to the list picList
                }
            }

            result.Data = picList; // add picList to the json Data object

            return result;
        }
    }
}