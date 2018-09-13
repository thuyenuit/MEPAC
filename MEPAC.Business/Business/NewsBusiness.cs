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
    public interface INewsBusiness
    {
        IQueryable<News> GetAll();
        IQueryable<News> Search(IDictionary<string, object> dic);
    }

    public class NewsBusiness : INewsBusiness
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubMenuBusiness _subMenuBusiness;
        public NewsBusiness(
            INewsRepository _newsRepository,
            ISubMenuBusiness _subMenuBusiness,
            IUnitOfWork _unitOfWork)
        {
            this._newsRepository = _newsRepository;
            this._subMenuBusiness = _subMenuBusiness;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<News> GetAll()
        {
            return _newsRepository.GetAll();
        }

        public IQueryable<News> Search(IDictionary<string, object> dic)
        {
            string keyWord = Utils.GetString(dic, "KeyWord");
            int status = Utils.GetInt(dic, "Status");
            int typeNewsID = Utils.GetInt(dic, "TypeNewsID");

            IQueryable<News> lstQuery = GetAll();

            if (status == 1)
                lstQuery = lstQuery.Where(x => x.IsActive == true);
            else if (status == 2)
                lstQuery = lstQuery.Where(x => x.IsActive == false);

            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = keyWord.ToUpper();
                lstQuery = lstQuery.Where(x => x.DisplayName.ToUpper().Contains(keyWord));
            }

            if (typeNewsID > 0)
            {
                lstQuery = lstQuery.Where(x => x.SubMenus.SubMenuID == typeNewsID);
            }

            return lstQuery;
        }

    }
}
