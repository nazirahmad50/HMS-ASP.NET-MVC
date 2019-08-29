using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class AccomadationsViewModel
    {
        public AccomadationType AccomadationType { get; set; }

        public IEnumerable<AccomadationPackage> AccomadationPackages { get; set; }
        public IEnumerable<Accomadation> Accomadations { get; set; }
        public int SelectedAccomadationPackageID { get; set; }
    }
}