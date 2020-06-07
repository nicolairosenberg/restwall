using System;

namespace RestLib.Infrastructure.Models.V1.Boards
{
    public class ResponseBoardDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime LastServerResetOn { get; set; }
        public DateTime LastActivityOn { get; set; }
    }
}
