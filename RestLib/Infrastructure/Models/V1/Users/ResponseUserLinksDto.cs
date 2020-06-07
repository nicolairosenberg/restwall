using System;
using System.Collections.Generic;

namespace RestLib.Infrastructure.Models.V1.Users
{
    public class ResponseUserLinksDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public DateTime JoinedOn { get; set; }
        public IEnumerable<LinkDto> Links { get; set; }
    }
}
