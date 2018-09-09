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

    public interface IHiringBusiness
    {
        IQueryable<Hiring> GetAll();

        Hiring GetById(int id);
        void SaveChange();
        void Update(Hiring objInpput);
        Hiring Create(Hiring objInpput);
        IQueryable<Hiring> Search(IDictionary<string, object> dic);
        void Delete(int Id);
    }

    public class HiringBusiness : IHiringBusiness
    {
        private readonly IHiringRepository _hiringRepository;
        private readonly IUnitOfWork _unitOfWork;
        public HiringBusiness(
            IHiringRepository _hiringRepository,
            IUnitOfWork _unitOfWork)
        {
            this._hiringRepository = _hiringRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<Hiring> GetAll()
        {
            return _hiringRepository.GetAll();
        }

        public Hiring GetById(int id)
        {
            return _hiringRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Hiring objInpput)
        {
            _hiringRepository.Update(objInpput);
        }


        public Hiring Create(Hiring objInpput)
        {
            return _hiringRepository.Create(objInpput);
        }

        public void Delete(int Id)
        {
            _hiringRepository.Delete(Id);
        }

        public IQueryable<Hiring> Search(IDictionary<string, object> dic)
        {
            string keyWord = Utils.GetString(dic, "KeyWord");
            int status = Utils.GetInt(dic, "Status");

            IQueryable<Hiring> lstQuery = GetAll();

            //if (status == 1)
            //    lstQuery = lstQuery.Where(x => x.IsActive == true);
            //else if (status == 2)
            //    lstQuery = lstQuery.Where(x => x.IsActive == false);

            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = keyWord.ToUpper();
                lstQuery = lstQuery.Where(x => x.Position.ToUpper().Contains(keyWord));
            }

            return lstQuery;
        }

    }
}
