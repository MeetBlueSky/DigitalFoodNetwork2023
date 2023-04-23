using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Entities.EF
{
    public class StaticContentGrupTemp
    {
        public StaticContentGrupTemp()
        {
            StaticContentGrupPage = new HashSet<StaticContentGrupPage>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Desc { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<StaticContentGrupPage> StaticContentGrupPage { get; set; }
    }
}
