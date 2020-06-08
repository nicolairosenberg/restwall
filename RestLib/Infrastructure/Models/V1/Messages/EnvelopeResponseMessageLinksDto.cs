using System.Collections.Generic;

namespace RestLib.Infrastructure.Models.V1.Messages
{
    public class EnvelopeResponseMessageLinksDto
    {
        public List<ResponseMessageLinksDto> Value { get; set; }
        public IEnumerable<LinkDto> Links { get; set; }
    }
}
