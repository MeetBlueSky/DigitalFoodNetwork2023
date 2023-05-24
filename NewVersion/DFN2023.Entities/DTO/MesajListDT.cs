using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.DTO
{
    public class MesajListDT
    {
        public List<MessageDTO>  yenigelenmesaj { get; set; } = new List<MessageDTO>();
        public List<MessageDTO>  mesajlar { get; set; } = new List<MessageDTO>();
        public int okunmamiscount { get; set; }
        public int tumcount { get; set; }
    }
}
