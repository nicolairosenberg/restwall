using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestLib.Infrastructure.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Auth0 { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; }
        public DateTime DeletedOn { get; set; }
    }
}