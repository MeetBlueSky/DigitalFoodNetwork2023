using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int FromUser { get; set; }

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
