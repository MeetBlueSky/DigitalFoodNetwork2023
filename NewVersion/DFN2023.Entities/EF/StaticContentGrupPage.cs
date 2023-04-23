﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int? OrderNo { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
        public string? Image5 { get; set; }
        public string? Video { get; set; } 
        public string? SeoKeywords { get; set; }
        public string? SeoDesc { get; set; }
        public string? Link { get; set; } 
        public string? FreeText1 { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Statu { get; set; }
        public int LangId { get; set; }

        [ForeignKey("GrupTempId")]
        public virtual StaticContentGrupTemp? StaticContentGrupTemp { get; set; }

        public virtual ICollection<StaticContentPage> StaticContentPage { get; set; }
    }
}
