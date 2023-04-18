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
            Company = new HashSet<Company>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int LangId { get; set; }
        public int RowNum { get; set; }


        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Company> Company { get; set; }

    }

}


