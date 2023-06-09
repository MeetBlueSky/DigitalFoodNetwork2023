﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DFN2023.Entities.EF
{
    public class City
    {
        public City()
        {
            County = new HashSet<County>();
            //OfficialCompany = new HashSet<Company>();
            //MapCompany = new HashSet<Company>();
        }

        public int Id { get; set; }
        public int? CountryId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int LangId { get; set; }
        public int RowNum { get; set; }


        public virtual Country? Country { get; set; }


        public virtual ICollection<County>? County { get; set; }

        //[InverseProperty("OfficialCity")]
        //public virtual ICollection<Company>? OfficialCompany { get; set; }

        //[InverseProperty("MapCity")]
        //public virtual ICollection<Company>? MapCompany { get; set; }

    }

}


