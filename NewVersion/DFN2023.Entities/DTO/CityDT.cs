using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Entities.DTO
{
    public class CityDT
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }/*fk*/
        public string Name { get; set; }
        public int LangId { get; set; }
        public int Status { get; set; }
        public int RowNum { get; set; }
      
        public string  Country { get; set; }
        
    }
}
