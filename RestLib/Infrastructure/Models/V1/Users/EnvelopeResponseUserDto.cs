using System.Collections.Generic;

namespace RestLib.Infrastructure.Models.V1.Users
{
    public class EnvelopeResponseUserDto
    {
        public List<ResponseUserDto> Value { get; set; }
        public List<LinkDto> Links { get; set; }
    }
}
