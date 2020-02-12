using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SMS.API.Infrastructure.Core
{
    public sealed class LogonAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //kiem tra action ap dung co su dung AllowAnonymousAttribute ko?
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            //neu action khong ap dung AllowAnonymousAttribute thi bat buoc phai dang nhap
            if (!skipAuthorization)
            {
                bool isNewSession = filterContext.HttpContext.Session.IsNewSession;
                //neu ko phai Session moi
                if (!isNewSession)
                {
                    CoreLogOnAuthorize(filterContext);
                }
                else
                {
                    // if session is newSession
                    HttpCookie sessionCookie = filterContext.HttpContext.Request.Cookies["ASP.NET_SessionId"];
                    if ((null != sessionCookie) && !string.IsNullOrEmpty(sessionCookie.Value) && !filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        /* Session Timeout! */
                        FormsAuthentication.SignOut(); //Removes the forms-authentication ticket from the browser
                        filterContext.HttpContext.Session.Abandon();
                        filterContext.Result = new HttpUnauthorizedResult(); //redirect trang dang nhap
                    }
                    else
                    {
                        // (null == sessionCookie) && string.IsNullOrEmpty(sessionookie.Value)
                        if (!filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            // Cookie didn't exist - must be a brand new login
                            //CoreLogOnAuthorize(filterContext);
                            filterContext.HttpContext.Session.Abandon();
                            filterContext.Result = new HttpUnauthorizedResult();
                        }
                        else
                        {
                            //CoreLogOnAuthorize(filterContext);
                            //neu la ajax request
                            FormsAuthentication.SignOut(); //Removes the forms-authentication ticket from the browser
                            filterContext.HttpContext.Session.Abandon();
                            filterContext.HttpContext.Response.Clear();
                            filterContext.HttpContext.Response.StatusCode = 454;
                            filterContext.HttpContext.Response.End();
                        }

                    }

                }
            }
        }

        private void CoreLogOnAuthorize(AuthorizationContext filterContext)
        {
            //kiem tra neu user chua dang nhap va request la ajax request
            if (!filterContext.HttpContext.Request.IsAuthenticated && filterContext.HttpContext.Request.IsAjaxRequest())
            {
                //xu ly ajax request


                //xoa Respone tra ve va dat statusCode = 451
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = 451;
                filterContext.HttpContext.Response.End();

                //When this filter detects an AJAX request, it responds with JSON data that reports the problem 
                /*
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);

                JsonMessage jm = new JsonMessage("Message", "ERROR");
                filterContext.Result = new JsonResult
                {
                    Data = jm,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                */
                //throw new AjaxUnAuthorizeException();
            }
            else base.OnAuthorization(filterContext);
        }
    }
}