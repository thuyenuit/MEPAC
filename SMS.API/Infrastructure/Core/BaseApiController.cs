using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SMS.API.Infrastructure.Core
{
    public class BaseApiController : ApiController
    {
        public BaseApiController()
        {
           
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage reponse = null;

            try
            {
                reponse = function.Invoke();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has thư following validation error.");

                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"Property: \"{ve.PropertyName}\" in state \"{ve.ErrorMessage}\"");
                    }
                }

                reponse = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                reponse = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
                throw new Exception(dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                reponse = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                throw new Exception(ex.Message);
            }

            return reponse;
        }
    }

    //public class BaseRequestAPI {
    //   /* public string UserID {
    //        get { return (HttpContext.Current.Session["USER_ID"] != null ? (string)HttpContext.Current.Session["USER_ID"] : null); }
    //    }
    //    public string FullName { 
    //        get { return (HttpContext.Current.Session["USER_FULLNAME"] != null ? (string)HttpContext.Current.Session["USER_FULLNAME"] : null); }
    //    }
    //    public string UserAccount {
    //        get { return (HttpContext.Current.Session["USER_ACCOUNT"] != null ? (string)HttpContext.Current.Session["USER_ACCOUNT"] : null); }
    //    }*/
    //}

    //public class BaseResponseMsgAPI {
    //    public int StatusCode { get; set; }
    //    public bool IsSuccess { get; set; }
    //    public string Mesage { get; set; }
    //   // public List<ResponseData> Data { get; set; }
    //}

    //public class ResponseData { 
    //    //public List<T> ListData { get; set; }
    //}
}
