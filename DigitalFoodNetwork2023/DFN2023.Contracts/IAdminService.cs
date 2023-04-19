using DFN2023.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Contracts
{
    public interface IAdminService
    {
        List<UserDTO> CheckUser(string uname, string pass);
    }
}
