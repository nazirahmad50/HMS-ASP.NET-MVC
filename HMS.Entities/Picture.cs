using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    public class Picture // as we want to upload multiple pictures so we created a sperate entity for Picture
    {
        public int ID { get; set; }
        public string URL { get; set; }
    }
}
