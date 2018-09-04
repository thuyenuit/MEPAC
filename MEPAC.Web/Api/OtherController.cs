using MEPAC.Business.Common;
using MEPAC.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MEPAC.Web.Api
{
    [RoutePrefix("api/other")]
    [Authorize]
    public class OtherController : BaseApiController
    {
        public OtherController()
        {
        }

        [Route("getListStatus")]
        [HttpGet]
        public HttpResponseMessage GetListStatus(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                List<Status> lstStatus = new List<Status>();
                lstStatus.Add(new Status() { StatusID = 1 , StatusName = "Đang hoạt động" });
                lstStatus.Add(new Status() { StatusID = 2 , StatusName = "Đã hủy" });

                var response = request.CreateResponse(HttpStatusCode.OK, lstStatus);
                return response;
            });
        }
    }
}
