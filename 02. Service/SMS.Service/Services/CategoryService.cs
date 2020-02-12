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
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Create(Category entity)
        {
            if (string.IsNullOrEmpty(entity.CategoryName))
            {
                throw new BusinessException("Vui lòng nhập tên thể loại");
            }

            if (string.IsNullOrEmpty(entity.CategoryName))
            {
                throw new BusinessException("Alias không được để trống");
            }

            if (GetAll().Any(x => x.CategoryName.ToUpper().Equals(entity.CategoryName.ToUpper())))
            {
                throw new BusinessException("Tên thể loại đã tồn tại. Vui lòng kiểm tra lại");
            }

            if (string.IsNullOrEmpty(entity.Alias))
            {
                entity.Alias = entity.CategoryName.GetSeoTitle();
            }

            categoryRepository.Create(entity);
            SaveChanges();
        }

        public void Update(Category entity)
        {
            var objEntity = categoryRepository.GetAll.FirstOrDefault(x => x.CategoryID == entity.CategoryID);
            if (objEntity != null)
            {
                categoryRepository.Update(entity);
            }        
        }

        public IQueryable<Category> GetAll()
        {
            return categoryRepository.GetAll;
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
