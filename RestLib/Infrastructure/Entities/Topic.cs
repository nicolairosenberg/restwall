using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestLib.Infrastructure.Entities
{
    public class Topic
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("BoardId")]
        public Board Board { get; set; }
        public Guid BoardId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
