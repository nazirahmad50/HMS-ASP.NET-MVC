﻿using HMS.Services;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                AccomadationTypes = AccomadationTypesService.Instance.GetAllAccomadationTypes(), // get all accomadation Types
                AccomadationPackages = AccomadationPackagesService.Instance.GetAllAccomadationPackages()
            };

            return View(model);
        }

     
    }
}