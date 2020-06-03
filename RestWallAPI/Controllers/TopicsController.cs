using Microsoft.AspNetCore.Mvc;

namespace RestWallAPI.Controllers
{
    [Route("boards/{boardId}/topics")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        public TopicsController()
        {
            
        }
    }
}