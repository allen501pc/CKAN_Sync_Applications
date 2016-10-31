using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKANSyncApplications.Settings
{
    public class SystemParameters
    {
        private static string connectionString = "Data Source=" + System.Configuration.ConfigurationManager.AppSettings["dbFileName"];

        public static string ConnectionString
        {
            get { return connectionString; }
        }
    }
}
