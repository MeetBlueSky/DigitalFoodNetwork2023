using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.EF
{
    public class CompanyType
    {
        //public CompanyType()
        //{
        //    Company = new HashSet<Company>();
        //}
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string? Icon { get; set; }
        public int? RowNum { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int Status { get; set; }


        //public virtual ICollection<Company> Company { get; set; }

    }
}
