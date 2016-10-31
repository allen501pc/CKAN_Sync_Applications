using CKANSyncApplications.Settings;
using CKANSyncApplications.Utility.HTTPSocket;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CKANSyncApplications.Model
{
    public class LicenseAccess : IDBAccess, IRemoteList
    {

        private static IDictionary<string, string> list = new Dictionary<string, string>();
        private static string tableName = "license";        

        public IDictionary<string, object> GetList()
        {
            return list as IDictionary<string, object>;
        }

        public void ObtainFromRemote(string url, string apiKey)
        {
            list.Clear();
            if (String.IsNullOrEmpty(apiKey))
            {
                throw new Exception("The api key is not installed.");
            }
            IDictionary<string, string> headers = new Dictionary<string, string>();
            headers["Authorization"] = apiKey;

            SimpleWebQuery myRequest = new SimpleWebQuery(url);
            HttpWebResponse response = myRequest.DoAction(@"/api/3/action/license_list", "GET", headers, null);
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
                        list.Add(item["id"], item["title"]);
                    }
                }
            }
            stream.Dispose();
            reader.Dispose();            
        }


        public void ObtainFromDB()
        {
            using (SQLiteConnection myconnection = new SQLiteConnection(SystemParameters.ConnectionString))
            {
                myconnection.Open();

                using (SQLiteCommand mycommand = new SQLiteCommand(myconnection))
                {
                    list.Clear();
                    mycommand.CommandText = String.Format("SELECT resource_id, display_name FROM {0}",tableName);
                    SQLiteDataReader dataReader = mycommand.ExecuteReader();
                    if (dataReader.Read())
                    {

                        list.Add(dataReader["resource_id"].ToString(), dataReader["display_name"].ToString());
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
                using (SQLiteTransaction tr = myconnection.BeginTransaction())
                {
                    using (SQLiteCommand mycommand = new SQLiteCommand(myconnection))
                    {
                        mycommand.Transaction = tr;
                        mycommand.CommandText = String.Format("DELETE FROM {0}", tableName);
                        mycommand.ExecuteNonQuery();
                        foreach (KeyValuePair<string, string> item in list)
                        {
                            mycommand.CommandText = String.Format("INSERT INTO {0} (resource_id, display_name) VALUES ('{1}','{2}')", tableName, item.Key, item.Value);
                            afftectedRows += mycommand.ExecuteNonQuery();
                        }
                        tr.Commit();
                    }
                }
                myconnection.Close();
            }
            return (afftectedRows == list.Count()) ? true : false;
        }
    }
}
