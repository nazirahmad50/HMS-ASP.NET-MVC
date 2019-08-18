using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccomadationTypesService
    {
        #region Singleton
        public static AccomadationTypesService Instance
        {
            get
            {
                // 'instance' doesnt exist in memory then create 'instance' obejct
                if (instance == null) instance = new AccomadationTypesService();

                // if its not null just return 'instance'
                return instance;
            }
        }

        private static AccomadationTypesService instance { get; set; }

        private AccomadationTypesService()
        {

        }
        #endregion

        /* using 'IEnumerable<>' instead of 'List<>' because List converts all the table values into a list
         * whereas IEnumerable converts the values into a list when its used in a foreach loop */

        /* before IEnumerable is being used in foreach loop it returns the query only such as 'Select * from AccomadationType'
         * whereas the 'List<>' converts the query into a list inside this method before its even used in foreach loop */
        // so if there are a lot of records in table then 'IEnumerable' will do better inperformance than 'List'
        public IEnumerable<AccomadationType> GetAllAccomadationTypes() // we cants use 'using' statement with IEnumerable
        {

            var context = new HMSContext();

            // 'context.AccomadationType' means select * from AccomadationType
            return context.AccomadationType.AsEnumerable(); 


        }

        public bool SaveAccomadationTypes(AccomadationType accomadationType) 
        {

            var context = new HMSContext();

            context.AccomadationType.Add(accomadationType); // add param accomadationType values to table AccomadationType

            // 'SaveChanges()' func returns a bool
            return context.SaveChanges() > 0; // if some changes has been made then return true


        }

    }
}
