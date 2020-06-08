using Microsoft.EntityFrameworkCore;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RestTests.UnitTests
{
    public class TopicRepositoryTests
    {
        public static DbContextOptions<DataContext> DataContextOptions { get; set; } = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "UnitTestDb").Options;
        public TopicRepositoryTests()
        {
            // NR: I did checks for code from postman, due to no db, could not mock the repos properly.
            // Let me show and explain.
        }

        [Fact]
        public async Task CreateAndGetTopicFromContext()
        {
            // Arrange
            TopicRepository topicRepository = new TopicRepository(new DataContext(DataContextOptions));
            Guid id = Guid.Parse("35df166a-1dca-4697-91f8-afe6499c16f4");

            // Act
            var returnedTopic = await topicRepository.CreateTopicAsync(new Topic
            {
                Id = id
            });
            var topic = await topicRepository.GetTopicAsync(id);

            // Assert
            Assert.Equal(returnedTopic.Id, topic.Id);

        }
    }
}
