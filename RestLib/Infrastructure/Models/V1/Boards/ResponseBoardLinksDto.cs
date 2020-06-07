using System;
using System.Collections.Generic;

namespace RestLib.Infrastructure.Models.V1.Boards
{
    public class ResponseBoardLinksDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime LastServerResetOn { get; set; }
        public DateTime LastActivityOn { get; set; }
        public IEnumerable<LinkDto> Links { get; set; }
    }
}
