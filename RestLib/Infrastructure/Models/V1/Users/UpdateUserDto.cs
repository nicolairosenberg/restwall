﻿using System;

namespace RestLib.Infrastructure.Models.V1.Users
{
    public class UpdateUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
    }
}
