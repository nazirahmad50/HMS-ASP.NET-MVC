﻿using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccomadationPackagesListingModel
    {
        public IEnumerable<AccomadationPackage> AccomadationPackage { get; set; }
        public string SearchTerm { get; set; }
    }

    public class AccomadationPackagesActionModel
    {
        public int ID { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }
        public string Name { get; set; }

        public int AccomadationTypeID { get; set; }
        public AccomadationType AccomadationType { get; set; }
        public IEnumerable<AccomadationType>  AccomadationTypes { get; set; } // AccomadationType object
    }

}