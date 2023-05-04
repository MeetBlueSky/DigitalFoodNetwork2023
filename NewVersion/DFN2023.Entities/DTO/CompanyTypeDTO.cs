using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.DTO
{
    public class CompanyTypeDTO
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string? Icon { get; set; }
        public int? RowNum { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int Status { get; set; }



        public int UserId { get; set; }
    }
}
