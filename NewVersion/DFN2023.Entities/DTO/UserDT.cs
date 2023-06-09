﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Entities.DTO
{
    public class UserDT
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
    }
}
