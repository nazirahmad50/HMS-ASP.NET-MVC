using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    /// <summary>
    /// Aprtment No. or Hotel Room No. based on Accomadation Package
    /// </summary>
    public class Accomadation
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int AccomadationPackageID { get; set; }
        public virtual AccomadationPackage AccomadationPackage { get; set; } // AccomadationPackage object
    }
}
