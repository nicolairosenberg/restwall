using Microsoft.AspNetCore.Mvc;
using Moq;
using RestLib.Infrastructure.Models.V1.Boards;
using RestLib.Infrastructure.Services.Interfaces;
using RestWallAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RestTests.UnitTests.Controllers.Api
{
    public class BoardsControllerTests
    {
        private Guid TestBoardGuid { get; set; } = Guid.Parse("5A7CA007-706B-44FE-8061-E52A920702E3");
        private Guid TestSecondBoardGuid { get; set; } = Guid.Parse("7BA32305-5A9F-4DDE-B2EB-5B9532EDEC53");

        [Fact]
        public async Task GetAllBoards()
        {
            // ARRANGE
            var mockRepo = new Mock<IBoardService>();
            mockRepo.Setup(repo => repo.GetBoardsAsync()).Returns(Task.FromResult(GetTestBoards()));
            var controller = new BoardsControllers(mockRepo.Object);

            // ACT
            var actionResult = await controller.GetBoardsAsync();
            var okResult = actionResult as OkObjectResult;

            // assert
            Assert.NotNull(actionResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        private ICollection<ResponseBoardDto> GetTestBoards()
        {
            var boards = new List<ResponseBoardDto>();
            boards.Add(new ResponseBoardDto()
            {
                Id = TestBoardGuid,
                Title = "Test1",
                Description = "Description1",
                LastServerResetOn = DateTime.Now,
                LastActivityOn = DateTime.Now
            });
            boards.Add(new ResponseBoardDto()
            {
                Id = TestSecondBoardGuid,
                Title = "Test2",
                Description = "Description2",
                LastServerResetOn = DateTime.Now,
                LastActivityOn = DateTime.Now
            });
            return boards;
        }
    }
}
