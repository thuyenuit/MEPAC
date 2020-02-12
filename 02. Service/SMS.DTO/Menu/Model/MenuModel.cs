using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DTO.Menu.Model
{
    public class MenuModel
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int OrderNumber { get; set; }
        public List<SubMenuModel> SubMenus { get; set; }
    }
}
