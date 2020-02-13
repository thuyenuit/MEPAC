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
using SMS.DTO.ProductCategory.Model;

namespace SMS.API.Api
{
    [Authorize]
    [RoutePrefix("api/product-category")]
    public class ProductCategoryController : BaseApiController
    {
        private readonly IProductCategoryService productCategoryService;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductCategoryController(IProductService productService,
            ICategoryService categoryService,
            IProductCategoryService productCategoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.productCategoryService = productCategoryService;
        }

        /// <summary>
        /// Tìm kiếm thể loại
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search")]
        public BaseResponse<BasePaginationSet<ProductCategoryViewModel>> Search(string keyword, int page = SystemParam.PAGE, int pageSize = SystemParam.PAGE_SIZE)
        {
            BaseResponse<BasePaginationSet<ProductCategoryViewModel>> response = new BaseResponse<BasePaginationSet<ProductCategoryViewModel>>();
            response.ResponseCode = BaseCode.SUCCESS;
            var queryCategory = categoryService.GetAll().Where(x => x.IsActive);
            var queryProductCategory = productCategoryService.GetAll().Where(x => x.IsActive);

            IQueryable<ProductCategoryViewModel> queryResponse = queryProductCategory.Join(queryCategory, pc => pc.CategoryID, c => c.CategoryID, (pc, c) => new { pc = pc, c = c })
                .Select(x => new ProductCategoryViewModel
                {
                    Name = x.pc.Name,
                    CategoryID = x.pc.CategoryID,
                    Alias = x.pc.Alias,
                    HomeFlag = x.pc.IsHomeFlag,
                    IsActive = x.pc.IsActive,
                    Sequence = x.pc.Sequence,
                    CategoryName = x.c.Name,
                }).OrderBy(x => x.Sequence).ThenBy(x => x.Alias);

            int totalRow = queryResponse.Count();

            List<ProductCategoryViewModel> lstResult = queryResponse.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var paginationset = new BasePaginationSet<ProductCategoryViewModel>()
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
                    Name = request.Model.CategoryName,
                    Alias = request.Model.Alias,
                    CreateBy = request.UserID,
                    CreateDate = DateTime.Now,
                    IsHomeFlag = request.Model.HomeFlag,
                    IsActive = request.Model.IsActive,
                    MetaDescription = request.Model.MetaDescription,
                    MetaKeyword = request.Model.MetaKeyword,
                    Sequence = request.Model.Sequence
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

                if (categoryService.GetAll().Any(x => x.CategoryID != model.CategoryID && model.CategoryName.ToUpper().Contains(x.Name.ToUpper())))
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
}
