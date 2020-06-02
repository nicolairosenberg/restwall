using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestLib.Infrastructure.Entities
{
    public class Board
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Topic> Topics { get; set; } = new List<Topic>();
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; }
    }
}