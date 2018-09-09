using MEPAC.Business;
using MEPAC.Business.Business;
using MEPAC.Model.Models;
using MEPAC.Web.Infrastructure.Core;
using MEPAC.Web.Models;
using MEPAC.Web.Provider;
using MEPAC.WebAdmin.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace MEPAC.Web.Api
{
    [RoutePrefix("api/partner")]
    [Authorize]
    public class PartnerController : BaseApiController
    {
        IPartnerBusiness PartnerBusiness;
        public PartnerController(
            IPartnerBusiness PartnerBusiness)
        {
            this.PartnerBusiness = PartnerBusiness;
        }

        [Route("search")]
        [HttpGet]
        public HttpResponseMessage Search(HttpRequestMessage request,
            int page, int pageSize, string keyWord, int status)
        {
            return CreateHttpResponse(request, () =>
            {
                IDictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("KeyWord", keyWord);
                dic.Add("Status", status);
                List<Partner> lstPartnerDB = PartnerBusiness.Search(dic).ToList();

                List<PartnerViewModel> lstPartnerVM = lstPartnerDB.Select(x => new PartnerViewModel()
                {
                    Address = x.Address,
                    CreateBy = x.CreateBy,
                    CreateDate = x.CreateDate,
                    Display = x.Display,
                    Email = x.Email,
                    IsActive = x.IsActive,
                    Logo = x.Logo,
                    MetaDescription = x.MetaDescription,
                    MetaKeyword = x.MetaKeyword,
                    Order = x.Order,
                    PartID = x.PartID,
                    Phone = x.Phone,
                    UpdateBy = x.UpdateBy,
                    UpdateDate = x.UpdateDate,
                    Website = x.Website,
                }).ToList();


                lstPartnerVM = lstPartnerVM.OrderByDescending(x => x.CreateDate).ThenBy(x => x.Display).ToList();
                int totalRow = lstPartnerVM.Count();
                List<PartnerViewModel> lstResult = lstPartnerVM.Skip((page) * pageSize).Take(pageSize).ToList();


                var paginationset = new PaginationSet<PartnerViewModel>()
                {
                    Items = lstResult.AsEnumerable(),
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                };

                var response = request.CreateResponse(HttpStatusCode.OK, paginationset);
                return response;
            });
        }

        [Route("getbyid")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int partnerId)
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
                        Partner obj = PartnerBusiness.GetById(partnerId);
                        if (obj != null)
                        {
                            PartnerViewModel obVM = new PartnerViewModel();
                            obVM.Address = obj.Address;
                            obVM.CreateBy = obj.CreateBy;
                            obVM.CreateDate = obj.CreateDate;
                            obVM.Display = obj.Display;
                            obVM.Email = obj.Email;
                            obVM.IsActive = obj.IsActive;
                            obVM.Logo = obj.Logo;
                            obVM.MetaDescription = obj.MetaDescription;
                            obVM.MetaKeyword = obj.MetaKeyword;
                            obVM.Order = obj.Order;
                            obVM.PartID = obj.PartID;
                            obVM.Phone = obj.Phone;
                            obVM.UpdateBy = obj.UpdateBy;
                            obVM.UpdateDate = obj.UpdateDate;
                            obVM.Website = obj.Website;

                            response = request.CreateResponse(HttpStatusCode.Created, obVM);
                        }
                    }
                    catch (Exception ex)
                    {
                        response = request.CreateResponse(HttpStatusCode.NotFound, "Không tìm thấy");
                    }
                }

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, PartnerViewModel partnerVM)
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
                    List<string> lstError = new List<string>();
                    if (string.IsNullOrEmpty(partnerVM.Display))
                    {
                        lstError.Add("Vui lòng nhập tên đối tác.");
                    }
                    else
                    {
                        partnerVM.Display = partnerVM.Display.Trim();
                        if (partnerVM.Display.Length > 200)
                        {
                            lstError.Add("Tên đối tác không được nhập quá 200 ký tự");
                        }
                    }

                    if (string.IsNullOrEmpty(partnerVM.Logo))
                    {
                        lstError.Add("Vui lòng chọn Logo cho đối tác");
                    }


                    if (!string.IsNullOrEmpty(partnerVM.MetaDescription)
                     && partnerVM.MetaDescription.Length > 500)
                    {
                        lstError.Add("Meta Description không được nhập quá 500 ký tự");
                    }

                    if (!string.IsNullOrEmpty(partnerVM.MetaKeyword)
                    && partnerVM.MetaKeyword.Length > 500)
                    {
                        lstError.Add("Meta Keyword không được nhập quá 500 ký tự");
                    }

                    if (lstError.Count() > 0)
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, string.Join(", ", lstError.ToList()));
                    }
                    //GlobalInfo _global = new GlobalInfo();


                    partnerVM.IsActive = true;
                    partnerVM.CreateDate = DateTime.Now;
                    partnerVM.CreateBy = UserInfoInstance.UserIDInstance;

                    Partner objNew = new Partner();
                    objNew.Address = partnerVM.Address;
                    objNew.CreateBy = partnerVM.CreateBy;
                    objNew.CreateDate = partnerVM.CreateDate;
                    objNew.Display = partnerVM.Display;
                    objNew.Email = partnerVM.Email;
                    objNew.IsActive = partnerVM.IsActive;
                    objNew.Logo = partnerVM.Logo;
                    objNew.MetaDescription = partnerVM.MetaDescription;
                    objNew.MetaKeyword = partnerVM.MetaKeyword;
                    objNew.Order = partnerVM.Order;
                    objNew.PartID = partnerVM.PartID;
                    objNew.Phone = partnerVM.Phone;
                    objNew.Website = partnerVM.Website;
                    PartnerBusiness.Create(objNew);
                    PartnerBusiness.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.Created, "Thêm thành công");
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, PartnerViewModel partnerVM)
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
                        Partner objP = PartnerBusiness.GetById(partnerVM.PartID);
                        if (objP == null)
                        {
                            lstError.Add("Đối tác không tồn tại");
                        }

                        if (string.IsNullOrEmpty(partnerVM.Display))
                        {
                            lstError.Add("Vui lòng nhập tên đối tác");
                        }
                        else
                        {
                            partnerVM.Display = partnerVM.Display.Trim();
                            if (partnerVM.Display.Length > 200)
                            {
                                lstError.Add("Tên đối tác không được nhập quá 200 ký tự");
                            }
                        }

                        if (string.IsNullOrEmpty(partnerVM.Logo))
                        {
                            lstError.Add("Vui lòng chọn logo cho dự án");
                        }


                        if (!string.IsNullOrEmpty(partnerVM.MetaDescription)
                         && partnerVM.MetaDescription.Length > 500)
                        {
                            lstError.Add("Meta Description không được nhập quá 500 ký tự");
                        }

                        if (!string.IsNullOrEmpty(partnerVM.MetaKeyword)
                        && partnerVM.MetaKeyword.Length > 500)
                        {
                            lstError.Add("Meta Keyword không được nhập quá 500 ký tự");
                        }

                        if (lstError.Count() > 0)
                        {
                            return request.CreateResponse(HttpStatusCode.BadRequest, string.Join(", ", lstError.ToList()));
                        }
                        //GlobalInfo _global = new GlobalInfo();

                        objP.Address = partnerVM.Address;
                        objP.CreateBy = partnerVM.CreateBy;
                        objP.CreateDate = partnerVM.CreateDate;
                        objP.Display = partnerVM.Display;
                        objP.Email = partnerVM.Email;
                        objP.IsActive = partnerVM.IsActive;
                        objP.Logo = partnerVM.Logo;
                        objP.MetaDescription = partnerVM.MetaDescription;
                        objP.MetaKeyword = partnerVM.MetaKeyword;
                        objP.Order = partnerVM.Order;
                        objP.PartID = partnerVM.PartID;
                        objP.Phone = partnerVM.Phone;
                        objP.Website = partnerVM.Website;
                        PartnerBusiness.Update(objP);
                        PartnerBusiness.SaveChange();
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

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string jsonlistId)
        {
            HttpResponseMessage response = null;
            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                List<int> lstPartnerID = new JavaScriptSerializer().Deserialize<List<int>>(jsonlistId);

                List<Partner> lstPartner = PartnerBusiness.GetAll().Where(x => lstPartnerID.Contains(x.PartID)).ToList();

                foreach (var item in lstPartner)
                {
                    PartnerBusiness.Delete(item.PartID);
                }
                PartnerBusiness.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK, lstPartner.Count());

            }
            return response;
        }

    }
}
