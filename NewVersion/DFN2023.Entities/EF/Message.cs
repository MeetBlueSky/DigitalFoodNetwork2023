using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.EF
{
    public class Message
    {


        public int Id { get; set; }

        public int FromUser { get; set; } //gönderen

        public int ToUser { get; set; }  //gönderilen

        public string? MessageContent { get; set; }
        public DateTime CreateDate { get; set; }
        public int? LastIP { get; set; }
        public int Status { get; set; }
        public bool? IsShow { get; set; }

        public virtual User User { get; set; }



    }
}
