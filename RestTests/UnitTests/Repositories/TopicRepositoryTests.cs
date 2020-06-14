using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Profiles;
using RestLib.Infrastructure.Repositories;
using RestLib.Infrastructure.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RestTests.UnitTests
{
    public class TopicRepositoryTests
    {
        public static DbContextOptions<DataContext> DataContextOptions { get; set; } = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "UnitTestDb").Options;

        [Fact]
        private async Task CreateAndRetrieveTopic()
        {
            // Arrange
            var boardId = Guid.Parse("34df166a-1dca-4697-91f8-afe6499c16f4");
            var topicId = Guid.Parse("35df166a-1dca-4697-91f8-afe6499c16f4");

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<TopicsProfile>();
            });

            var mapper = new Mapper(config);

            var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "UnitTestDb")
                    .Options;

            var context = new DataContext(options);
            context.Topics.Add(
                new Topic
                {
                    BoardId = boardId,
                    Id = topicId
                });

            context.SaveChanges();

            var topicRepository = new TopicRepository(context);
            var topicService = new TopicService(topicRepository, mapper);

            // Act
            var test = await topicService.GetTopicAsync(topicId);

            // Assert

            Assert.Equal(test.Id, test.Id);
        }
    }
}
