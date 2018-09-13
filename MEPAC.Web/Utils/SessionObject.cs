using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace MEPAC.Web.Utils
{
    public class SessionObject : IHttpHandler, IRequiresSessionState
    {
        static HttpSessionState Session
        {
            get {
                if (HttpContext.Current == null)
                    throw new ApplicationException("No Session");
                return HttpContext.Current.Session;
            }
        }

        public bool IsReusable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public static T Get<T>(string key)
        {
            if (Session[key] == null)
                return default(T);

            return (T)Session[key];
        }

        public static void Set<T>(string key, T value)
        {
            Session[key] = value;
        }

        public static int GetInt(string key)
        {
            int s = Get<int>(key);
            return s;
        }

        public static void SetInt(string key, int value)
        {
            Set<int>(key, value);
        }

        public static string GetString(string key)
        {
            string s = Get<string>(key);
            return s;
        }

        public static void SetString(string key, string value)
        {
            Set<string>(key, value);
        }

       
    }
}