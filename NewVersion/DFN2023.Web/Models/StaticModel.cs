using DFN2023.Entities.DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFN2023.Web.Models
{
    public class StaticModel
    {
        public StaticContentGrupPageDTO? StaticContentGrupPage { get; set; }
        public StaticContentPageDTO? StaticContentPage { get; set; }


        public List<StaticContentGrupPageDTO>? StaticContentGrupPageList { get; set; }
        public List<StaticContentPageDTO>? StaticContentPageList { get; set; }
        public List<StaticContentPageDTO>? StaticContentPageList2 { get; set; }


        public List<SliderDTO>? SlaytList { get; set; }
    }
}
