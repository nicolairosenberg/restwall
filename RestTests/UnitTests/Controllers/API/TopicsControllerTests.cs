using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestLib.Infrastructure.Models.V1.Topics;
using RestLib.Infrastructure.Profiles;
using RestLib.Infrastructure.Services.Interfaces;
using RestWallAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RestTests.UnitTests.Controllers.Api
{
    public class TopicsControllerTests
    {
        private Guid TestBoardGuid { get; set; } = Guid.Parse("5A7CA007-706B-44FE-8061-E52A920702E3");
        private Guid TestTopicGuid { get; set; } = Guid.Parse("7BA32305-5A9F-4DDE-B2EB-5B9532EDEC53");
        private Guid TestUserGuid { get; set; } = Guid.Parse("1A7CA007-706B-44FE-8061-E52A920702E3");
        private Guid TestSecondUserGuid { get; set; } = Guid.Parse("2A7CA007-706B-44FE-8061-E52A920702E3");

        [Fact]
        public async Task GetAllTopicsForBoard()
        {
            // ARRANGE
            var mockRepo = new Mock<ITopicService>();
            mockRepo.Setup(repo => repo.GetTopicsAsync(TestBoardGuid)).Returns(Task.FromResult(GetTestTopics()));
            var controller = new TopicsController(mockRepo.Object);

            // ACT
            ActionResult<IEnumerable<ResponseTopicDto>> actionResult = await controller.GetTopicsAsync(TestBoardGuid);
            var result = actionResult.Result as OkObjectResult;

            // assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task CreateTopicOnBoard()
        {
            // ARRANGE
            var requestTopic = new RequestTopicDto()
            {
                Title = "New Title",
                Text = "This is the text",
                UserId = TestUserGuid
            };

            var responseTopic = new ResponseTopicDto()
            {
                Id = TestTopicGuid,
                Title = requestTopic.Title,
                Text = requestTopic.Text
            };

            var mockRepo = new Mock<ITopicService>();
            mockRepo.Setup(repo => repo.CreateTopicAsync(TestBoardGuid, requestTopic)).Returns(Task.FromResult(responseTopic));
            var controller = new TopicsController(mockRepo.Object);

            // ACT
            ActionResult<ResponseTopicDto> actionResult = await controller.CreateTopicAsync(TestBoardGuid, requestTopic);
            var result = actionResult.Result as CreatedAtRouteResult;

            // assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal("GetTopicAsync", result.RouteName);
        }
        
        private ICollection<ResponseTopicDto> GetTestTopics()
        {
            var topics = new List<ResponseTopicDto>();
            topics.Add(new ResponseTopicDto()
            {
                Id = TestBoardGuid,
                Title = "Test1",
                Text = "Description1",
                BoardId = TestBoardGuid,
                LastActivityOn = DateTime.Now,
                CreatedOn = DateTime.Now,
                UserId = TestUserGuid
            });
            return topics;
        }
    }
}
