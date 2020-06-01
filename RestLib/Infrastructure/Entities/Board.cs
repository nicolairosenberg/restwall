using System;
using System.Collections.Generic;

namespace RestLib.Infrastructure.Entities
{
    public class Board
    {
        public long Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Message> Topics { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; }
    }
}