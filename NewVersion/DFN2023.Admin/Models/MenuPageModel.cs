using DFN2023.Entities.DTO;
using System.Collections.Generic;

namespace DFN2023.Admin.Models
{
    public class MenuPageModel
    {
        public List<MenuManagementDTO> ParentList { get; set; }
        public List<MenuManagementDTO> ChildList { get; set; }
    }
}
