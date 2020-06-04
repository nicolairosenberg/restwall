using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Repositories;
using RestLib.Infrastructure.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RestTests.UnitTests
{
    public class TopicRepositoryTests
    {
        //public static DbContextOptions<IdNetworkContext> NetworkOptions { get; set; } = new DbContextOptionsBuilder<IdNetworkContext>().UseSqlServer("").Options;
        public static DbContextOptions<DataContext> DataContextOptions { get; set; } = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "UnitTestDb").Options;
        //options => options.UseInMemoryDatabase(databaseName: "MessageBoard")
        public TopicRepositoryTests()
        {

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
