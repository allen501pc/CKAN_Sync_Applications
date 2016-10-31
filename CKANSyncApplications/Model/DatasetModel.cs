using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKANSyncApplications.Model
{
    public class DatasetModel
    {
        /// <summary>
        /// The serial number in the client's database. It doesn't represent the actual resource id in the CKAN.
        /// </summary>
        public int id = -1;

        /// <summary>
        /// The visible state.
        /// 0 => False.
        /// 1 => True
        /// </summary>
        public short visible = 0;

        /// <summary>
        /// The number of resources. 
        /// </summary>
        public int numberOfResources = 0;

        /// <summary>
        /// The resource id recorded in the remote CKAN platform.
        /// </summary>
        public string  resourceId = String.Empty;

        /// <summary>
        /// The sync state.
        /// </summary>
        public string syncState = String.Empty;
        
        public string log = String.Empty;
        
        public string organizationResourceId = String.Empty;
        
        public string licenseResourceId = String.Empty;
        
        public string description = String.Empty;
        
        public string url = String.Empty;
        
        public string tags = String.Empty;
        
        public string authorEmail = String.Empty;

        public string maintainer = String.Empty;

        /// <summary>
        /// THe URL's name of this dataset.
        /// </summary>
        public string name = String.Empty;

        /// <summary>
        /// The note is only stored in the client but not in any field of the dataset on the CKAN platform. 
        /// </summary>
        public string note;

        /// <summary>
        /// The title field represents the folder name. The folder name is also the title of the dataset. 
        /// </summary>
        public string title;        

        public DateTime syncTime;

        public DateTime lastUpdateTime; 

    }
}
