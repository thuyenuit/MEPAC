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
        Projects GetById(int id);
        Projects Create(Projects objInpput);
        void Create(Projects objInpput, List<string> lstImages);
        void Update(Projects objInpput);
        void Update(Projects objInpput, List<string> lstImages);
        void Delete(int Id);
        void SaveChange();
    }

    public class ProjectsBusiness : IProjectsBusiness
    { 
        private readonly IMetaImageBusiness _metaImageBusiness;
        private readonly IProjectsRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProjectsBusiness(
            IProjectsRepository _projectRepository,          
            IMetaImageBusiness _metaImageBusiness,
            IUnitOfWork _unitOfWork)
        {
            this._metaImageBusiness = _metaImageBusiness;
            this._projectRepository = _projectRepository;
            this._unitOfWork = _unitOfWork;
        }

        public Projects Create(Projects objInpput)
        {
            return _projectRepository.Create(objInpput);
        }

        public void Create(Projects objInpput, List<string> lstImages)
        {
            try
            {
                Projects objP = _projectRepository.Create(objInpput);
                _unitOfWork.Commit();

                if (lstImages.Count() > 0)
                {
                    int projectID = objP.ProjectID;
                    foreach (var item in lstImages)
                    {
                        MetaImage objImage = new MetaImage();
                        objImage.ParentID = projectID;
                        objImage.Link = item;
                        objImage.IsActive = true;
                        objImage.TypeID = 1;
                        objImage.MetaImageID = 0;
                        _metaImageBusiness.Create(objImage);
                    }
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {

            }    
        }

        public void Delete(int Id)
        {
            _projectRepository.Delete(Id);
        }

        public IQueryable<Projects> GetAll()
        {
            return _projectRepository.GetAll();
        }

        public Projects GetById(int id)
        {
            return _projectRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
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

        public void Update(Projects objInpput)
        {
           _projectRepository.Update(objInpput);
        }

        public void Update(Projects objInpput, List<string> lstImages)
        {
            try
            {
                var result = _projectRepository.GetSingleById(objInpput.ProjectID);

                if (result != null)
                {
                    result.Display = objInpput.Display;
                    result.FromDate = objInpput.FromDate;
                    result.ToDate = objInpput.ToDate;
                    result.Image = objInpput.Image;
                    result.Description = objInpput.Description;
                    result.IsActive = objInpput.IsActive;
                    result.IsShow = objInpput.IsShow;
                    result.PostBy = objInpput.PostBy;
                    result.PostDate = objInpput.PostDate;
                    result.UpdateBy = objInpput.UpdateBy;
                    result.UpdateDate = objInpput.UpdateDate;
                    result.MetaDescription = objInpput.MetaDescription;
                    result.MetaKeyword = objInpput.MetaKeyword;

                    _projectRepository.Update(result);
                    _unitOfWork.Commit();

                    if (lstImages.Count() > 0)
                    {
                        int projectID = result.ProjectID;

                        List<MetaImage> lstImageOfProject = _metaImageBusiness.GetAll()
                            .Where(x => x.ParentID == projectID && x.TypeID == ParamFile.TYPE_IMAGE_PROJECT).ToList();

                        foreach (var item in lstImageOfProject)
                        {
                            _metaImageBusiness.Delete(item.MetaImageID);
                        }
                        _metaImageBusiness.Save();

                        foreach (var item in lstImages)
                        {
                            MetaImage objImage = new MetaImage();
                            objImage.ParentID = projectID;
                            objImage.Link = item;
                            objImage.IsActive = true;
                            objImage.TypeID = ParamFile.TYPE_IMAGE_PROJECT;
                            _metaImageBusiness.Create(objImage);
                        }
                         _metaImageBusiness.Save();
                    }

                   // _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {

            }
           

            
        }
    }
}
