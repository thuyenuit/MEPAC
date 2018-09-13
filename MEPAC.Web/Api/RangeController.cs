using MEPAC.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MEPAC.Business.Business;
using MEPAC.Model.Models;
using MEPAC.Web.Models;
using MEPAC.WebAdmin.Infrastructure.Core;
using System.Web.Script.Serialization;

namespace MEPAC.Web.Api
{
    [RoutePrefix("api/range")]
    [Authorize]
    public class RangeController : BaseApiController
    {
        IMenuBusiness MenuBusiness;
        ISubMenuBusiness SubMenuBusiness;
        IRangeBusiness RangeBusiness;

        public RangeController(IMenuBusiness MenuBusiness,
        ISubMenuBusiness SubMenuBusiness,
        IRangeBusiness RangeBusiness)
        {
            this.MenuBusiness = MenuBusiness;
            this.SubMenuBusiness = SubMenuBusiness;
            this.RangeBusiness = RangeBusiness;
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
                var lstRangeDB = RangeBusiness.Search(dic);
                var lstSubMenu = SubMenuBusiness.GetAll().Where(x=>x.MenuID == 3);
                var lstMenu = MenuBusiness.GetAll();

                List<RangeViewModel> lstRangeVM = (from sm in lstSubMenu
                                                   join r in lstRangeDB on sm.SubMenuID equals r.SubMenuID into rt
                                                   from r in rt.DefaultIfEmpty()
                                                   join m in lstMenu on sm.MenuID equals m.MenuID
                                                   select new RangeViewModel
                                                   {
                                                       RangeID = r != null ? r.RangeID : 0,
                                                       Cotntent = r != null ? r.Cotntent : "",
                                                       MenuID = m.MenuID,
                                                       MenuName = m.Display,
                                                       SubMenuID = sm.SubMenuID,
                                                       LinkImage = sm.Image,
                                                       SubMenuName = sm.Display,
                                                       MetaKeyword = r != null ? r.MetaKeyword : "",
                                                       MetaDescription = r != null ? r.MetaDescription : "",
                                                       CreateBy = r != null ? r.CreateBy : "",
                                                       CreateDate = r.CreateDate,
                                                       UpdateBy = r != null ? r.UpdateBy : "",
                                                       UpdateDate = r != null ? r.UpdateDate : null,
                                                   }).ToList();

                lstRangeVM = lstRangeVM.OrderByDescending(x => x.CreateDate).ThenBy(x => x.SubMenuName).ToList();
                int totalRow = lstRangeVM.Count();

                List<RangeViewModel> lstResult = lstRangeVM.Skip((page) * pageSize).Take(pageSize).ToList();
                List<string> lstUserID = lstResult.Select(x => x.CreateBy).ToList();


                var paginationset = new PaginationSet<RangeViewModel>()
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
        public HttpResponseMessage GetById(HttpRequestMessage request, int rangeId)
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
                        IDictionary<string, object> dic = new Dictionary<string, object>();
                        var lstRangeDB = RangeBusiness.Search(dic);
                        var lstSubMenu = SubMenuBusiness.GetAll();
                        var lstMenu = MenuBusiness.GetAll();
                        var objRVM = new RangeViewModel();

                        var objDB = (from sm in lstSubMenu.Where(x => x.SubMenuID == rangeId)
                                     join r in lstRangeDB on sm.SubMenuID equals r.SubMenuID into rt
                                     from r in rt.DefaultIfEmpty()
                                     join m in lstMenu on sm.MenuID equals m.MenuID
                                     select new RangeViewModel
                                     {
                                         RangeID = r != null ? r.RangeID : 0,
                                         Cotntent = r != null ? r.Cotntent : "",
                                         MenuID = m.MenuID,
                                         MenuName = m.Display,
                                         SubMenuID = sm.SubMenuID,
                                         LinkImage = sm.Image,
                                         SubMenuName = sm.Display,
                                         MetaKeyword = r != null ? r.MetaKeyword : "",
                                         MetaDescription = r != null ? r.MetaDescription : "",
                                         CreateBy = r != null ? r.CreateBy : "",
                                         CreateDate = r.CreateDate,
                                         UpdateBy = r != null ? r.UpdateBy : "",
                                         UpdateDate = r != null ? r.UpdateDate : null,
                                     }).FirstOrDefault();
                        if (objDB != null)
                        {
                            objRVM = objDB;
                        }

                        response = request.CreateResponse(HttpStatusCode.Created, objRVM);
                    }
                    catch (Exception ex)
                    {
                        response = request.CreateResponse(HttpStatusCode.NotFound, "Không tìm thấy");
                    }
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, RangeViewModel rangeVM)
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
                        var objSubMenu = SubMenuBusiness.GetById(rangeVM.SubMenuID);

                        if (objSubMenu == null)
                        {
                            lstError.Add("Lĩnh vực không tồn tại.");
                        }

                        if (string.IsNullOrEmpty(rangeVM.SubMenuName))
                        {
                            lstError.Add("Vui lòng nhập tên lĩnh vực.");
                        }
                        else
                        {
                            rangeVM.SubMenuName = rangeVM.SubMenuName.Trim();
                            if (rangeVM.SubMenuName.Length > 200)
                            {
                                lstError.Add("Tên lĩnh vực không được nhập quá 200 ký tự");
                            }
                        }

                        if (string.IsNullOrEmpty(rangeVM.LinkImage))
                        {
                            lstError.Add("Hình ảnh không được phép rỗng.");
                        }


                        if (!string.IsNullOrEmpty(rangeVM.MetaDescription)
                         && rangeVM.MetaDescription.Length > 500)
                        {
                            lstError.Add("Meta Description không được nhập quá 500 ký tự");
                        }

                        if (!string.IsNullOrEmpty(rangeVM.MetaKeyword)
                        && rangeVM.MetaKeyword.Length > 500)
                        {
                            lstError.Add("Meta Keyword không được nhập quá 500 ký tự");
                        }

                        if (lstError.Count() > 0)
                        {
                            return request.CreateResponse(HttpStatusCode.BadRequest, string.Join(", ", lstError.ToList()));
                        }

                        //List<string> lstMoreImage = new JavaScriptSerializer().Deserialize<List<string>>(projectVM.PostBy);
                        //projectVM.ListMoreImage = lstMoreImage;

                        //if (projectVM.IsShow)
                        //{
                        //    projectVM.PostBy = Provider.UserInfoInstance.UserIDInstance;
                        //    projectVM.PostDate = DateTime.Now;
                        //}

                        //SubMenu objSM = new SubMenu()
                        //{
                        //    SubMenuID = rangeVM.SubMenuID,
                        //    Display = rangeVM.SubMenuName,
                        //    Image = rangeVM.LinkImage,
                        //};
                        objSubMenu.Display = rangeVM.SubMenuName;
                        objSubMenu.Image = rangeVM.LinkImage;
                        SubMenuBusiness.Update(objSubMenu);

                        Range objRDB = RangeBusiness.GetById(rangeVM.RangeID);
                        if (objRDB == null)
                        {
                            Range objR = new Range()
                            {
                                SubMenuID = rangeVM.SubMenuID,
                                Cotntent = rangeVM.Cotntent,
                                MetaDescription = rangeVM.MetaDescription,
                                MetaKeyword = rangeVM.MetaKeyword,
                                CreateDate = DateTime.Now,
                                CreateBy = "",
                            };
                            RangeBusiness.Create(objR);
                        }
                        else
                        {
                            //Range objR = new Range()
                            //{
                            //    Cotntent = rangeVM.Cotntent,
                            //    MetaDescription = rangeVM.MetaDescription,
                            //    MetaKeyword = rangeVM.MetaKeyword,
                            //    UpdateDate = DateTime.Now,
                            //    UpdateBy = "",
                            //};
                            objRDB.Cotntent = rangeVM.Cotntent;
                            objRDB.MetaDescription = rangeVM.MetaDescription;
                            objRDB.MetaKeyword = rangeVM.MetaKeyword;
                            objRDB.UpdateDate = DateTime.Now;
                            objRDB.UpdateBy = "";
                            RangeBusiness.Update(objRDB);
                        }

                        SubMenuBusiness.Save();
                        RangeBusiness.Save();
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
                List<int> lstSubMenuID = new JavaScriptSerializer().Deserialize<List<int>>(jsonlistId);

                List<Range> lstRange = RangeBusiness.GetAll().Where(x => lstSubMenuID.Contains(x.SubMenuID)).ToList();
                foreach (var item in lstRange)
                {
                    RangeBusiness.Delete(item.RangeID);
                }

                List<SubMenu> lstSubMenu = SubMenuBusiness.GetAll().Where(x => lstSubMenuID.Contains(x.SubMenuID)).ToList();
                foreach (var item in lstSubMenu)
                {
                    SubMenuBusiness.Delete(item.SubMenuID);
                }

                RangeBusiness.Save();
                SubMenuBusiness.Save();
                response = request.CreateResponse(HttpStatusCode.OK, lstSubMenuID.Count());

            }
            return response;
        }

    }
}
