using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories
{
    public class TopicRepository : ITopicRepository, IDisposable
    {
        private readonly DataContext _dataContext;

        public TopicRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Topic> CreateTopicAsync(Guid userId, Topic topic)
        {
            topic.UserId = userId;
            await _dataContext.AddAsync(topic);
            return topic;
        }

        public Task GetTopicAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetTopicsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateTopicAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteTopicAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
