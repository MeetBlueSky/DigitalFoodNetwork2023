using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Entities.DTO
{
    public class SliderHeaderDT
    {
        public int Id { get; set; }
        public string SliderName { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
    }
}
