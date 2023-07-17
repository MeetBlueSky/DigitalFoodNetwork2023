using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.DTO
{
    public class LanguageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Status { get; set; }
        public string? Icon { get; set; }
        public Boolean DefaultLang { get; set; }
        public DateTime? Date { get; set; }
    }
}
