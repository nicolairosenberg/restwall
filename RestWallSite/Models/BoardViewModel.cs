using RestLib.Infrastructure.Models.V1.Boards;
using RestLib.Infrastructure.Models.V1.Messages;
using RestLib.Infrastructure.Models.V1.Topics;
using System.Collections.Generic;

namespace RestWallSite.Models
{
    public class BoardViewModel
    {
        public ICollection<ResponseBoardDto> Boards { get; set; }
        public ICollection<ResponseTopicDto> Topics { get; set; }
        public ICollection<ResponseMessageDto> Messages { get; set; }
    }
}
