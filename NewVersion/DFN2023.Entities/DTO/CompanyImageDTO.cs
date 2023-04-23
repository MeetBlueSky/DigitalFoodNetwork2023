using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.DTO
{
    public class CompanyImageDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Path { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int Status { get; set; }
        public int RowNum { get; set; }
        public string? Desc { get; set; }


    }
}
