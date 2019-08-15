using HMS.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data
{
    // inherit from 'IdentityDbContext' instead of 'DBContext' because we want to use Users as well
    // Through Identity Framework Microsoft will create all the necessary tables
    public class HMSContext : IdentityDbContext<HMSUser>
    {
        public HMSContext() : base("HMSConnectionString")
        {

        }

        public static HMSContext Create()
        {
            return new HMSContext();
        }

        // Tables in database
        public DbSet<AccomadationType> AccomadationType { get; set; }
        public DbSet<AccomadationPackage> AccomadationPackage { get; set; }
        public DbSet<Accomadation> Accomadation { get; set; }
        public DbSet<Booking> Booking { get; set; }


    }
}
