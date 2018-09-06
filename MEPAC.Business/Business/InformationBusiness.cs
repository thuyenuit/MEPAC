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
    public interface IInformationBusiness
    {
        IQueryable<Information> GetAll();
        void Update(Information obj);
        void SaveChange();
    }

    public class InformationBusiness : IInformationBusiness
    {
        private readonly IInformationRepository _informationRepository;
        private readonly IUnitOfWork _unitOfWork;
        public InformationBusiness(
            IInformationRepository _informationRepository,
            IUnitOfWork _unitOfWork)
        {
            this._informationRepository = _informationRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<Information> GetAll()
        {
            return _informationRepository.GetAll();
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Information obj)
        {
            Information result = _informationRepository.GetSingleById(obj.InformationID);
            if (result != null)
            {
                result.Address1 = obj.Address1;
                result.Address2 = obj.Address2;
                result.Address3 = obj.Address3;
                result.Address4 = obj.Address4;
                result.Address5 = obj.Address5;
                result.Content = obj.Content;
                result.Display = obj.Display;
                result.Email = obj.Email;
                result.MetaDescription = obj.MetaDescription;
                result.MetaKeyword = obj.MetaKeyword;
                result.Phone = obj.Phone;
                result.Website = obj.Website;
                result.UpdateDate = DateTime.Now;

                _informationRepository.Update(result);
                _unitOfWork.Commit();
            }
        }
    }
}
