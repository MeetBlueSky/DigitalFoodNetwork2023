
using DFN2023.Entities.DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFN2023.Admin.Models
{
    public class CategoryPageModel
    {
        public int lang { get; set; }
        public string language { get; set; }
        public List<CategoryDTO> categoryList { get; set; }
    }
}
