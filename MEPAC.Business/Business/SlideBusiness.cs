using MEPAC.Reportsitory.Infrastructure.Interfaces;
using MEPAC.Reportsitory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEPAC.Model.Models;

namespace MEPAC.Business.Business
{
    public interface ISlideBusiness
    {
        IQueryable<Slide> GetAll();
        Slide GetById(int id);
        Slide Create(Slide objInpput);
        void Update(Slide objInpput);
        void Delete(int Id);
        void SaveChange();
    }

    public class SlideBusiness : ISlideBusiness
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SlideBusiness(
            ISlideRepository _slideRepository,
            IUnitOfWork _unitOfWork)
        {
            this._slideRepository = _slideRepository;
            this._unitOfWork = _unitOfWork;
        }

        public Slide Create(Slide objInpput)
        {
            return _slideRepository.Create(objInpput);
        }

        public void Delete(int Id)
        {
            _slideRepository.Delete(Id);
        }

        public IQueryable<Slide> GetAll()
        {
            return _slideRepository.GetAll();
        }

        public Slide GetById(int id)
        {
            return _slideRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide objInpput)
        {
            Slide objSlide = _slideRepository.GetSingleById(objInpput.SlideID);

            if (objSlide != null)
            {
                objSlide.Content = objInpput.Content;
                objSlide.Image = objInpput.Image;
                objSlide.MetaDescription = objInpput.MetaDescription;
                objSlide.MetaKeyword = objInpput.MetaKeyword;
                objSlide.IsActive = objInpput.IsActive;
                objSlide.UpdateBy = objInpput.UpdateBy;
                objSlide.UpdateDate = objInpput.UpdateDate;
                _slideRepository.Update(objSlide);
                _unitOfWork.Commit();
            }

           
        }
    }
}
