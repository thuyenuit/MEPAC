using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.API.Infrastructure.Core
{
    public class Global
    {
        public Global() { }

        public string UserID {
            get {
                return (HttpContext.Current.Session["USER_ID"] != null ? (string)HttpContext.Current.Session["USER_ID"] : null);
            }
        }
        public string UserAccount {
            get {
                return (HttpContext.Current.Session["USER_ACCOUNT"] != null ? (string)HttpContext.Current.Session["USER_ACCOUNT"] : null);
            }
        }
        public string UserFullName {
            get {
                return (HttpContext.Current.Session["USER_FULLNAME"] != null ? (string)HttpContext.Current.Session["USER_FULLNAME"] : null);
            }
        }

    }
}