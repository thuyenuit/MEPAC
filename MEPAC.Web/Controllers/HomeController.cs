using MEPAC.Business;
using MEPAC.Business.Business;
using MEPAC.Model.Models;
using MEPAC.Web.Models;
using MEPAC.WebAdmin.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MEPAC.Web.Controllers
{
    public class HomeController : BaseController
    {
        IRangeBusiness _rangeBusiness;
        ISubMenuBusiness _subMenuBusiness;
        IMenuBusiness _menuBusiness;
        ISlideBusiness _slideBusiness;
        IProjectsBusiness _projectsBusiness;
        IMetaImageBusiness _metaImageBusiness;
        IInformationBusiness _informationBusiness;
        public HomeController(ISlideBusiness _slideBusiness,
            IRangeBusiness _rangeBusiness,
        ISubMenuBusiness _subMenuBusiness,
        IMenuBusiness _menuBusiness,
         IMetaImageBusiness _metaImageBusiness,
        IProjectsBusiness _projectsBusiness,
        IInformationBusiness _informationBusiness) {

            this._slideBusiness = _slideBusiness;
            this._projectsBusiness = _projectsBusiness;
            this._rangeBusiness = _rangeBusiness;
            this._subMenuBusiness = _subMenuBusiness;
            this._menuBusiness = _menuBusiness;
            this._metaImageBusiness =_metaImageBusiness;
            this._informationBusiness = _informationBusiness;
    }

        public ActionResult Index()
        {
            List<Slide> lstSlide = _slideBusiness.GetAll().Where(x => x.IsActive).ToList();
            ViewData["LIST_SLIDE"] = lstSlide;

            List<Projects> lstProject = _projectsBusiness.GetAll().Where(x => x.IsActive && x.IsShow && x.IsRepresentative).ToList();
            ViewData["LIST_PROJECT_REP"] = lstProject;

            IDictionary<string, object> dic = new Dictionary<string, object>();
            var lstRangeDB = _rangeBusiness.Search(dic);
            var lstSubMenu = _subMenuBusiness.GetAll().Where(x => x.MenuID == 3);
            var lstMenu = _menuBusiness.GetAll();

            List<RangeViewModel> lstRangeVM = (from sm in lstSubMenu
                                               join r in lstRangeDB on sm.SubMenuID equals r.SubMenuID into rt
                                               from r in rt.DefaultIfEmpty()
                                               join m in lstMenu on sm.MenuID equals m.MenuID
                                               select new RangeViewModel
                                               {
                                                   RangeID = r != null ? r.RangeID : 0,
                                                   Cotntent = r != null ? r.Cotntent : "",
                                                   MenuID = m.MenuID,
                                                   MenuName = m.Display,
                                                   SubMenuID = sm.SubMenuID,
                                                   LinkImage = sm.Image,
                                                   SubMenuName = sm.Display,
                                                   MetaKeyword = r != null ? r.MetaKeyword : "",
                                                   MetaDescription = r != null ? r.MetaDescription : "",
                                                   CreateBy = r != null ? r.CreateBy : "",
                                                   CreateDate = r.CreateDate,
                                                   UpdateBy = r != null ? r.UpdateBy : "",
                                                   UpdateDate = r != null ? r.UpdateDate : null,
                                                   OrderBy = sm.Order
                                               }).OrderBy(x=>x.OrderBy).ToList();
            ViewData["LIST_RANGE"] = lstRangeVM;


            return View();
        }

        public ActionResult About()
        {
            Information objInfo = _informationBusiness.GetAll().FirstOrDefault();
            return View(objInfo);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Project()
        {
            List<ClientYearViewModel> lstYearsVM = new List<ClientYearViewModel>();
            int currentYear = DateTime.Now.Year;
            for (int i = 2005; i <= 2018; i++)
            {
                ClientYearViewModel objYear = new ClientYearViewModel();
                objYear.Display = i.ToString();
                objYear.YearID = i;
                objYear.Selected = i == currentYear ? true : false;
                if (i == 2018 && !lstYearsVM.Any(x => x.Selected))
                {
                    objYear.Selected = true;
                }
                lstYearsVM.Add(objYear);
            }

            List<ClientProjectViewMdel> lstProjectsCompleteVM = new List<ClientProjectViewMdel>();
            for (int i = 0; i < 8; i++)
            {
                ClientProjectViewMdel objProject = new ClientProjectViewMdel();
                objProject.ProjectID = (i + 100);
                objProject.Year = "2018";
                objProject.IsComplete = true;
                objProject.LinkImage = LinkImage(i);
                objProject.MetaDescription = "Mô tả dự án - SEO";
                objProject.Display = ProjectName(i);
                lstProjectsCompleteVM.Add(objProject);
            }

            List<ProjectsViewModel> lstProjectsUnCompleteVM = new List<ProjectsViewModel>();

            ViewData["LIST_YEAR"] = lstYearsVM.OrderByDescending(x => x.Display).ToList();
            ViewData["LIST_PROJECT_COMPLETE"] = lstProjectsCompleteVM;
            ViewData["LIST_PROJECT_UNCOMPLETE"] = lstProjectsUnCompleteVM;

            ViewBag.Title = "Danh sách các dự án theo năm";
            return View();
        }

        private string LinkImage(int i)
        {
            string link = "";
            switch (i)
            {
                case 0:
                    link = "/Assets/Client/images/duantieubieu1.jpg";
                    break;
                case 1:
                    link = "/Assets/Client/images/duantieubieu2.jpg";
                    break;
                case 2:
                    link = "/Assets/Client/images/duantieubieu3.jpg";
                    break;
                case 3:
                    link = "/Assets/Client/images/van-phong-cho-thue-binh-thanh-coteccons-building-184470-1_221854.jpg";
                    break;
                case 4:
                    link = "/Assets/Client/images/CTBrotex.jpg";
                    break;
                case 5:
                    link = "/Assets/Client/images/bmwindow.jpg";
                    break;
                case 6:
                    link = "/Assets/Client/images/BKE_1474597585.jpg";
                    break;
                case 7:
                    link = "/Assets/Client/images/SC_VivoCity_Tet_640_auto.JPG";
                    break;
            }
            return link;
        }

        private string ProjectName(int i)
        {
            string link = "";
            switch (i)
            {
                case 0:
                    link = "Vinhomes Golden River";
                    break;
                case 1:
                    link = "The Goold View";
                    break;
                case 2:
                    link = "Masteri Thảo Điền";
                    break;
                case 3:
                    link = "Tòa nhà Coteccons";
                    break;
                case 4:
                    link = "Công trình Brotex";
                    break;
                case 5:
                    link = "Công trình BM Windown";
                    break;
                case 6:
                    link = "Công trình Worldon";
                    break;
                case 7:
                    link = "Nhà hàng MC Donald's Vivo";
                    break;
            }
            return link;
        }


        public ActionResult ProjectsDetail(int id)
        {
            ProjectsViewModel obVM = new ProjectsViewModel();
            Projects obj = _projectsBusiness.GetById(id);
            if (obj != null)
            {
               
                obVM.ProjectID = obj.ProjectID;
                obVM.Display = obj.Display;
                obVM.LinkImage = obj.Image;
                obVM.Description = obj.Description;
                obVM.CreateBy = obj.CreateBy;
                obVM.CreateDate = obj.CreateDate;
                obVM.IsActive = obj.IsActive;
                obVM.IsShow = obj.IsShow;
                obVM.PostBy = obj.PostBy;
                obVM.PostDate = obj.PostDate;
                obVM.MetaDescription = obj.MetaDescription;
                obVM.MetaKeyword = obj.MetaKeyword;
                obVM.IsRepresentative = obj.IsRepresentative;

                List<MetaImage> lstMetaImage = _metaImageBusiness.GetAll().Where(x => x.TypeID == ParamFile.TYPE_IMAGE_PROJECT && x.ParentID == obj.ProjectID).ToList();
                if (lstMetaImage.Count() > 0)
                {
                    List<string> lstImage = lstMetaImage.Select(x => x.Link).ToList();
                    obVM.ListMoreImage = lstImage;
                }  
            }

            return View(obVM);
        }
    }
}