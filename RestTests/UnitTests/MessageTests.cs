using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWallAPI.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RestTests.UnitTests
{
    public class MessageTests
    {
        MessagesController _systemUnderTest;
        ILogger<MessagesController> _myInterface;

        [Fact]
        public async Task SuccessfulGetRequest()
        {
            // ARRANGE
            //_systemUnderTest = new MessageController(_myInterface);

            //// ACT
            //var result = await _systemUnderTest.GetMessageAsync(Guid.NewGuid());
            //var okResult = result as OkObjectResult;

            //// assert
            //Assert.NotNull(okResult);
            //Assert.Equal(200, okResult.StatusCode);
        }
    }
}
