using SMS.API.Infrastructure.Core;
using SMS.Shared.Shares;
using SMS.DTO.Category.Model;
using SMS.Model.Models;
using SMS.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMS.DTO.Base;
using SMS.DTO.Category.Resquest;

namespace SMS.API.Api
{
    [Authorize]
    [RoutePrefix("api/category")]
    public class CategoryController : BaseApiController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public CategoryController(IProductService productService,
            ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        /// <summary>
        /// Tìm kiếm thể loại
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search")]
        public BaseResponse<BasePaginationSet<CategoryModel>> Search(string keyword, int page = SystemParam.PAGE, int pageSize = SystemParam.PAGE_SIZE)
        {
            BaseResponse<BasePaginationSet<CategoryModel>> response = new BaseResponse<BasePaginationSet<CategoryModel>>();
            response.ResponseCode = BaseCode.SUCCESS;
            var queryCategory = categoryService.GetAll().Where(x => x.IsActive);
            if (!string.IsNullOrEmpty(keyword))
            {
                queryCategory = queryCategory.Where(x => x.Alias.ToUpper().Contains(keyword.ToUpper()) || x.CategoryName.ToUpper().Contains(keyword.ToUpper()));
            }
                       
            IQueryable<CategoryModel> queryResponse = queryCategory.Select(x => new CategoryModel
            {
                CategoryName = x.CategoryName,
                CategoryID = x.CategoryID,
                Alias = x.Alias,
                //CreateBy = x.CreateBy,
                //CreateDate = x.CreateDate,
                HomeFlag = x.HomeFlag,
                IsActive = x.IsActive,
                MetaDescription = x.MetaDescription,
                MetaKeyword = x.MetaKeyword,
                //ModifiedDate = x.ModifiedDate,
                Sequence = x.OrderNumber,
                //UpdateBy = x.UpdateBy
            }).OrderBy(x => x.Sequence).ThenBy(x => x.Alias);

            int totalRow = queryResponse.Count();

            List<CategoryModel> lstResult = queryResponse.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var paginationset = new BasePaginationSet<CategoryModel>()
            {
                Items = lstResult,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalRow,
                TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
            };
            response.Data = paginationset;
            return response;
        }

        /// <summary>
        /// Get thể loại bởi id
        /// </summary>
        /// <param name="request"> request</param>
        /// <param name="id"> id thể loại</param>
        /// <returns></returns>
        [HttpGet]
        [Route("firstOrDefault")]
        public HttpResponseMessage FirstOrDefault(HttpRequestMessage request, long id)
        {
            return CreateHttpResponse(request, () =>
            {
                var objCategory = categoryService.GetAll().Where(x => x.IsActive && x.CategoryID == id);
                return request.CreateResponse(HttpStatusCode.OK, objCategory);
            });
        }

        ///// <summary>
        ///// Thêm mới một thể loại
        ///// </summary>
        ///// <param name="request"> request</param>
        ///// <param name="model">obj model thể loại</param>
        //[HttpPost]
        //[Route("create")]
        //public HttpResponseMessage Create(HttpRequestMessage request, CategoryModel model)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        BaseResponseMsgAPI responseApi = new BaseResponseMsgAPI();
        //        responseApi.StatusCode = (int)HttpStatusCode.OK;
        //        responseApi.IsSuccess = true;
        //        responseApi.Mesage = "Thêm mới thành công";

        //        if (!ModelState.IsValid)
        //        {
        //            responseApi.StatusCode = (int)HttpStatusCode.BadRequest;
        //            responseApi.IsSuccess = false;
        //            responseApi.Mesage = "Vui lòng nhập đầy đủ thông tin";
        //            return request.CreateResponse(HttpStatusCode.BadRequest, responseApi);
        //        }

        //        if (string.IsNullOrEmpty(model.CategoryName))
        //        {
        //            responseApi.StatusCode = (int)HttpStatusCode.BadRequest;
        //            responseApi.IsSuccess = false;
        //            responseApi.Mesage = "Vui lòng nhập tên thể loại";
        //            return request.CreateResponse(HttpStatusCode.BadRequest, responseApi);
        //        }

        //        Category objInsert = new Category()
        //        {
        //            CategoryName = model.CategoryName,
        //            Alias = model.Alias,
        //            CreateBy = model.Login_User_ID,
        //            CreateDate = DateTime.Now,
        //            HomeFlag = model.HomeFlag,
        //            IsActive = model.IsActive,
        //            MetaDescription = model.MetaDescription,
        //            MetaKeyword = model.MetaKeyword,
        //            OrderNumber = model.OrderNumber                    
        //        };
        //        Guid id = categoryService.Create(objInsert);
        //        if (id == null)
        //        {
        //            responseApi.StatusCode = (int)HttpStatusCode.BadRequest;
        //            responseApi.IsSuccess = false;
        //            responseApi.Mesage = "Thêm mới thất bại";
        //            return request.CreateResponse(HttpStatusCode.BadRequest, responseApi);
        //        }
        //        return request.CreateResponse(HttpStatusCode.OK, responseApi);
        //    });
        //}

        /// <summary>
        /// Thêm mới một thể loại
        /// </summary>
        /// <param name="request"> request</param>
        /// <param name="model">obj model thể loại</param>
        [HttpPost]
        [Route("create")]
        public BaseResponse<BusinessException> Create(AddOrEditCategoryRequest<CategoryModel> request)
        {
            BaseResponse<BusinessException> response = new BaseResponse<BusinessException>()
            {
                ResponseCode = BaseCode.SUCCESS,
                Message = "Thêm mới thành công",
                MsgType = BaseCode.SUCCESS_TYPE
            };

            try
            {
                Category objInsert = new Category()
                {
                    CategoryName = request.Model.CategoryName,
                    Alias = request.Model.Alias,
                    CreateBy = request.UserID,
                    CreateDate = DateTime.Now,
                    HomeFlag = request.Model.HomeFlag,
                    IsActive = request.Model.IsActive,
                    MetaDescription = request.Model.MetaDescription,
                    MetaKeyword = request.Model.MetaKeyword,
                    OrderNumber = request.Model.OrderNumber
                };
                categoryService.Create(objInsert);
                return response;
            }
            catch (BusinessException ex)
            {
                response.ResponseCode = BaseCode.CRUD_ERROR;
                response.Message = ex.Message;
                response.Data = ex;
                response.MsgType = BaseCode.ERROR_TYPE;
                return response;
            }
        }

        /// <summary>
        /// Cập nhật thể loại
        /// </summary>
        /// <param name="request"> request</param>
        /// <param name="model">obj model thể loại</param>
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, CategoryModel model)
        {
            return CreateHttpResponse(request, () =>
            {
                if (string.IsNullOrEmpty(model.CategoryName))
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, "Vui lòng nhập tên sản phẩm");
                }

                if (categoryService.GetAll().Any(x => x.CategoryID != model.CategoryID && model.CategoryName.ToUpper().Contains(x.CategoryName.ToUpper())))
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, "Cập nhật thất bại. Tên thể loại đã tồn tại");
                }

                var objCategory = categoryService.GetAll().FirstOrDefault(x => x.CategoryID == model.CategoryID);
                if (objCategory == null)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, "Cập nhật thất bại. Thể loại không tồn tại");
                }
                else
                {
                    categoryService.Update(objCategory);
                }

                return request.CreateResponse(HttpStatusCode.OK, "Cập nhật thành công");
            });
        }

        /// <summary>
        /// Xóa thể loại
        /// </summary>
        /// <param name="request"> request</param>
        /// <param name="strJsonId">json id</param>
        [HttpPut]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, string strJsonId)
        {
            return CreateHttpResponse(request, () =>
            {
                if (string.IsNullOrEmpty(strJsonId))
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, "Xóa thất bại. Vui lòng kiểm tra lại");
                }



                return request.CreateResponse(HttpStatusCode.OK, "Xóa thành công");
            });
        }
    }

    public class BaseResponseMsgAPI
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Mesage { get; set; }
        // public List<ResponseData> Data { get; set; }
    }
}
