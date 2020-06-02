using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task CreateMessageAsync();
        Task GetMessageAsync();
        Task GetMessagesAsync();
        Task UpdateMessageAsync();
        Task DeleteMessageAsync();
    }
}
