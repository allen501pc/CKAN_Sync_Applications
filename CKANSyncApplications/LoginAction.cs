using CKANSyncApplications.Utility.HTTPSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CKANSyncApplications
{
    /// <summary>
    /// This class is used to perform login action.
    /// </summary>
    public class LoginAction
    {
        protected SimpleWebQuery query = new SimpleWebQuery();

        public void SetURL(string url)
        {
            query.SetURL(url);
        }

        public string GetURL()
        {
            return query.GetURL();
        }

        /// <summary>
        /// Do login action. 
        /// </summary>
        /// <param name="requests">Dictionary object of keys and values.</param>
        /// <returns>HttpWebResponse</returns>
        public HttpWebResponse Login(IDictionary<string, string> headers, IDictionary<string, object> postParameters)
        {
            return query.DoAction("/login_generic?came_from=/user/logged_in", "POST", headers, postParameters);
        }

        /// <summary>
        /// Do logout action. 
        /// </summary>
        /// <param name="requests">Dictionary object of keys and values.</param>
        /// <returns>HttpWebResponse</returns>
        public HttpWebResponse Logout(IDictionary<string, string> headers)
        {
            return query.DoAction("/user/_logout", "GET", headers, null);
        }

        public HttpWebResponse GetUserList(IDictionary<string, string> headers)
        {
            return query.DoAction("/api/3/action/user_list", "GET", headers, null);
        }
    }
}
