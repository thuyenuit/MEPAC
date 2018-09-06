using MEPAC.Business.Business;
using MEPAC.Business.Common;
using MEPAC.Model.Models;
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
        IInformationBusiness _informationBusiness;
        public OtherController(IInformationBusiness _informationBusiness)
        {
            this._informationBusiness = _informationBusiness;
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

        [Route("getInforCompany")]
        [HttpGet]
        public HttpResponseMessage GetInforCompany(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var obj = _informationBusiness.GetAll().FirstOrDefault();

                var response = request.CreateResponse(HttpStatusCode.OK, obj);
                return response;
            });
        }

        [Route("updateInforCompany")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, Information info)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
                }
                else
                {
                    try
                    {
                        List<string> lstError = new List<string>();
                        if (info.InformationID <= 0)
                        {
                            lstError.Add("Thông tin không tồn tại");
                        }

                        if (string.IsNullOrEmpty(info.Content))
                        {
                            lstError.Add("Vui lòng nhập thông tin giới thiệu");
                        }

                        _informationBusiness.Update(info);
                        response = request.CreateResponse(HttpStatusCode.OK, "Cập nhật thành công");
                    }
                    catch (Exception ex)
                    {
                        response = request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                    }

                }

                return response;
            });
        }
    }
}
