using MeetHub.API.Models.RequestsUser;
using MeetHub.Application.DTOs.UserDto;

namespace MeetHub.API.Mapper.UsersMapper
{
    public static class UserRequestMapper
    {
        public static CreateUserDto ToDto(this CreateUserRequest request)
        {
            return new CreateUserDto
            {
                Name = request.Name,
                DisplayName = request.DisplayName,
                Login = request.Login,
                Password = request.Password,
                Email = request.Email,
                PhotoUrl = request.PhotoUrl
            };
        }
    }
}
