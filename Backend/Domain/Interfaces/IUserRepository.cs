using MeetHub.Domain.Entities;

namespace MeetHub.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User?> GetById(Guid id);
        Task Update(User user);

        Task<User?> GetByLogin(string login);
        Task<User?> GetByEmail(string email);
        Task<User?> GetByLoginOrEmail(string value);

        Task<bool> ExistsByLogin(string login);
        Task<bool> ExistsByEmail(string email);
    }
}
