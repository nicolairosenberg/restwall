﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestLib.Infrastructure.Entities
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("TopicId")]
        public Topic Topic { get; set; }
        public Guid TopicId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
