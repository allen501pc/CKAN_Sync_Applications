using CKANSyncApplications.Utility.Security;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace CKANSyncApplications.Settings
{
    public class BasicSettings : CKANSyncApplications.Model.IDBAccess
    {
        public string url = String.Empty;

        public string account = String.Empty;

        /// <summary>
        /// The password stored in the DB should be encrypted.
        /// </summary>
        public string password = String.Empty;

        /// <summary>
        /// The api key stored in the DB should be encrypted.
        /// </summary>
        public string apiKey = String.Empty;

        public string cookie = String.Empty;

        public string rootFolderPath = String.Empty;

        public string displayName = String.Empty;

        public string defaultOrganizationResourceID = String.Empty;

        public string defaultLicenseResourceID = String.Empty;

        public string email = String.Empty;

        /// <summary>
        /// The organization list which the current user has access permission.
        /// [key]-[value] => [resource_id]-[display_name]
        /// </summary>
        // public IDictionary<string, string> organizationList = new Dictionary<string, string>();

        /// <summary>
        /// The license list.
        /// </summary>
        // public IDictionary<string, string> licenseList = new Dictionary<string, string>();

        public int syncable = 1;

        private string tableName = @"settings";

        public void ObtainSettingsFromRemote(string account, string password)
        {
            LoginAction loginObject = new LoginAction();
            loginObject.SetURL(url);
            // Prepare the needed information to login.
            IDictionary<string, string> headers = new Dictionary<string, string>();
            IDictionary<string, object> postParameters = new Dictionary<string, object>();
            headers["Content-Type"] = "application/x-www-form-urlencoded";
            postParameters["login"] = account.Replace("'", "\\'");
            postParameters["password"] = password.Replace("'", "\\'");
            
            HttpWebResponse response = loginObject.Login(headers, postParameters);
            if (response != null)
            {
                cookie = response.Headers["Set-Cookie"].ToString();
                headers["Cookie"] = cookie;

                response = loginObject.GetUserList(headers);
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                string result = reader.ReadToEnd();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                IDictionary<string, object> deserializedResult = serializer.Deserialize<IDictionary<string, object>>(result);
                if (deserializedResult["success"].ToString().Equals("true"))
                {
                    if (deserializedResult["result"] is IDictionary<string, object>)
                    {
                        IDictionary<string, IDictionary<string, string>> results = (deserializedResult["result"] as IDictionary<string, IDictionary<string, string>>);
                        foreach (IDictionary<string, string> item in results.Values)
                        {
                            if (item.ContainsKey("name") && item["name"].Equals(account))
                            {
                                if (item.ContainsKey("apikey"))
                                {
                                    this.apiKey = item["apikey"];
                                }

                                if (results.ContainsKey("display_name"))
                                {
                                    this.displayName = item["display_name"];
                                }

                                if (results.ContainsKey("email"))
                                {
                                    this.email = item["email"];
                                }

                                break;
                            }
                        }
                    }
                    loginObject.Logout(headers);
                }
                stream.Dispose();
                reader.Dispose();
            }
            else
            {
                throw new Exception("It's unable to connect the CKAN platform.");
            }          
        }

        public void ObtainFromDB()
        {

            using (SQLiteConnection myconnection = new SQLiteConnection(SystemParameters.ConnectionString))
            {                
                myconnection.Open();

                using (SQLiteCommand mycommand = new SQLiteCommand(myconnection))
                {
                    mycommand.CommandText = String.Format("SELECT * FROM {0}", tableName);
                    SQLiteDataReader dataReader = mycommand.ExecuteReader();
                    if (dataReader.Read())
                    {
                        if (dataReader["syncable"].ToString().Equals("1"))
                        {
                            this.syncable = 1;
                        }
                        else
                        {
                            this.syncable = 0;
                        }
                        
                        this.account = dataReader["account"].ToString();
                        this.password = DataProtection.Decrypt(dataReader["password"].ToString());
                        this.url = dataReader["url"].ToString();
                        this.apiKey = DataProtection.Decrypt(dataReader["api_key"].ToString());
                        this.rootFolderPath = dataReader["root_folder_path"].ToString();
                        this.displayName = dataReader["display_name"].ToString();
                        this.defaultOrganizationResourceID = dataReader["default_organization_resource_id"].ToString();
                        this.defaultLicenseResourceID = dataReader["default_license_resource_id"].ToString();
                    }
                }
                myconnection.Close();
            }
        }

        public bool WriteToDB()
        {
            int afftectedRows = 0;
            using (SQLiteConnection myconnection = new SQLiteConnection(SystemParameters.ConnectionString))
            {
                myconnection.Open();

                using (SQLiteCommand mycommand = new SQLiteCommand(myconnection))
                {
                    mycommand.CommandText = String.Format("DELETE FROM {0}", tableName);
                    mycommand.ExecuteNonQuery();
                    string text = @"INSERT INTO {0} (account, password, url, api_key, 
                                              root_folder_path, display_name, default_organization_resource_id, default_license_resource_id) VALUES ('"
                                              + this.account + "','" + DataProtection.Encrypt(this.password) + "','"
                                              + this.url + "','" + DataProtection.Encrypt(this.apiKey) + "','" + this.rootFolderPath + "','"
                                              + this.displayName + "','" + this.defaultOrganizationResourceID + "','" + this.defaultLicenseResourceID + "')";
                    mycommand.CommandText = String.Format(text, tableName);
                    afftectedRows = mycommand.ExecuteNonQuery();
                }
                myconnection.Close();
            }
            return (afftectedRows == 1) ? true : false;
        }
    }
}
