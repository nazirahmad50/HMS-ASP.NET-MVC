using HMS.Entities;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccomadationsListingViewModel
    {
        public IEnumerable<Accomadation> Accomadation { get; set; }
        public IEnumerable<AccomadationPackage> AccomadationPackage { get; set; }
        public int? AccomadationPackageID { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
        public int TotalRecords { get; set; }
    }

    public class AccomadationsActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int AccomadationPackageID { get; set; }
        public AccomadationPackage AccomadationPackage { get; set; } // AccomadationPackage object
        public IEnumerable<AccomadationPackage> AccomadationPackages { get; set; } // AccomadationPackage object

    }
}