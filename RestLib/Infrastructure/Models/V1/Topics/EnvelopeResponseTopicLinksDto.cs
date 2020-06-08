using System.Collections.Generic;

namespace RestLib.Infrastructure.Models.V1.Topics
{
    public class EnvelopeResponseTopicLinksDto
    {
        public List<ResponseTopicLinksDto> Value { get; set; }
        public IEnumerable<LinkDto> Links { get; set; }
    }
}
