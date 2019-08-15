using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data
{
    public class HMSContext : DbContext
    {
        public HMSContext() : base("HMSConnectionString")
        {

        }

        public DbSet<AccomadationType> AccomadationType { get; set; }
        public DbSet<AccomadationPackage> AccomadationPackage { get; set; }
        public DbSet<Accomadation> Accomadation { get; set; }
        public DbSet<Booking> Booking { get; set; }

    }
}
