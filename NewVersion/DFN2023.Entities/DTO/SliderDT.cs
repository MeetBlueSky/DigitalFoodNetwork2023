using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Entities.DTO
{
    public class SliderDT
    {
        public int Id { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Target { get; set; }
        public int Type { get; set; }
        public int RowNum { get; set; }
        public int Status { get; set; }
        public int LangId { get; set; }


        public string? SliderHeaderName { get; set; } 
    }
}
