﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.DTO
{
    public class SignUpDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string phoneNumber { get; set; }
        public string Name { get; set; }

    }
}
