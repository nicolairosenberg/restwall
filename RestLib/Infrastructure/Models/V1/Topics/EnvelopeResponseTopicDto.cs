using System.Collections.Generic;

namespace RestLib.Infrastructure.Models.V1.Topics
{
    public class EnvelopeResponseTopicDto
    {
        public List<ResponseTopicDto> Value { get; set; }
        public List<LinkDto> Links { get; set; }
    }
}
