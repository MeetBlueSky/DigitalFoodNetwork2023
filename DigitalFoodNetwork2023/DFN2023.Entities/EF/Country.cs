using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Entities.EF
{
    public class Country
    {
        public Country()
        {
            City = new HashSet<City>();
            OfficialCompany = new HashSet<Company>();
            MapCompany = new HashSet<Company>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int LangId { get; set; }
        public int RowNum { get; set; }


        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Company> OfficialCompany { get; set; }
        public virtual ICollection<Company> MapCompany { get; set; }

    }

}


