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
    public interface IRangeBusiness
    {
        IQueryable<Range> GetAll();
        Range GetById(int id);
        IQueryable<Range> Search(IDictionary<string, object> dic);
        Range Create(Range objInpput);
        void Update(Range objInpput);
        void Save();
        void Delete(int Id);
    }

    public class RangeBusiness : IRangeBusiness
    {
        private readonly IRangeRepository _rangeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RangeBusiness(
            IRangeRepository _rangeRepository,
            IUnitOfWork _unitOfWork)
        {
            this._rangeRepository = _rangeRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<Range> GetAll()
        {
            return _rangeRepository.GetAll();
        }

        public IQueryable<Range> Search(IDictionary<string, object> dic)
        {
            string keyWord = Utils.GetString(dic, "KeyWord");
            int status = Utils.GetInt(dic, "Status");

            IQueryable<Range> lstQuery = GetAll();

            if (status == 1)
                lstQuery = lstQuery.Where(x => x.SubMenu.IsActive == true);
            else if (status == 2)
                lstQuery = lstQuery.Where(x => x.SubMenu.IsActive == false);

            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = keyWord.ToUpper();
                lstQuery = lstQuery.Where(x => x.SubMenu.Display.ToUpper().Contains(keyWord));
            }

            return lstQuery;
        }

        public Range GetById(int id)
        {
            return _rangeRepository.GetSingleById(id);
        }

        public Range Create(Range objInpput)
        {
            return _rangeRepository.Create(objInpput);
        }

        public void Update(Range objInpput)
        {
            _rangeRepository.Update(objInpput);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Delete(int Id)
        {
            _rangeRepository.Delete(Id);
        }

    }
}
