using System.Collections.Generic;

namespace RestLib.Infrastructure.Models.V1.Users
{
    public class EnvelopeResponseUserLinksDto
    {
        public List<ResponseUserLinksDto> Value { get; set; }
        public IEnumerable<LinkDto> Links { get; set; }
    }
}
