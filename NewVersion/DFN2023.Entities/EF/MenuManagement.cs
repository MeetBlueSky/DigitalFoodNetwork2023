using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.EF
{
    public class MenuManagement
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? ChildId { get; set; }
        public string Name { get; set; }
        public int? MenuFeatureId { get; set; }
        public int ClickType { get; set; }
        public int? OpeningType { get; set; }
        public int MenuLayer { get; set; }
        public string MenuLayerCode { get; set; }
        public string Image { get; set; }
        public String? Link { get; set; }
        public int Status { get; set; }
        public int RowNum { get; set; }
        public int? LangId { get; set; }
    }
}
