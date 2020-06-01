using System;

namespace RestLib.Infrastructure.Entities
{
    public class Message
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
