using MEPAC.Business;
using MEPAC.Business.Business;
using MEPAC.Model.Models;
using MEPAC.Web.Controllers;
using MEPAC.Web.Infrastructure.Core;
using MEPAC.Web.Models;
using MEPAC.Web.Provider;
using MEPAC.Web.Utils;
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
    [RoutePrefix("api/slide")]
    [Authorize]
    public class SlideController : BaseApiController
    {
        IMetaImageBusiness _metaImageBusiness;
        ISlideBusiness _slideBusiness;
        IApplicationUserBusiness _applicationUserBusiness;
        public SlideController(
            IMetaImageBusiness _metaImageBusiness,
            ISlideBusiness _slideBusiness,
            IApplicationUserBusiness _applicationUserBusiness)
        {
            this._metaImageBusiness = _metaImageBusiness;
            this._slideBusiness = _slideBusiness;
            this._applicationUserBusiness = _applicationUserBusiness;
        }

        [Route("search")]
        [HttpGet]
        public HttpResponseMessage Search(HttpRequestMessage request,
            int page, int pageSize, int status)
        {
            return CreateHttpResponse(request, () =>
            {
                IQueryable<Slide> iquery = _slideBusiness.GetAll();
                if (status == 1)
                {
                    iquery = iquery.Where(x => x.IsActive == true);
                }
                else
                {
                    iquery = iquery.Where(x => x.IsActive == false);
                }

                List<Slide> lstSlideDB = iquery.ToList();

                List<SlideViewModel> lstSlideVM = lstSlideDB.Select(x => new SlideViewModel()
                {
                    SlideID = x.SlideID,
                    Content = x.Content,
                    Image = x.Image,                  
                    MetaKeyword = x.MetaKeyword,
                    MetaDescription = x.MetaDescription,
                    CreateBy = x.CreateBy,
                    CreateDate = x.CreateDate,
                    UpdateBy = x.UpdateBy,
                    UpdateDate = x.UpdateDate,
                    IsActive = x.IsActive
                }).ToList();

                string StrDate = string.Empty;
                string StrHour = string.Empty;
                string StrUser = string.Empty;

                if (lstSlideVM.Count() > 0)
                {
                    string userId = string.Empty;
                    var objProjectByUpdate = lstSlideVM.OrderByDescending(x => x.UpdateDate).FirstOrDefault();
                    var objProjectByCreate = lstSlideVM.OrderByDescending(x => x.CreateDate).FirstOrDefault();

                    if (objProjectByUpdate.UpdateDate.HasValue)
                    {
                        DateTime timeUpdate = objProjectByUpdate.UpdateDate.Value;
                        DateTime timeCreate = objProjectByCreate.CreateDate;
                        if (timeCreate < timeUpdate)
                        {
                            StrHour = string.Format("{0}h{1}", objProjectByUpdate.UpdateDate.Value.ToString("HH"),
                                                            objProjectByUpdate.UpdateDate.Value.ToString("mm"));
                            StrDate = objProjectByUpdate.UpdateDate.Value.ToString("dd/MM/yyy");
                            userId = objProjectByUpdate.UpdateBy;
                        }
                        else
                        {
                            StrHour = string.Format("{0}h{1}", objProjectByCreate.CreateDate.ToString("HH"),
                                                           objProjectByCreate.CreateDate.ToString("mm"));
                            StrDate = objProjectByCreate.CreateDate.ToString("dd/MM/yyy");
                            userId = objProjectByCreate.CreateBy;
                        }
                    }
                    else
                    {
                        StrHour = string.Format("{0}h{1}", objProjectByCreate.CreateDate.ToString("HH"),
                                                              objProjectByCreate.CreateDate.ToString("mm"));
                        StrDate = objProjectByCreate.CreateDate.ToString("dd/MM/yyy");
                        userId = objProjectByCreate.CreateBy;
                    }

                    if (!string.IsNullOrEmpty(userId))
                    {
                        List<string> lstUserCode = new List<string>();
                        var userResult = _applicationUserBusiness.GetSingleById(userId);
                        if (userResult != null)
                            StrUser = userResult.FullName;
                    }
                }

                lstSlideVM = lstSlideVM.OrderByDescending(x => x.SlideID).ToList();
                int totalRow = lstSlideVM.Count();

                List<SlideViewModel> lstResult = lstSlideVM.Skip((page) * pageSize).Take(pageSize).ToList();
                List<string> lstUserID = lstResult.Select(x => x.CreateBy).ToList();
                var lstUSer = _applicationUserBusiness.GetAll().Where(x => lstUserID.Contains(x.Id)).ToList();

                foreach (var item in lstResult)
                {
                    var objUser = lstUSer.Where(x => x.Id == item.CreateBy).FirstOrDefault();
                    if (objUser != null)
                    {
                        item.FullNameCreate = objUser.FullName;
                    }

                    if (!string.IsNullOrEmpty(item.UpdateBy))
                    {
                        objUser = lstUSer.Where(x => x.Id == item.UpdateBy).FirstOrDefault();
                        if (objUser != null)
                        {
                            item.FullNameUpdate = objUser.FullName;
                        }
                    }
                }


                var paginationset = new PaginationSet<SlideViewModel>()
                {
                    Items = lstResult.AsEnumerable(),
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    StrDate = StrDate,
                    StrHour = StrHour,
                    StrUser = StrUser
                };

                var response = request.CreateResponse(HttpStatusCode.OK, paginationset);
                return response;
            });
        }

        [Route("getbyid")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int projectId)
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
                        Slide obj = _slideBusiness.GetById(projectId);
                        if (obj != null)
                        {
                            SlideViewModel obVM = new SlideViewModel();
                            obVM.SlideID = obj.SlideID;
                            obVM.Content = obj.Content;
                            obVM.Image = obj.Image;
                            obVM.CreateBy = obj.CreateBy;
                            obVM.CreateDate = obj.CreateDate;
                            obVM.IsActive = obj.IsActive;                          
                            obVM.MetaDescription = obj.MetaDescription;
                            obVM.MetaKeyword = obj.MetaKeyword;

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
        public HttpResponseMessage Create(HttpRequestMessage request, SlideViewModel projectVM)
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
                    if (string.IsNullOrEmpty(projectVM.Content))
                    {
                       // lstError.Add("Vui lòng nhập tên dự án");
                    }
                    else
                    {
                        projectVM.Content = projectVM.Content.Trim();
                        if (projectVM.Content.Length > 200)
                        {
                            lstError.Add("Mô tả ngắn không được nhập quá 200 ký tự");
                        }
                    }

                    if (string.IsNullOrEmpty(projectVM.Image))
                    {
                        lstError.Add("Vui lòng chọn ảnh slide");
                    }


                    if (!string.IsNullOrEmpty(projectVM.MetaDescription)
                     && projectVM.MetaDescription.Length > 500)
                    {
                        lstError.Add("Meta Description không được nhập quá 500 ký tự");
                    }

                    if (!string.IsNullOrEmpty(projectVM.MetaKeyword)
                    && projectVM.MetaKeyword.Length > 500)
                    {
                        lstError.Add("Meta Keyword không được nhập quá 500 ký tự");
                    }

                    if (lstError.Count() > 0)
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, string.Join(", ", lstError.ToList()));
                    }
                    GlobalInfo _global = new GlobalInfo();

                    projectVM.IsActive = true;
                    projectVM.CreateDate = DateTime.Now;
                    projectVM.CreateBy = AdminController.UserID;// UserInfoInstance.UserIDInstance;

                    Slide objNew = new Slide();
                    objNew.Content = projectVM.Content;
                    objNew.Image = projectVM.Image;
                    objNew.CreateBy = projectVM.CreateBy;
                    objNew.CreateDate = projectVM.CreateDate;
                    objNew.IsActive = projectVM.IsActive;
                    objNew.MetaDescription = projectVM.MetaDescription;
                    objNew.MetaKeyword = projectVM.MetaKeyword;
                    _slideBusiness.Create(objNew);
                    _slideBusiness.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.Created, "Thêm thành công");
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, SlideViewModel projectVM)
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
                        if (string.IsNullOrEmpty(projectVM.Content))
                        {
                            // lstError.Add("Vui lòng nhập tên dự án");
                        }
                        else
                        {
                            projectVM.Content = projectVM.Content.Trim();
                            if (projectVM.Content.Length > 200)
                            {
                                lstError.Add("Mô tả ngắn không được nhập quá 200 ký tự");
                            }
                        }

                        if (string.IsNullOrEmpty(projectVM.Image))
                        {
                            lstError.Add("Vui lòng chọn ảnh slide");
                        }


                        if (!string.IsNullOrEmpty(projectVM.MetaDescription)
                         && projectVM.MetaDescription.Length > 500)
                        {
                            lstError.Add("Meta Description không được nhập quá 500 ký tự");
                        }

                        if (!string.IsNullOrEmpty(projectVM.MetaKeyword)
                        && projectVM.MetaKeyword.Length > 500)
                        {
                            lstError.Add("Meta Keyword không được nhập quá 500 ký tự");
                        }

                        if (lstError.Count() > 0)
                        {
                            return request.CreateResponse(HttpStatusCode.BadRequest, string.Join(", ", lstError.ToList()));
                        }

                        GlobalInfo _global = new GlobalInfo();

                        Slide objNew = new Slide();
                        objNew.SlideID = projectVM.SlideID;
                        objNew.Content = projectVM.Content;                    
                        objNew.Image = projectVM.Image;                  
                        objNew.IsActive = projectVM.IsActive;
                        objNew.UpdateBy = AdminController.UserID;// UserInfoInstance.UserIDInstance;
                        objNew.UpdateDate = DateTime.Now;
                        objNew.MetaDescription = projectVM.MetaDescription;
                        objNew.MetaKeyword = projectVM.MetaKeyword;
                        _slideBusiness.Update(objNew);
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
                List<int> lstSlideID = new JavaScriptSerializer().Deserialize<List<int>>(jsonlistId);

                List<Slide> lstSlide = _slideBusiness.GetAll().Where(x => lstSlideID.Contains(x.SlideID)).ToList();

                foreach (var item in lstSlide)
                {
                    _slideBusiness.Delete(item.SlideID);
                }
                _slideBusiness.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK, lstSlide.Count());

            }
            return response;
        }

    }
}
