using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Entities.EF
{
    public class StaticContentTemp
    {
        public StaticContentTemp()
        {
            StaticContentPage = new HashSet<StaticContentPage>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Desc { get; set; }
        public int? RowNum { get; set; }
        public virtual ICollection<StaticContentPage> StaticContentPage { get; set; }
    }
}
