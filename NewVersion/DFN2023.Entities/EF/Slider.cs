using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Entities.EF
{
  public  class Slider
    {
        public int Id { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public string? Target { get; set; }
        public int? Type { get; set; }
        public string? Header { get; set; }
        public string? Text { get; set; }
        public int RowNum { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string? LastIP { get; set; }
        public int Status { get; set; }
        public int LangId { get; set; }


        public virtual SliderHeader? SliderHeader { get; set; }
    }
}
