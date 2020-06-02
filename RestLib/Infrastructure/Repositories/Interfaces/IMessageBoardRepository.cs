using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories.Interfaces
{
    public interface IMessageBoardRepository
    {
        Task CreateBoardAsync();
        Task GetBoardAsync();
        Task GetBoardsAsync();
        Task UpdateBoardAsync();
        Task DeleteBoardAsync();


        Task CreateTopicAsync();
        Task GetTopicAsync();
        Task GetTopicsAsync();
        Task UpdateTopicAsync();
        Task DeleteTopicAsync();

        Task CreateMessageAsync();
        Task GetMessageAsync();
        Task GetMessagesAsync();
        Task UpdateMessageAsync();
        Task DeleteMessageAsync();

        Task CreateUserAsync();
        Task GetUserAsync();
        Task GetUsersAsync();
        Task UpdateUserAsync();
        Task DeleteUserAsync();
    }
}
