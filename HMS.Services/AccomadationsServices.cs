using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccomadationsServices
    {
        #region Singleton
        public static AccomadationsServices Instance
        {
            get
            {
                // 'instance' doesnt exist in memory then create 'instance' obejct
                if (instance == null) instance = new AccomadationsServices();

                // if its not null just return 'instance'
                return instance;
            }
        }

        private static AccomadationsServices instance { get; set; }

        private AccomadationsServices()
        {

        }
        #endregion


        #region CRUD

        public IEnumerable<Accomadation> SearchAccomadations(string searchTerm, int? accomadationPackageID, int pageSize, int page)
        {

            var context = new HMSContext();

            var accomadation = context.Accomadation.AsQueryable(); // 'AsQueryable' will allow us to use query on the AccomadationType such as using 'Where' on it

            // find based on searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // check if the searchterm exist in the database column Name
                accomadation = accomadation.Where(x => x.Name != null && x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            // find based on accomadationTypeID
            if (accomadationPackageID.HasValue && accomadationPackageID.Value > 0)
            {
                // check if the searchterm exist in the database column Name
                accomadation = accomadation.Where(x => x.AccomadationPackageID == accomadationPackageID.Value);
            }

            // skip = (1 - 1) * 3 = 0 * 3 = 0
            // skip = (2 - 1) * 3 = 1 * 3 = 3
            // skip = (3 - 1) * 3 = 2 * 3 = 6
            var skip = (page - 1) * pageSize;

            return accomadation.OrderBy(x => x.AccomadationPackageID).Skip(skip).Take(pageSize).AsEnumerable(); // we have to use the 'sortBy' if we are going to use 'Skip'


        }

        public Accomadation GetAccomadationsByID(int ID) 
        {
            // in Post Delete method we are calling 'GetAccomadationsByID' 'DeleteAccomadations' services one after another
            // so we have to dispose of one of these context as we are calling two services in the post Delete method
            using (var context = new HMSContext()) 
            {
                return context.Accomadation.Find(ID);

            }

        }

     

        public bool SaveAccomadations(Accomadation accomadation) 
        {

            var context = new HMSContext();

            context.Accomadation.Add(accomadation); // add param accomadationType values to table AccomadationType

            // 'SaveChanges()' func returns a bool
            return context.SaveChanges() > 0; // if some changes has been made then return true


        }

        public bool UpdateAccomadations(Accomadation accomadation)
        {

            var context = new HMSContext();

            context.Entry(accomadation).State = System.Data.Entity.EntityState.Modified; // modify the accomadation type 

            return context.SaveChanges() > 0; 


        }

        public bool DeleteAccomadations(Accomadation accomadation)
        {

            var context = new HMSContext();

            context.Entry(accomadation).State = System.Data.Entity.EntityState.Deleted; // delete accomadation type 

            return context.SaveChanges() > 0;


        }
        #endregion

        public int SearchAccomadationsCount(string searchTerm, int? accomadationTypeID)
        {

            var context = new HMSContext();

            var accomadation = context.Accomadation.AsQueryable(); // 'AsQueryable' will allow us to use query on the AccomadationType such as using 'Where' on it

            // find based on searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // check if the searchterm exist in the database column Name
                accomadation = accomadation.Where(x => x.Name != null && x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            // find based on accomadationTypeID
            if (accomadationTypeID.HasValue && accomadationTypeID.Value > 0)
            {
                // check if the searchterm exist in the database column Name
                accomadation = accomadation.Where(x => x.AccomadationPackageID == accomadationTypeID.Value);
            }



            return accomadation.Count();


        }

    }
}
