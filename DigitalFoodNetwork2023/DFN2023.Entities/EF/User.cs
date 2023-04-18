using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Entities.EF
{
    public class User
    {
        public User()
        {
            UserUrunler = new HashSet<UserUrunler>();
            Message = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Email { get; set; }
        public string? CitizenID { get; set; }
        public string Phone { get; set; }
        public int Role { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string? LastIP { get; set; }
        public int? EmailConfirmed { get; set; }
        public DateTime? EmailConfirmDate { get; set; }
        public int? GDPRConfirmed { get; set; }
        public int? GDPRConfirmDate { get; set; }
        public int Status { get; set; }



        public virtual ICollection<UserUrunler> UserUrunler { get; set; }
        public virtual ICollection<Message> Message { get; set; }

    }
}
