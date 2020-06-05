using System;

namespace RestLib.Infrastructure.Models.V1
{
    public class RequestMessageDto
    {
        public Guid TopicId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
    }
}
