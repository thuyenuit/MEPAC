using MEPAC.Reportsitory.Infrastructure.Interfaces;
using MEPAC.Reportsitory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEPAC.Model.Models;
using MEPAC.Business.Common;

namespace MEPAC.Business.Business
{
    public interface IPartnerBusiness
    {
        IQueryable<Partner> GetAll();
        Partner Create(Partner objInpput);
        void Delete(int Id);
        Partner GetById(int id);
        void SaveChange();
        void Update(Partner objInpput);
        IQueryable<Partner> Search(IDictionary<string, object> dic);
    }

    public class PartnerBusiness : IPartnerBusiness
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PartnerBusiness(
            IPartnerRepository _partnerRepository,
            IUnitOfWork _unitOfWork)
        {
            this._partnerRepository = _partnerRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<Partner> GetAll()
        {
            return _partnerRepository.GetAll();
        }

        public Partner Create(Partner objInpput)
        {
            return _partnerRepository.Create(objInpput);
        }

        public void Delete(int Id)
        {
            _partnerRepository.Delete(Id);
        }

        public Partner GetById(int id)
        {
            return _partnerRepository.GetSingleById(id);
        }
        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
        public void Update(Partner objInpput)
        {
            _partnerRepository.Update(objInpput);
        }

        public IQueryable<Partner> Search(IDictionary<string, object> dic)
        {
            string keyWord = Utils.GetString(dic, "KeyWord");
            int status = Utils.GetInt(dic, "Status");

            IQueryable<Partner> lstQuery = GetAll();

            if (status == 1)
                lstQuery = lstQuery.Where(x => x.IsActive == true);
            else if (status == 2)
                lstQuery = lstQuery.Where(x => x.IsActive == false);

            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = keyWord.ToUpper();
                lstQuery = lstQuery.Where(x => x.Display.ToUpper().Contains(keyWord));
            }

            return lstQuery;
        }
    }
}
