using System.Collections.Generic;

namespace RestLib.Infrastructure.Models.V1.Messages
{
    public class EnvelopeResponseMessageDto
    {
        public List<ResponseMessageDto> Value { get; set; }
        public List<LinkDto> Links { get; set; }
    }
}
