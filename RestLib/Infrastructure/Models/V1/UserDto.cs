﻿using System;

namespace RestLib.Infrastructure.Models.V1
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public DateTime JoinedOn { get; set; }
    }
}
