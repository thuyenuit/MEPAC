using MEPAC.Model.Models;
using MEPAC.Reportsitory.Infrastructure.Interfaces;
using MEPAC.Reportsitory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Business.Business
{
    public interface IContactBusiness
    {
        IQueryable<Contact> GetAll();
    }

    public class ContactBusiness : IContactBusiness
    {
         private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ContactBusiness(
            IContactRepository _contactRepository,
            IUnitOfWork _unitOfWork)
        {
            this._contactRepository = _contactRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<Contact> GetAll()
        {
            return _contactRepository.GetAll();
        }

    }
}
