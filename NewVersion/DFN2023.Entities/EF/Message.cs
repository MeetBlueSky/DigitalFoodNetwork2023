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
        //public Message()
        //{
        //    ChildMessage = new HashSet<Message>();
        //}
        public int Id { get; set; }


        //[ForeignKey("From")]
        public int FromUser { get; set; }
        //public virtual User From { get; set; }


        //[ForeignKey("To")]
        public int ToUser { get; set; }
        //public virtual User To { get; set; }
        public int? FromRolId { get; set; }


        public string? MessageContent { get; set; }
        public DateTime CreateDate { get; set; }
        public int? LastIP { get; set; }
        public int Status { get; set; }
        public bool? UserShow { get; set; }
        public bool? CompanyShow { get; set; }



    }
}
