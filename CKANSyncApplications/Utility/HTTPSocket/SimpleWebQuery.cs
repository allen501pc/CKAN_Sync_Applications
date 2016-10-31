using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CKANSyncApplications.Utility.HTTPSocket
{
    public class SimpleWebQuery
    {
        protected string url;
        public void SetURL(string url)
        {
            this.url = url;
        }

        public string GetURL()
        {
            return this.url;
        }

        public SimpleWebQuery()
        {

        }

        public SimpleWebQuery(string url)
        {
            SetURL(url);
        }

        public HttpWebResponse DoAction(string queryPath, string method, IDictionary<string, string> headers, IDictionary<string,object> postParameters)
        {
            string contentType = "application/x-www-form-urlencoded";
            string userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";
            string queryURL = this.url + "/" + queryPath;
            HttpWebResponse response = null;
            foreach (KeyValuePair<string, string> item in headers)
            {
                string key = item.Key.ToLower();
                if (key.Equals("content-type"))
                {
                    contentType = item.Value;
                }
                else if (key.Equals("user-agent"))
                {
                    userAgent = item.Value;
                }
            }
            switch (method.ToUpper())
            {
                case "POST":
                    method = "POST";
                    bool hasFileStream = false;                    
                    string[] pairs = postParameters.Select(x => string.Format("{0}={1}", x.Key, x.Value)).ToArray();
                    byte[] postData = Encoding.UTF8.GetBytes(string.Join("&", pairs));
                    foreach(object item in postParameters.Values) {
                        if (item is UploadPostData.FileParameter)
                        {
                            hasFileStream = true;
                            break;
                        }                        
                    }
                    if (hasFileStream)
                    {
                        response = UploadPostData.MultipartFormDataPost(queryURL, userAgent, headers, postParameters);
                    }
                    else
                    {
                        response = UploadPostData.PostForm(queryURL, userAgent, contentType,headers, postData);
                    }
                    
                    break;

                case "GET":
                    method = "GET";
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(queryURL);
                    httpWebRequest.ContentType = contentType;
                    httpWebRequest.UserAgent = userAgent;
                    httpWebRequest.KeepAlive = true;
                    httpWebRequest.Method = method;
                    if (headers != null)
                    {
                        foreach (KeyValuePair<string, string> item in headers)
                        {
                            httpWebRequest.Headers.Add(item.Key, item.Value);
                        }
                    }

                    response = httpWebRequest.GetResponse() as HttpWebResponse;
                    break;

                default:
                    method = "POST";
                    break;
            }

            return response;            
        }
    }
}
