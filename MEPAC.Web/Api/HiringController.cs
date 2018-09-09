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
    [RoutePrefix("api/hiring")]
    [Authorize]
    public class HiringController : BaseApiController
    {
        IHiringBusiness HiringBusiness;
        public HiringController(
            IHiringBusiness HiringBusiness)
        {
            this.HiringBusiness = HiringBusiness;
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
                List<Hiring> lstHiringDB = HiringBusiness.Search(dic).ToList();

                List<HiringViewModel> lstHiringVM = lstHiringDB.Select(x => new HiringViewModel()
                {
                    Content = x.Content,
                    CreateBy = x.CreateBy,
                    CreateDate = x.CreateDate,
                    EnddDate = x.EnddDate,
                    HiringID = x.HiringID,
                    IsActive = x.IsActive,
                    IsShow = x.IsShow,
                    LinkImage = x.LinkImage,
                    MetaDescription = x.MetaDescription,
                    MetaKeyword = x.MetaKeyword,
                    Position = x.Position,
                    PostBy = x.PostBy,
                    PostDate = x.PostDate,
                    StartDate = x.StartDate,
                    UpdateBy = x.UpdateBy,
                    UpdateDate = x.UpdateDate
                }).ToList();


                lstHiringVM = lstHiringVM.OrderByDescending(x => x.CreateDate).ThenBy(x => x.Position).ToList();
                int totalRow = lstHiringVM.Count();

                List<HiringViewModel> lstResult = lstHiringVM.Skip((page) * pageSize).Take(pageSize).ToList();

                var paginationset = new PaginationSet<HiringViewModel>()
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
        public HttpResponseMessage GetById(HttpRequestMessage request, int hiringId)
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
                        Hiring obj = HiringBusiness.GetById(hiringId);
                        if (obj != null)
                        {
                            HiringViewModel obVM = new HiringViewModel();
                            obVM.Content = obj.Content;
                            obVM.CreateBy = obj.CreateBy;
                            obVM.CreateDate = obj.CreateDate;
                            obVM.EnddDate = obj.EnddDate;
                            obVM.HiringID = obj.HiringID;
                            obVM.IsActive = obj.IsActive;
                            obVM.IsShow = obj.IsShow;
                            obVM.LinkImage = obj.LinkImage;
                            obVM.MetaDescription = obj.MetaDescription;
                            obVM.MetaKeyword = obj.MetaKeyword;
                            obVM.Position = obj.Position;
                            obVM.PostBy = obj.PostBy;
                            obVM.PostDate = obj.PostDate;
                            obVM.StartDate = obj.StartDate;
                            obVM.UpdateBy = obj.UpdateBy;
                            obVM.UpdateDate = obj.UpdateDate;

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
        public HttpResponseMessage Create(HttpRequestMessage request, HiringViewModel hiringVM)
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
                    if (string.IsNullOrEmpty(hiringVM.Position))
                    {
                        lstError.Add("Vui lòng tin tuyển dụng");
                    }
                    else
                    {
                        hiringVM.Position = hiringVM.Position.Trim();
                        if (hiringVM.Position.Length > 200)
                        {
                            lstError.Add("Tin tuyển dụng không được nhập quá 200 ký tự");
                        }
                    }

                    if (string.IsNullOrEmpty(hiringVM.LinkImage))
                    {
                        lstError.Add("Vui lòng chọn ảnh cho tin tuyển dụng");
                    }


                    if (!string.IsNullOrEmpty(hiringVM.MetaDescription)
                     && hiringVM.MetaDescription.Length > 500)
                    {
                        lstError.Add("Meta Description không được nhập quá 500 ký tự");
                    }

                    if (!string.IsNullOrEmpty(hiringVM.MetaKeyword)
                    && hiringVM.MetaKeyword.Length > 500)
                    {
                        lstError.Add("Meta Keyword không được nhập quá 500 ký tự");
                    }

                    if (lstError.Count() > 0)
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, string.Join(", ", lstError.ToList()));
                    }
                    //GlobalInfo _global = new GlobalInfo();

                    hiringVM.IsActive = true;
                    hiringVM.CreateDate = DateTime.Now;
                    hiringVM.CreateBy = UserInfoInstance.UserIDInstance;
                    hiringVM.PostBy = string.Empty;

                    Hiring objNew = new Hiring();
                    objNew.Content = hiringVM.Content;
                    objNew.CreateBy = hiringVM.CreateBy;
                    objNew.CreateDate = DateTime.Now;
                    objNew.EnddDate = hiringVM.EnddDate;
                    objNew.HiringID = hiringVM.HiringID;
                    objNew.IsActive = true;
                    objNew.IsShow = false;
                    objNew.LinkImage = hiringVM.LinkImage;
                    objNew.MetaDescription = hiringVM.MetaDescription;
                    objNew.MetaKeyword = hiringVM.MetaKeyword;
                    objNew.Position = hiringVM.Position;
                    objNew.PostBy = hiringVM.PostBy;
                    objNew.PostDate = hiringVM.PostDate;
                    objNew.StartDate = hiringVM.StartDate.Value;
                    objNew.UpdateBy = hiringVM.UpdateBy;
                    objNew.UpdateDate = hiringVM.UpdateDate;
                    HiringBusiness.Create(objNew);
                    HiringBusiness.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.Created, "Thêm thành công");
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, HiringViewModel hiringVM)
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
                        Hiring objH = HiringBusiness.GetById(hiringVM.HiringID);
                        List<string> lstError = new List<string>();
                        if (objH == null)
                        {
                            lstError.Add("Dự án không tồn tại.");
                        }

                        if (string.IsNullOrEmpty(hiringVM.Position))
                        {
                            lstError.Add("Vui lòng nhập tin tuyển dụng.");
                        }
                        else
                        {
                            hiringVM.Position = hiringVM.Position.Trim();
                            if (hiringVM.Position.Length > 200)
                            {
                                lstError.Add("Tin tuyển dụng không được nhập quá 200 ký tự.");
                            }
                        }

                        if (string.IsNullOrEmpty(hiringVM.LinkImage))
                        {
                            lstError.Add("Vui lòng chọn ảnh cho tin tuyển dụng.");
                        }

                        DateTime a = DateTime.Now;
                        var aa = DateTime.TryParse(hiringVM.StartDate.Value.ToString(), out a);
                        var bb = DateTime.TryParse(hiringVM.EnddDate.Value.ToString(), out a);
                        if (!aa || !bb) 
                        {
                            lstError.Add("Thời gian thực hiện không được để trống.");
                        }


                        if (!string.IsNullOrEmpty(hiringVM.MetaDescription)
                         && hiringVM.MetaDescription.Length > 500)
                        {
                            lstError.Add("Meta Description không được nhập quá 500 ký tự");
                        }

                        if (!string.IsNullOrEmpty(hiringVM.MetaKeyword)
                        && hiringVM.MetaKeyword.Length > 500)
                        {
                            lstError.Add("Meta Keyword không được nhập quá 500 ký tự");
                        }

                        if (lstError.Count() > 0)
                        {
                            return request.CreateResponse(HttpStatusCode.BadRequest, string.Join(", ", lstError.ToList()));
                        }

                        if (hiringVM.IsShow)
                        {
                            hiringVM.PostBy = UserInfoInstance.UserIDInstance;
                            hiringVM.PostDate = DateTime.Now;
                        }

                        objH.Content = hiringVM.Content;
                        objH.CreateBy = hiringVM.CreateBy;
                        objH.CreateDate = DateTime.Now;
                        objH.EnddDate = hiringVM.EnddDate.Value.AddDays(1);
                        objH.HiringID = hiringVM.HiringID;
                        objH.IsActive = hiringVM.IsActive;
                        objH.IsShow = hiringVM.IsShow;
                        objH.LinkImage = hiringVM.LinkImage;
                        objH.MetaDescription = hiringVM.MetaDescription;
                        objH.MetaKeyword = hiringVM.MetaKeyword;
                        objH.Position = hiringVM.Position;
                        objH.PostBy = UserInfoInstance.UserIDInstance;
                        objH.PostDate = DateTime.Now;
                        objH.StartDate = hiringVM.StartDate.Value.AddDays(1);
                        objH.UpdateBy = hiringVM.UpdateBy;
                        objH.UpdateDate = DateTime.Now;
                        HiringBusiness.Update(objH);
                        HiringBusiness.SaveChange();
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
                List<int> lstHiringID = new JavaScriptSerializer().Deserialize<List<int>>(jsonlistId);

                List<Hiring> lstHiring = HiringBusiness.GetAll().Where(x => lstHiringID.Contains(x.HiringID)).ToList();

                foreach (var item in lstHiring)
                {
                    HiringBusiness.Delete(item.HiringID);
                }
                HiringBusiness.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK, lstHiringID.Count());

            }
            return response;
        }

    }
}
