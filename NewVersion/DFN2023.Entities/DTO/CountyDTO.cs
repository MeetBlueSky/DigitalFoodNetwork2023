using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.DTO
{
    public class CountyDTO
    {

        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int LangId { get; set; }
        public int RowNum { get; set; }

    }

}
