using System;

namespace RestWallSite.Models
{
    public class CreateTopicModel
    {
        public Guid BoardId { get; set; }
        public Guid TopicId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
    }
}
