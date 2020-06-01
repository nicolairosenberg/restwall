﻿using System;
using System.Collections.Generic;

namespace RestLib.Infrastructure.Entities
{
    public class User
    {
        public long Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Auth0 { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public int MessageCount { get; set; }
        public List<Message> Messages { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; }
        public DateTime DeletedOn { get; set; }
    }
}