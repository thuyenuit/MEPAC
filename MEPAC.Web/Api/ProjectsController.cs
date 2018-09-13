using MEPAC.Business;
using MEPAC.Business.Business;
using MEPAC.Model.Models;
using MEPAC.Web.Controllers;
using MEPAC.Web.Infrastructure.Core;
using MEPAC.Web.Models;
using MEPAC.Web.Provider;
//using MEPAC.Web.Utils;
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
    [RoutePrefix("api/projects")]
    [Authorize]
    public class ProjectsController : BaseApiController
    {
        IMetaImageBusiness _metaImageBusiness;
        IProjectsBusiness _projectsBusiness;
        IApplicationUserBusiness _applicationUserBusiness;
        public ProjectsController(
            IMetaImageBusiness _metaImageBusiness,
            IProjectsBusiness _projectsBusiness,
            IApplicationUserBusiness _applicationUserBusiness)
        {
            this._metaImageBusiness = _metaImageBusiness;
            this._projectsBusiness = _projectsBusiness;
            this._applicationUserBusiness = _applicationUserBusiness;
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
                List<Projects> lstProjectsDB = _projectsBusiness.Search(dic).ToList();

                List<ProjectsViewModel> lstProjectVM = lstProjectsDB.Select(x => new ProjectsViewModel()
                {
                    ProjectID = x.ProjectID,
                    Display = x.Display,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    LinkImage = x.Image,
                    Description = x.Description,
                    MetaKeyword = x.MetaKeyword,
                    MetaDescription = x.MetaDescription,
                    CreateBy = x.CreateBy,
                    CreateDate = x.CreateDate,
                    UpdateBy = x.UpdateBy,
                    UpdateDate = x.UpdateDate,
                    PostBy = x.PostBy,
                    PostDate = x.PostDate,
                    IsActive = x.IsActive,
                    IsShow = x.IsShow,
                    IsRepresentative = x.IsRepresentative,
                    IsFinish = x.IsFinish
            }).ToList();

                string StrDate = string.Empty;
                string StrHour = string.Empty;
                string StrUser = string.Empty;

                if (lstProjectVM.Count() > 0)
                {
                    string userId = string.Empty;
                    var objProjectByUpdate = lstProjectVM.OrderByDescending(x => x.UpdateDate).FirstOrDefault();
                    var objProjectByCreate = lstProjectVM.OrderByDescending(x => x.CreateDate).FirstOrDefault();

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

                lstProjectVM = lstProjectVM.OrderByDescending(x => x.ProjectID).ThenBy(x => x.Display).ToList();
                int totalRow = lstProjectVM.Count();

                List<ProjectsViewModel> lstResult = lstProjectVM.Skip((page) * pageSize).Take(pageSize).ToList();
                List<string> lstUserID = lstResult.Select(x => x.CreateBy).ToList();
                var lstUSer = _applicationUserBusiness.GetAll().Where(x => lstUserID.Contains(x.Id)).ToList();

                foreach (var item in lstResult)
                {
                    var objUser = lstUSer.Where(x => x.Id == item.CreateBy).FirstOrDefault();
                    if (objUser != null)
                    {
                        item.FullNameCreate = objUser.FullName;
                    }

                    if(!string.IsNullOrEmpty(item.UpdateBy))
                    {
                        objUser = lstUSer.Where(x => x.Id == item.UpdateBy).FirstOrDefault();
                        if (objUser != null)
                        {
                            item.FullNameUpdate = objUser.FullName;
                        }
                    }                
                }


                var paginationset = new PaginationSet<ProjectsViewModel>()
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
                        Projects obj = _projectsBusiness.GetById(projectId);
                        if (obj != null)
                        {
                            ProjectsViewModel obVM = new ProjectsViewModel();
                            obVM.ProjectID = obj.ProjectID;
                            obVM.Display = obj.Display;
                            obVM.FromDate = obj.FromDate;
                            obVM.ToDate = obj.ToDate;
                            obVM.LinkImage = obj.Image;
                            obVM.Description = obj.Description;
                            obVM.CreateBy = obj.CreateBy;
                            obVM.CreateDate = obj.CreateDate;
                            obVM.IsActive = obj.IsActive;
                            obVM.IsShow = obj.IsShow;
                            obVM.PostBy = obj.PostBy;
                            obVM.PostDate = obj.PostDate;
                            obVM.MetaDescription = obj.MetaDescription;
                            obVM.MetaKeyword = obj.MetaKeyword;
                            obVM.IsRepresentative = obj.IsRepresentative;
                            obVM.IsFinish = obj.IsFinish;
                            List<MetaImage> lstMetaImage = _metaImageBusiness.GetAll().Where(x => x.TypeID == 1 && x.ParentID == obj.ProjectID).ToList();
                            if (lstMetaImage.Count() > 0)
                            {
                                List<string> lstImage = lstMetaImage.Select(x => x.Link).ToList();
                                obVM.ListMoreImage = lstImage;
                            }

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
        public HttpResponseMessage Create(HttpRequestMessage request, ProjectsViewModel projectVM)
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
                    if (string.IsNullOrEmpty(projectVM.Display))
                    {
                        lstError.Add("Vui lòng nhập tên dự án");
                    }
                    else
                    {
                        projectVM.Display = projectVM.Display.Trim();
                        if (projectVM.Display.Length > 200)
                        {
                            lstError.Add("Tên dự án không được nhập quá 200 ký tự");
                        }
                    }

                    if (string.IsNullOrEmpty(projectVM.LinkImage))
                    {
                        lstError.Add("Vui lòng chọn ảnh đại diện cho dự án");
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
                    //GlobalInfo _global = new GlobalInfo();
                    List<string> lstMoreImage = new JavaScriptSerializer().Deserialize<List<string>>(projectVM.JSonMoreImage);
                    projectVM.ListMoreImage = lstMoreImage;

                    projectVM.IsActive = true;
                    projectVM.CreateDate = DateTime.Now;
                    projectVM.CreateBy = AdminController.UserID; // UserInfoInstance.UserIDInstance;

                    if (projectVM.IsShow)
                    {
                        projectVM.PostBy = AdminController.UserID; // UserInfoInstance.UserIDInstance;
                        projectVM.PostDate = DateTime.Now;
                    }

                    Projects objNew = new Projects();
                    objNew.Display = projectVM.Display;
                    objNew.ToDate = projectVM.ToDate;
                    objNew.FromDate = projectVM.FromDate;
                    objNew.Image = projectVM.LinkImage;
                    objNew.Description = projectVM.Description;
                    objNew.CreateBy = projectVM.CreateBy;
                    objNew.CreateDate = projectVM.CreateDate;
                    objNew.IsActive = projectVM.IsActive;
                    objNew.IsShow = projectVM.IsShow;
                    objNew.PostBy = projectVM.PostBy;
                    objNew.PostDate = projectVM.PostDate;
                    objNew.MetaDescription = projectVM.MetaDescription;
                    objNew.MetaKeyword = projectVM.MetaKeyword;
                    objNew.IsRepresentative = projectVM.IsRepresentative;
                    objNew.IsFinish = projectVM.IsFinish;
                    _projectsBusiness.Create(objNew, lstMoreImage);
                    response = request.CreateResponse(HttpStatusCode.Created, "Thêm thành công");
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, ProjectsViewModel projectVM)
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
                        if (projectVM.ProjectID <= 0)
                        {
                            lstError.Add("Dự án không tồn tại");
                        }

                        if (string.IsNullOrEmpty(projectVM.Display))
                        {
                            lstError.Add("Vui lòng nhập tên dự án");
                        }
                        else
                        {
                            projectVM.Display = projectVM.Display.Trim();
                            if (projectVM.Display.Length > 200)
                            {
                                lstError.Add("Tên dự án không được nhập quá 200 ký tự");
                            }
                        }

                        if (string.IsNullOrEmpty(projectVM.LinkImage))
                        {
                            lstError.Add("Vui lòng chọn ảnh đại diện cho dự án");
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
                        //GlobalInfo _global = new GlobalInfo();
                        List<string> lstMoreImage = !string.IsNullOrEmpty(projectVM.JSonMoreImage) ? new JavaScriptSerializer().Deserialize<List<string>>(projectVM.JSonMoreImage) :  new List<string>();
                        projectVM.ListMoreImage = lstMoreImage;

                        if (projectVM.IsShow)
                        {
                            projectVM.PostBy = AdminController.UserID; // UserInfoInstance.UserIDInstance;
                            projectVM.PostDate = DateTime.Now;
                        }

                        Projects objNew = new Projects();
                        objNew.ProjectID = projectVM.ProjectID;
                        objNew.Display = projectVM.Display;
                        objNew.FromDate = projectVM.FromDate;
                        objNew.ToDate = projectVM.ToDate;
                        objNew.Image = projectVM.LinkImage;
                        objNew.Description = projectVM.Description;
                        objNew.IsActive = projectVM.IsActive;
                        objNew.IsShow = projectVM.IsShow;
                        objNew.PostBy = projectVM.PostBy;
                        objNew.PostDate = projectVM.PostDate;
                        objNew.UpdateBy = AdminController.UserID; // UserInfoInstance.UserIDInstance;
                        objNew.UpdateDate = DateTime.Now;
                        objNew.MetaDescription = projectVM.MetaDescription;
                        objNew.MetaKeyword = projectVM.MetaKeyword;
                        objNew.IsRepresentative = projectVM.IsRepresentative;
                        objNew.IsFinish = projectVM.IsFinish;
                        _projectsBusiness.Update(objNew, lstMoreImage);
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
                List<int> lstProjectID = new JavaScriptSerializer().Deserialize<List<int>>(jsonlistId);

                List<Projects> lstProjects = _projectsBusiness.GetAll().Where(x=> lstProjectID.Contains(x.ProjectID)).ToList();

                foreach (var item in lstProjects)
                {
                    _projectsBusiness.Delete(item.ProjectID);
                }
                _projectsBusiness.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK, lstProjects.Count());

            }
            return response;
        }

    }
}
