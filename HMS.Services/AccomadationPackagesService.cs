﻿using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccomadationPackagesService
    {
        #region Singleton
        public static AccomadationPackagesService Instance
        {
            get
            {
                // 'instance' doesnt exist in memory then create 'instance' obejct
                if (instance == null) instance = new AccomadationPackagesService();

                // if its not null just return 'instance'
                return instance;
            }
        }

        private static AccomadationPackagesService instance { get; set; }

        private AccomadationPackagesService()
        {

        }
        #endregion

        #region CRUD

       
        public IEnumerable<AccomadationPackage> SearchAccomadationPackages(string searchTerm, int? accomadationTypeID, int pageSize, int page) // we cants use 'using' statement with IEnumerable
        {

            var context = new HMSContext();

            var accomadationPackage = context.AccomadationPackage.AsQueryable(); // 'AsQueryable' will allow us to use query on the AccomadationType such as using 'Where' on it

            // find based on searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // check if the searchterm exist in the database column Name
                accomadationPackage = accomadationPackage.Where(x => x.Name != null && x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            // find based on accomadationTypeID
            if (accomadationTypeID.HasValue && accomadationTypeID.Value > 0)
            {
                // check if the searchterm exist in the database column Name
                accomadationPackage = accomadationPackage.Where(x => x.AccomadationTypeID == accomadationTypeID.Value);
            }

            // skip = (1 - 1) * 3 = 0 * 3 = 0
            // skip = (2 - 1) * 3 = 1 * 3 = 3
            // skip = (3 - 1) * 3 = 2 * 3 = 6
            var skip = (page - 1) * pageSize;

            return accomadationPackage.OrderBy(x=>x.AccomadationTypeID).Skip(skip).Take(pageSize).AsEnumerable(); // we have to use the 'sortBy' if we are going to use 'Skip'


        }

        public AccomadationPackage GetAccomadationPackagesByID(int ID)
        {
          
                var context = new HMSContext();
     
             return context.AccomadationPackage.Find(ID);

            


        }

        public bool SaveAccomadationPackages(AccomadationPackage accomadationPackage)
        {

            var context = new HMSContext();

            context.AccomadationPackage.Add(accomadationPackage); // add param accomadationPackage values to table AccomadationPackage

            // 'SaveChanges()' func returns a bool
            return context.SaveChanges() > 0; // if some changes has been made then return true


        }

        public bool UpdateAccomadationPackages(AccomadationPackage accomadationPackage)
        {

            var context = new HMSContext();

            var existingAccomadationpackage = context.AccomadationPackage.Find(accomadationPackage.ID); // find existing accomadatioPackages

            // remove exisitng AccomadationPackage Pictures from db AccomadationPackagePictures 
            context.AccomadationPackagePicture.RemoveRange(existingAccomadationpackage.AccomadationPackagePictures); 

            // set the current values for existingAccomadationpackage, this does not include other objects such as AccomadationPackagePicture
            context.Entry(existingAccomadationpackage).CurrentValues.SetValues(accomadationPackage); 

            context.AccomadationPackagePicture.AddRange(accomadationPackage.AccomadationPackagePictures); // add AccomadationPackagePictures from param accomadationPackage to db AccomadationPackagePicture

            return context.SaveChanges() > 0;


        }

        public bool DeleteAccomadationPackages(AccomadationPackage accomadationPackage)
        {


            var context = new HMSContext();

            var existingAccomadationpackage = context.AccomadationPackage.Find(accomadationPackage.ID); // find existing accomadatioPackages

            // remove exisitng AccomadationPackage Pictures from db AccomadationPackagePictures 
            context.AccomadationPackagePicture.RemoveRange(existingAccomadationpackage.AccomadationPackagePictures);

            context.Entry(existingAccomadationpackage).State = System.Data.Entity.EntityState.Deleted; // delete accomadation package 

            return context.SaveChanges() > 0;


        }
        #endregion

        public int SearchAccomadationPackagesCount(string searchTerm, int? accomadationTypeID) 
        {

            var context = new HMSContext();

            var accomadationPackage = context.AccomadationPackage.AsQueryable(); // 'AsQueryable' will allow us to use query on the AccomadationType such as using 'Where' on it

            // find based on searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // check if the searchterm exist in the database column Name
                accomadationPackage = accomadationPackage.Where(x => x.Name != null && x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            // find based on accomadationTypeID
            if (accomadationTypeID.HasValue && accomadationTypeID.Value > 0)
            {
                // check if the searchterm exist in the database column Name
                accomadationPackage = accomadationPackage.Where(x => x.AccomadationTypeID == accomadationTypeID.Value);
            }



            return accomadationPackage.Count();


        }

        public IEnumerable<AccomadationPackage> GetAllAccomadationPackages()
        {

            var context = new HMSContext();

            return context.AccomadationPackage.AsEnumerable();


        }

        #region Front End

        public IEnumerable<AccomadationPackage> GetAllAccomadationPackagesByAccomadationType(int accomadationtypeID)
        {

            var context = new HMSContext();

            return context.AccomadationPackage.Where(x=>x.AccomadationTypeID == accomadationtypeID).AsEnumerable();


        }


        #endregion

        #region Pictures

        public List<AccomadationPackagePicture> GetPicturesByAccomadationPackageID(int accomadationtypePckageID)
        {

            var context = new HMSContext();

            return context.AccomadationPackage.Find(accomadationtypePckageID).AccomadationPackagePictures.ToList();


        }

        #endregion
    }
}
