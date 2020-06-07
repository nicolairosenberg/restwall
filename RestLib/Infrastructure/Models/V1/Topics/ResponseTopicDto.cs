using System;
using System.Collections.Generic;

namespace RestLib.Infrastructure.Models.V1.Topics
{
    public class ResponseTopicDto
    {
        public Guid Id { get; set; }
        public Guid BoardId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastActivityOn { get; set; }
        public IEnumerable<LinkDto> Links { get; set; }
    }
}
