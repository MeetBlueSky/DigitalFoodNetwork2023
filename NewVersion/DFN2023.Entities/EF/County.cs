using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.EF
{
    public class County
    {
        //public County()
        //{
        //    //OfficialCompany = new HashSet<Company>();
        //    //MapCompany = new HashSet<Company>();
        //}

        public int Id { get; set; }
        public int? CityId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int LangId { get; set; }
        public int RowNum { get; set; }


        public virtual City? City { get; set; }


        //[InverseProperty("OfficialCounty")]
        //public virtual ICollection<Company>? OfficialCompany { get; set; }

        //[InverseProperty("MapCounty")]
        //public virtual ICollection<Company>? MapCompany { get; set; }

    }

}
