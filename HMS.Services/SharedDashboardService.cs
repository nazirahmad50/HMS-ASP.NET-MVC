using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class SharedDashboardService
    {
        #region Singleton
        public static SharedDashboardService Instance
        {
            get
            {
                // 'instance' doesnt exist in memory then create 'instance' obejct
                if (instance == null) instance = new SharedDashboardService();

                // if its not null just return 'instance'
                return instance;
            }
        }

        private static SharedDashboardService instance { get; set; }

        private SharedDashboardService()
        {

        }
        #endregion


        public bool SavePicture(Picture picture)
        {
            var context = new HMSContext();

            context.Picture.Add(picture);

            return context.SaveChanges() > 0;
        }
    }
}
