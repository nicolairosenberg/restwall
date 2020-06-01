using System;
using System.Collections.Generic;

namespace RestLib.Infrastructure.Entities
{
    public class Message
    {
        public long Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public long BoardId { get; set; }
        public Board Board { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<Message> Replies { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
