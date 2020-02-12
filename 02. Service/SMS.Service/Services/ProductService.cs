using SMS.DAL.Repositories;
using SMS.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Model.Models;
using SMS.DAL.Infrastructure.Interfaces;
using SMS.Service.ServiceObject;

namespace SMS.Service.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductDetailRepository productDetailRepository;
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(
            IProductRepository productRepository,
            IProductDetailRepository productDetailRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.productDetailRepository = productDetailRepository;
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Create(ProductSO entity)
        {
            Product objNew = new Product() {
                CategoryID = entity.CategoryID,
                ProductName = entity.ProductName,
                ProductCode = entity.ProductCode,
                Alias = entity.Alias,
                Image =entity.Image,
                MoreImage = entity.MoreImage,
                Content = entity.Content,
                OrderNumber =entity.OrderNumber,
                HomeFlag =entity.HomeFlag,
                HotFlag = entity.HotFlag,
                Status = entity.Status,
                Tags = entity.Tags,
                CreateBy = entity.CreateBy,
                CreateDate = entity.CreateDate,
                MetaDescription = entity.MetaDescription,
                MetaKeyword = entity.MetaKeyword,
                IsActive = true,            
            }; 
                   
            productRepository.Create(objNew);
            this.SaveChanges();

            ProductDetail objNewDetail = new ProductDetail() {
                ProductID = objNew.ProductID,
                Quantity = entity.Quantity,
                PriceInput = entity.PriceInput,
                PriceSale = entity.PriceSale,
                CreateBy = entity.CreateBy,
                CreateDate = entity.CreateDate,
            };

            productDetailRepository.Create(objNewDetail);
            this.SaveChanges();
        }

        public void Update(ProductSO entity)
        {
            //productRepository.Update(entity);
        }

        public IQueryable<ProductSO> Search(IDictionary<string, object> dic)
        {
            string productName = null;
            string productCode = null;
            string keyword = null;
            Guid categoryId = new Guid() ;
            int statusId = 0;
            bool checkHomeFlag = false;
            bool iskHomeFlag = false;
            bool checkHotFlag = false;
            bool iskHotFlag = false;

            IQueryable<Product> queryProduct = productRepository.GetAll.Where(x=>x.IsActive);
            IQueryable<Category> queryCategory = categoryRepository.GetAll.Where(x=>x.IsActive);

            if (!string.IsNullOrEmpty(productName))
                queryProduct = queryProduct.Where(x => productName.ToUpper().Contains(x.ProductName.ToUpper()));

            if (!string.IsNullOrEmpty(productCode))
                queryProduct = queryProduct.Where(x => productCode.ToUpper().Contains(x.ProductCode.ToUpper()));

            if (!string.IsNullOrEmpty(keyword))
                queryProduct = queryProduct.Where(x => keyword.ToUpper().Contains(x.ProductName.ToUpper())
                                             || keyword.ToUpper().Contains(x.ProductCode.ToUpper()));

            if (statusId > 0)
                queryProduct = queryProduct.Where(x => x.Status == statusId);

            if (checkHomeFlag)
            {
                queryProduct = queryProduct.Where(x => x.HomeFlag == iskHomeFlag);
            }

            if (checkHotFlag)
            {
                queryProduct = queryProduct.Where(x => x.HotFlag == iskHotFlag);
            }

            //if(categoryId)
            //queryCategory = queryCategory.Where(x => x.CategoryID == categoryId);

            IQueryable<ProductSO> result = (from pr in queryProduct
                                            join ca in queryCategory on pr.CategoryID equals ca.CategoryID
                                            join prd in productDetailRepository.GetAll.Where(x=>x.IsActive).OrderByDescending(x=>x.CreateDate) 
                                                on pr.ProductID equals prd.ProductID into j1
                                            from g1 in j1.DefaultIfEmpty()
                                            select new ProductSO()
                                            {
                                                ProductID = pr.ProductID,
                                                ProductCode =pr.ProductCode,
                                                ProductName = pr.ProductName,
                                                Image = pr.Image,
                                                Quantity = g1 != null ? g1.Quantity : 0,
                                                PriceInput = g1 != null ? g1.PriceInput : null,
                                                PriceSale = g1 != null ? g1.PriceSale : null,
                                                Alias = pr.Alias,
                                                AliasCategory = ca.Alias,
                                                CategoryID = pr.CategoryID,
                                                CategoryName = ca.CategoryName,
                                                Content = pr.Content,
                                                CreateBy =pr.CreateBy,
                                                CreateDate =pr.CreateDate,
                                                HomeFlag = pr.HomeFlag,
                                                HotFlag = pr.HotFlag,
                                                IsActive = pr.IsActive,
                                                MetaDescription =pr.MetaDescription,
                                                MetaKeyword = pr.MetaKeyword,
                                                ModifiedDate = pr.ModifiedDate,
                                                MoreImage =pr.MoreImage,
                                                OrderNumber = pr.OrderNumber,
                                                OrderNumberCategroy = ca.OrderNumber,
                                                Status = pr.Status,
                                                Tags = pr.Tags,
                                                UpdateBy = pr.UpdateBy
                                            }).OrderBy(x => x.OrderNumberCategroy).ThenBy(x => x.AliasCategory).ThenBy(x=>x.OrderNumber).ThenBy(x=>x.Alias);

            return result;
        }

        public IQueryable<Product> GetAll()
        {
           return productRepository.GetAll; 
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
