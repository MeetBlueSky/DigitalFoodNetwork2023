using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.EF
{
    public class CategoryProductBase
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ProductBaseId { get; set; }
        public int RowNum { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string? LastIP { get; set; }
        public int Status { get; set; }


        public virtual Category Category { get; set; }
        public virtual ProductBase ProductBase { get; set; }
    }
}
