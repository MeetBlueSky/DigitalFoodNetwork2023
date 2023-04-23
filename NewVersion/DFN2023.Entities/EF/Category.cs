using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.EF
{
    public class Category
    {
        public Category()
        {
            ChildCategory = new HashSet<Category>();
            CategoryProductBase = new HashSet<CategoryProductBase>();
            ProductCompany = new HashSet<ProductCompany>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Image { get; set; }
        public int? ParentId { get; set; }
        public int RowNum { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string? LastIP { get; set; }
        public int Status { get; set; }
        public int LangId { get; set; }


        public virtual Category ParentCategory { get; set; }


        public virtual ICollection<Category> ChildCategory { get; set; }
        public virtual ICollection<CategoryProductBase> CategoryProductBase { get; set; }
        public virtual ICollection<ProductCompany> ProductCompany { get; set; }



    }
}
