using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Entities.DTO
{
    public class CountyDT
    {
        public int Id { get; set; }
        public int? CityId { get; set; }/*fk*/
        public string Name { get; set; }
        public int LangId { get; set; }
        public int Status { get; set; }
        public int RowNum { get; set; }
      
        public string  City { get; set; }
        
    }
}
