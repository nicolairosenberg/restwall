using Microsoft.EntityFrameworkCore;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Parameters;
using RestLib.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Topic> CreateTopicAsync(Topic topic)
        {
            await _dataContext.AddAsync(topic);
            await _dataContext.SaveChangesAsync();
            return topic;
        }

        public async Task<Topic> GetTopicAsync(Guid topicId)
        {
            return await _dataContext.Topics.Where(x => x.Id == topicId).SingleOrDefaultAsync();
        }

        //public async Task<IEnumerable<Topic>> GetTopicsAsync(Guid boardId)
        //{
        //    return await _dataContext.Topics.Where(x => x.BoardId == boardId).ToListAsync();
        //}

        public async Task<IEnumerable<Topic>> GetTopicsAsync(Guid boardId, TopicsParams topicsParams)
        {
            var collection = _dataContext.Topics as IQueryable<Topic>;

            //return await _dataContext.Topics.Where(x => x.BoardId == boardId).ToListAsync();

            return await collection
                .Skip(topicsParams.PageSize * (topicsParams.PageNumber - 1))
                .Take(topicsParams.PageSize)
                .ToListAsync();
        }

        public async Task<Topic> UpdateTopicAsync(Topic topic)
        {
            topic.Title = topic.Title;
            topic.Text = topic.Text;
            topic.UpdatedOn = DateTime.Now;
            _dataContext.Update(topic);
            await _dataContext.SaveChangesAsync();
            return topic;
        }

        public async Task<Topic> DeleteTopicAsync(Topic topic)
        {
            _dataContext.Remove(topic);
            await _dataContext.SaveChangesAsync();

            return topic;
        }

        public async Task<bool> ExistsAsync(Guid topicId)
        {
            return await _dataContext.Topics.Where(x => x.Id == topicId).AnyAsync();
        }

        public void Dispose()
        {
        }
    }
}
