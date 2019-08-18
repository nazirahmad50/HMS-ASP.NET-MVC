using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccomadationTypesListingModel
    {
        public IEnumerable<AccomadationType> AccomadationType { get; set; }
    }

    public class AccomadationTypesActionModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

    }
}