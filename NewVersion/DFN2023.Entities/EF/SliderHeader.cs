using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.EF
{
    public class SliderHeader
    {
        public SliderHeader()
        {
            Slider = new HashSet<Slider>();
        }
        public int Id { get; set; }
        public string SliderName { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }


        public virtual ICollection<Slider> Slider { get; set; }
    }
}
