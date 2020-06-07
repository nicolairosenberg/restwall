using System.Collections.Generic;

namespace RestLib.Infrastructure.Models.V1.Boards
{
    public class EnvelopeResponseBoardLinksDto
    {
        public List<ResponseBoardLinksDto> Value { get; set; }
        public IEnumerable<LinkDto> Links { get; set; }
    }
}
