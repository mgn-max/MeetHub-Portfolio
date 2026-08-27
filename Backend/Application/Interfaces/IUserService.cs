using MeetHub.Application.DTOs;
using MeetHub.Application.DTOs.UserDetailsDto;
using MeetHub.Application.DTOs.UserDto;
using MeetHub.Domain.Entities;
using MeetHub.Domain.Enums.UserEnum;

namespace MeetHub.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailsDto> CreateUser(CreateUserDto request);
        Task<UserDto> GetUserById(Guid id);
        Task<UserDetailsDto> GetUserDetailsById(Guid id);
        Task<LoginResponseDto> Login(string loginOrEmail, string password);

        Task UpdateUserName(Guid id, string name);
        Task UpdateUserDisplayName(Guid id, string? displayName);
        Task UpdateUserLogin(Guid id, string login);
        Task UpdateUserPassword(Guid id, string password);
        Task UpdateUserEmail(Guid id, string email);
        Task UpdateUserStatus(Guid id, UserStatus userStatus);
        Task UpdateUserPhoto(Guid id, string? photoUrl);

    }
}
