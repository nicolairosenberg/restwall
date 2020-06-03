using System;

namespace RestLib.Infrastructure.Models.V1
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
