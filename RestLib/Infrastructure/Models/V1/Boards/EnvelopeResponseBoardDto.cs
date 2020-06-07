using System.Collections.Generic;

namespace RestLib.Infrastructure.Models.V1.Boards
{
    public class EnvelopeResponseBoardDto
    {
        public List<ResponseBoardDto> Value { get; set; }
        public List<LinkDto> Links { get; set; }
    }
}
