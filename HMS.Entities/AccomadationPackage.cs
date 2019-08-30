using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    /// <summary>
    /// Types of Hotel Room and Appartment
    /// </summary>
    public class AccomadationPackage
    {
        public int ID { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }
        public string Name { get; set; }

        public int AccomadationTypeID { get; set; }
        // virtual allows us to access the props of the AccomadationType object
        public virtual AccomadationType AccomadationType { get; set; } // AccomadationType object

        public List<AccomadationPackagePicture> AccomadationPackagePictures { get; set; }



    }
}
