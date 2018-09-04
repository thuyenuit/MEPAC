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
    public interface IProjectsBusiness
    {
        IQueryable<Projects> GetAll();
        IQueryable<Projects> Search(IDictionary<string, object> dic);
        Projects Create(Projects objInpput);
    }

    public class ProjectsBusiness : IProjectsBusiness
    {
        private readonly IProjectsRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProjectsBusiness(
            IProjectsRepository _projectRepository,
            IUnitOfWork _unitOfWork)
        {
            this._projectRepository = _projectRepository;
            this._unitOfWork = _unitOfWork;
        }

        public Projects Create(Projects objInpput)
        {
            return _projectRepository.Create(objInpput);
        }

        public IQueryable<Projects> GetAll()
        {
            return _projectRepository.GetAll();
        }

        public IQueryable<Projects> Search(IDictionary<string, object> dic)
        {
            string keyWord = Utils.GetString(dic, "KeyWord");
            int status = Utils.GetInt(dic, "Status");

            IQueryable<Projects> lstQuery = GetAll();

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
