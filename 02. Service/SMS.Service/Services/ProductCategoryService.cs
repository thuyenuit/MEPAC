using SMS.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Model.Models;
using SMS.DAL.Repositories;
using SMS.DAL.Infrastructure.Interfaces;
using SMS.Shared.Shares;
using SMS.DTO.Base;

namespace SMS.Service.Services
{
    public class ProductCategoryService: IProductCategoryService
    {
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductCategoryService(
            IProductCategoryRepository productCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            this.productCategoryRepository = productCategoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Create(ProductCategory entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new BusinessException("Vui lòng nhập tên thể loại");
            }

            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new BusinessException("Alias không được để trống");
            }

            if (GetAll().Any(x => x.Name.ToUpper().Equals(entity.Name.ToUpper())))
            {
                throw new BusinessException("Tên thể loại đã tồn tại. Vui lòng kiểm tra lại");
            }

            if (string.IsNullOrEmpty(entity.Alias))
            {
                entity.Alias = entity.Name.GetSeoTitle();
            }

            productCategoryRepository.Create(entity);
            SaveChanges();
        }

        public void Update(ProductCategory entity)
        {
            var objEntity = productCategoryRepository.GetAll.FirstOrDefault(x => x.ProductCategoryID == entity.ProductCategoryID);
            if (objEntity != null)
            {
                productCategoryRepository.Update(entity);
            }        
        }

        public IQueryable<ProductCategory> GetAll()
        {
            return productCategoryRepository.GetAll;
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
