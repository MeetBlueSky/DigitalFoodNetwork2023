using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Entities.EF
{
    public class UserUrunler
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public DateTime CreatedDate { get; set; }


        public virtual User User { get; set; }
        public virtual Company Company { get; set; }
    }
}
