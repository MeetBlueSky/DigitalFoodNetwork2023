using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.DTO
{
    public class MesajListDT
    {
        public List<MessageDTO>? gelenokunmamis { get; set; }
        public List<MessageDTO>? gonderdigimiz { get; set; }
    }
}
