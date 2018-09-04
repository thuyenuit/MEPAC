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
    public interface INewsBusiness
    {
        IQueryable<News> GetAll();
    }

    public class NewsBusiness : INewsBusiness
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public NewsBusiness(
            INewsRepository _newsRepository,
            IUnitOfWork _unitOfWork)
        {
            this._newsRepository = _newsRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<News> GetAll()
        {
            return _newsRepository.GetAll();
        }

    }
}
