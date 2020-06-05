using System;
using System.Collections.Generic;

namespace RestLib.Infrastructure.Models.V1
{
    public class RequestTopicDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public ICollection<RequestMessageDto> Messages { get; set; } = new List<RequestMessageDto>();
    }
}
