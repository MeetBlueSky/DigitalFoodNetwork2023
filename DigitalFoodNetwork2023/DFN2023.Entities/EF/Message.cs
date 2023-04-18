using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.EF
{
    public class Message
    {
        public Message()
        {
            ChildMessage = new HashSet<Message>();
        }
        public int Id { get; set; }
        public int FromUser { get; set; }
        public int ToUser { get; set; }
        public int ParentId { get; set; }
        public string? MessageContent { get; set; }
        public string MessageType { get; set; }
        public DateTime CreateDate { get; set; }
        public int? LastIP { get; set; }
        public int Status { get; set; }


        public virtual Message ParentMessage { get; set; }
        public virtual User From { get; set; }
        public virtual User To { get; set; }



        public virtual ICollection<Message> ChildMessage { get; set; }

    }
}
