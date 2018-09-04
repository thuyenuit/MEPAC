using MEPAC.Business.Business;
using MEPAC.Model.Models;
using MEPAC.Web.Infrastructure.Core;
using MEPAC.Web.Models;
using MEPAC.WebAdmin.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MEPAC.Web.Api
{
    [RoutePrefix("api/projects")]
    [Authorize]
    public class ProjectsController : BaseApiController
    {
        IProjectsBusiness _projectsBusiness;
        IApplicationUserBusiness _applicationUserBusiness;
        public ProjectsController(
            IProjectsBusiness _projectsBusiness,
            IApplicationUserBusiness _applicationUserBusiness)
        {
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
                    IsShow = x.IsShow
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

                IEnumerable<ProjectsViewModel> lstResult = lstProjectVM.Skip((page) * pageSize).Take(pageSize);
                var paginationset = new PaginationSet<ProjectsViewModel>()
                {
                    Items = lstResult,
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
                             lstError.Add("Tên sản phẩm không được nhập quá 200 ký tự");
                         }                       
                     }

                   
                    if (string.IsNullOrEmpty(projectVM.LinkImage))
                    {
                        lstError.Add("Vui lòng chọn ảnh dự án");
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

                    projectVM.IsActive = true;
                    projectVM.CreateDate = DateTime.Now;
                    projectVM.CreateBy = Provider.UserInfoInstance.UserCodeInstance;

                    if(projectVM.IsShow)
                    {
                        projectVM.PostBy = Provider.UserInfoInstance.UserCodeInstance;
                        projectVM.PostDate = DateTime.Now;
                    }

                    Projects objNew = new Projects();
                    objNew.Display = projectVM.Display;
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

                                        response = request.CreateResponse(HttpStatusCode.Created, "Thêm thành công");
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
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
                    /*List<string> lstError = new List<string>();
                    if (productVM.ProductID <= 0)
                    {
                        lstError.Add("Thông tin sản phẩm không hợp lệ");
                    }

                    if (string.IsNullOrEmpty(productVM.ProductName))
                    {
                        lstError.Add("Vui lòng nhập tên sản phẩm");
                    }
                    else
                    {
                        productVM.ProductName = productVM.ProductName.Trim();
                        if (productVM.ProductName.Length > 255)
                        {
                            return request.CreateResponse(HttpStatusCode.BadRequest, "Vui lòng nhập tên sản phẩm");
                        }

                        bool checkName = _productService.GetAll().Any(x => x.ProductID != productVM.ProductID && x.ProductName.ToUpper().Equals(productVM.ProductName));
                        if (checkName)
                        {
                            lstError.Add(string.Format("Tên sản phẩm <i>{0}</i> đã tồn tại", productVM.ProductName));
                        }
                    }

                    if (productVM.CategoryID <= 0)
                    {
                        lstError.Add("Vui lòng chọn thể loại");
                    }

                    if (!string.IsNullOrEmpty(productVM.MetaDescription)
                    && productVM.MetaDescription.Length > 500)
                    {
                        lstError.Add("MetaDescription không được nhập quá 500 ký tự");
                    }

                    if (string.IsNullOrEmpty(productVM.ProductCode))
                    {
                        //string code = Utils.AutoGenericCode();
                        //productVM.ProductCode = code;
                        lstError.Add("Vui lòng nhập mã sản phẩm");
                    }
                    else
                    {
                        productVM.ProductCode = productVM.ProductCode.Trim();
                        if (productVM.ProductCode.Length > 255)
                        {
                            lstError.Add("Mã sản phẩm không được nhập quá 255 ký tự");
                        }

                        bool checkCode = _productService.GetAll().Any(x => x.ProductID != productVM.ProductID && x.ProductCode.ToUpper().Equals(productVM.ProductCode));
                        if (checkCode)
                        {
                            lstError.Add(string.Format("Mã sản phẩm <i>{0}</i> đã tồn tại", productVM.ProductCode));
                        }
                    }

                    if ((productVM.ExistMaximum.HasValue && productVM.ExistMinimum.HasValue)
                        && (productVM.ExistMaximum > 0 && productVM.ExistMinimum > 0))
                    {
                        if (productVM.ExistMaximum.Value <= productVM.ExistMinimum.Value)
                        {
                            lstError.Add("Giá trị tồn tối thiểu không được lớn hơn tồn tối đa");
                        }
                    }

                    if (lstError.Count() > 0)
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, string.Join(", ", lstError.ToList()));
                    }

                    productVM.IsActive = true;
                    productVM.CreateDate = DateTime.Now;
                    productVM.CreateBy = UserInfoInstance.UserCodeInstance;

                    var productDB = _productService.GetSingleById(productVM.ProductID);
                    if (productDB != null)
                    {
                        productVM.UpdateDate = DateTime.Now;
                        productDB.MapProduct(productVM);
                        _productService.Update(productDB);
                        _productService.SaveChanges();
                        response = request.CreateResponse(HttpStatusCode.OK, "Cập nhật thành công");
                    }
                    else
                        response = request.CreateResponse(HttpStatusCode.NotFound, "Không tìm thấy sản phẩm.");*/

                    response = request.CreateResponse(HttpStatusCode.OK, "Cập nhật thành công");
                }

                return response;
            });
        }

    }
}
