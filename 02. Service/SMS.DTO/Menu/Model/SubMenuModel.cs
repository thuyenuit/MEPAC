using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DTO.Menu.Model
{
    public class SubMenuModel
    {
        public int SubMenuID { get; set; }
        public int MenuID { get; set; }
        public string SubMenuName { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int OrderNumber { get; set; }
    }
}
