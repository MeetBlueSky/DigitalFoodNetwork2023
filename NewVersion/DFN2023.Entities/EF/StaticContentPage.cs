﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DFN2023.Entities.EF
{
    public class StaticContentPage
    {
        public int Id { get; set; }
        public int GrupId { get; set; }
        public int TempId { get; set; }
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Html { get; set; }
        public int OrderNo { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
        public string? Image5 { get; set; }
        public string? Attachment1 { get; set; }
        public string? Attachment2 { get; set; }
        public string? Attachment3 { get; set; }
        public string? Video { get; set; } 
        public string? SeoKeywords { get; set; }
        public string? SeoDesc{ get; set; }
        public string? Link { get; set; } 
        public int LangId { get; set; }
        public int Statu { get; set; } 
        public DateTime Date { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? FreeText1 { get; set; }

        [ForeignKey("GrupId")]
        public virtual StaticContentGrupPage? StaticContentGrupPage { get; set; }

        [ForeignKey("TempId")]
        public virtual StaticContentTemp? StaticContentTemp { get; set; }
    }
}
