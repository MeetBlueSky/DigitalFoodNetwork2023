using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Entities.EF
{
    public class StaticContentGrupPage
    {
        public StaticContentGrupPage()
        {
            StaticContentPage = new HashSet<StaticContentPage>();
        }
        public int Id { get; set; }
        public int GrupTempId { get; set; }
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Html { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
        public string? Image5 { get; set; }
        public string? Video { get; set; }
        public string? SeoKeywords { get; set; }
        public string? SeoDesc { get; set; }
        public string? Keywords { get; set; }
        public string? Link { get; set; }
        public string? Slug { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public int LangId { get; set; }


        public virtual StaticContentGrupTemp StaticContentGrupTemp { get; set; }
        public virtual ICollection<StaticContentPage> StaticContentPage { get; set; }
    }
}
