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
    public class DatasetAccess : IDBAccess, IRemoteList
    {
        private static IDictionary<string, DatasetModel> list = new Dictionary<string, DatasetModel>();

        private static string tableName = "dataset";

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
            HttpWebResponse response = myRequest.DoAction(@"/api/3/action/package_list", "GET", headers, null);
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            string result = reader.ReadToEnd();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IDictionary<string, object> deserializedResult = serializer.Deserialize<IDictionary<string, object>>(result);
            if (deserializedResult["success"].ToString().Equals("true"))
            {
                if (deserializedResult["result"] is IDictionary<string, object>)
                {
                    IDictionary<string, string> results = (deserializedResult["result"] as IDictionary<string, string>);
                    foreach (string item in results.Values)
                    {
                        list.Add(item);
                    }
                }
            }
            stream.Dispose();
            reader.Dispose();
        }

        public DatasetModel GetDataModelByPackageName(string url, string apiKey, string name)
        {
            if (String.IsNullOrEmpty(apiKey))
            {
                throw new Exception("The api key is not installed.");
            }
            if (String.IsNullOrEmpty(name))
            {
                throw new Exception("The input package name is empty.");
            }

            IDictionary<string, string> headers = new Dictionary<string, string>();
            headers["Authorization"] = apiKey;

            SimpleWebQuery myRequest = new SimpleWebQuery(url);
            HttpWebResponse response = myRequest.DoAction(String.Format(@"/api/3/action/package_show?id={0}", name), "GET", headers, null);
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            string result = reader.ReadToEnd();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IDictionary<string, object> deserializedResult = serializer.Deserialize<IDictionary<string, object>>(result);
            if (deserializedResult["success"].ToString().Equals("true"))
            {
                if (deserializedResult["result"] is IDictionary<string, object>)
                {
                    IDictionary<string, string> results = (deserializedResult["result"] as IDictionary<string, string>);                    
                    DatasetModel model = new DatasetModel();

                    foreach (KeyValuePair<string,string> item in results)
                    {
                        switch (item.Key)
                        {
                            case "maintainer":
                                model.maintainer = item.Value;
                                break;

                            case "author_email":
                                model.authorEmail = item.Value;
                                break;

                            case "license_id":
                                model.licenseResourceId = item.Value;
                                break;

                            case "name":
                                model.name = item.Value;
                                break;
                            case "num_resources":
                                model.numberOfResources = Convert.ToInt32(item.Value);
                                break;

                            case "title":
                                model.title = item.Value;
                                break;

                            case "notes":
                                model.note = item.Value;
                            
                        }
                        list.Add(item);
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
                    mycommand.CommandText = String.Format("SELECT resource_id, display_name FROM {0}", tableName);
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
