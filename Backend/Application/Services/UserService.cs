using MeetHub.Application.DTOs;
using MeetHub.Application.DTOs.UserDetailsDto;
using MeetHub.Application.DTOs.UserDto;
using MeetHub.Application.Exceptions;
using MeetHub.Application.Interfaces;
using MeetHub.Domain.Entities;
using MeetHub.Domain.Enums.UserEnum;
using MeetHub.Domain.Exceptions;
using MeetHub.Domain.Interfaces;

namespace MeetHub.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<UserDetailsDto> CreateUser(CreateUserDto request)
        {
            var normalizedLogin = request.Login.Trim().ToLower();
            var normalizedEmail = request.Email.Trim().ToLower();

            LengthValidation(request);
            PasswordValidation(request.Password);

            if (await _userRepository.ExistsByLogin(normalizedLogin))
                throw new BusinessRuleException("Login já cadastrado no sistema");
            if (await _userRepository.ExistsByEmail(normalizedEmail))
                throw new BusinessRuleException("Email já cadastrado no sistema");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User user = new User(request.Name, request.DisplayName, normalizedLogin, normalizedEmail, passwordHash, request.PhotoUrl);
            await _userRepository.Add(user);

            return new UserDetailsDto
            {
                Id = user.Id,
                Name = user.Name,
                DisplayName = user.DisplayName,
                Login = user.Login,
                Email = user.Email,
                PhotoUrl = user.PhotoUrl,
                CreatedAt = user.CreatedAt,
                UserStatus = user.UserStatus
            };
        }

        private async Task<User> GetSupportUserById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new NotFoundException("Usuario não encontrado");
            return user;
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            var user = await GetSupportUserById(id);
            return new UserDto { Id = user.Id, Name = user.Name };
        }

        public async Task<UserDetailsDto> GetUserDetailsById(Guid id)
        {
            var user = await GetSupportUserById(id);
            return new UserDetailsDto
            {
                Id = user.Id,
                Name = user.Name,
                DisplayName = user.DisplayName,
                Login = user.Login,
                Email = user.Email,
                PhotoUrl = user.PhotoUrl,
                CreatedAt = user.CreatedAt,
                UserStatus = user.UserStatus
            };
        }

        public async Task<LoginResponseDto> Login(string loginOrEmail, string password)
        {
            var normalized = loginOrEmail.Trim().ToLower();
            var user = await _userRepository.GetByLoginOrEmail(normalized);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) || user.UserStatus != UserStatus.Active)
                throw new BusinessRuleException("Usuário ou senha inválidos");

            var token = _tokenService.GenerateToken(
                user.Id,
                user.Name,
                user.Email
                );

            return new LoginResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                Token = token
            };
        }

        public async Task UpdateUserDisplayName(Guid id, string? displayName)
        {
            var normalizedDisplayName = string.IsNullOrWhiteSpace(displayName) ? null : displayName.Trim();
            if (normalizedDisplayName?.Length > 100)
                throw new BusinessRuleException("Nome de mostragem muito longo");

            var user = await GetSupportUserById(id);
            user.UpdateDisplayName(normalizedDisplayName);
            await _userRepository.Update(user);
        }

        public async Task UpdateUserEmail(Guid id, string email)
        {
            var normalizedEmail = email.Trim().ToLower();
            if (normalizedEmail.Length > 200)
                throw new BusinessRuleException("Email muito longo");

            var user = await GetSupportUserById(id);

            if (user.Email != normalizedEmail)
                if (await _userRepository.ExistsByEmail(normalizedEmail))
                    throw new BusinessRuleException("Email já cadastrado no sistema");

            user.UpdateEmail(normalizedEmail);
            await _userRepository.Update(user);
        }

        public async Task UpdateUserLogin(Guid id, string login)
        {
            var normalizedLogin = login.Trim().ToLower();
            if (normalizedLogin.Length > 50)
                throw new BusinessRuleException("Login muito longo");

            var user = await GetSupportUserById(id);

            if (user.Login != normalizedLogin)
                if (await _userRepository.ExistsByLogin(normalizedLogin))
                    throw new BusinessRuleException("Não é possivel alterar o login para o informado pois esse login já existe no sistema");

            user.UpdateLogin(normalizedLogin);
            await _userRepository.Update(user);
        }

        public async Task UpdateUserName(Guid id, string name)
        {
            var normalizedUserName = name.Trim();
            if (normalizedUserName.Length > 100)
                throw new BusinessRuleException("Nome muito longo");

            var user = await GetSupportUserById(id);
            user.UpdateName(normalizedUserName);
            await _userRepository.Update(user);
        }

        public async Task UpdateUserPassword(Guid id, string password)
        {
            if (password.Length > 200)
                throw new BusinessRuleException("Senha muito longa");

            PasswordValidation(password);

            var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = await GetSupportUserById(id);

            if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new BusinessRuleException("Nova senha não pode ser igual a senha atual");

            user.UpdatePassword(newPasswordHash);
            await _userRepository.Update(user);
        }

        public async Task UpdateUserPhoto(Guid id, string? photoUrl)
        {
            var normalizedPhotoUrl = string.IsNullOrWhiteSpace(photoUrl) ? null : photoUrl.Trim();
            if (normalizedPhotoUrl?.Length > 500)
                throw new BusinessRuleException("Imagem invalida por favor entre em contato com o suporte ou tente uma outra imagem");

            var user = await GetSupportUserById(id);
            user.UpdatePhotoUrl(normalizedPhotoUrl);
            await _userRepository.Update(user);
        }
        public async Task UpdateUserStatus(Guid id, UserStatus userStatus)
        {
            var user = await GetSupportUserById(id);

            switch (userStatus)
            {
                case UserStatus.Active:
                    user.Activate();
                    break;

                case UserStatus.Inactive:
                    user.Inactivate();
                    break;

                case UserStatus.Suspended:
                    user.Suspend();
                    break;

                default:
                    throw new BusinessRuleException("Status de usuario invalido");
            }

            await _userRepository.Update(user);
        }


        private static void PasswordValidation(string password)
        {
            string message = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula e um número.";

            if (password.Length < 8)
            {
                throw new BusinessRuleException("A senha deve ter no minimo 8 caracteres");
            }

            bool temRegras = password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit);

            if (!temRegras)
            {
                throw new BusinessRuleException(message);
            }
        }

        private static void LengthValidation(CreateUserDto request)
        {
            var normalizedLogin = request.Login.Trim();
            var normalizedEmail = request.Email.Trim();
            var normalizedName = request.Name.Trim();
            var normalizedPassword = request.Password.Trim();
            var normalizedDisplayName = string.IsNullOrWhiteSpace(request.DisplayName) ? null : request.DisplayName.Trim();
            var normalizedPhotoUrl = string.IsNullOrWhiteSpace(request.PhotoUrl) ? null : request.PhotoUrl.Trim();

            if (normalizedDisplayName?.Length > 100)
                throw new BusinessRuleException("Nome de mostragem muito longo");
            if (normalizedName.Length > 100)
                throw new BusinessRuleException("Nome muito longo");
            if (normalizedLogin.Length > 50)
                throw new BusinessRuleException("Login muito longo");
            if (normalizedPassword.Length > 200)
                throw new BusinessRuleException("Senha muito longa");
            if (normalizedEmail.Length > 200)
                throw new BusinessRuleException("Email muito longo");
            if (normalizedPhotoUrl?.Length > 500)
                throw new BusinessRuleException("Imagem invalida por favor entre em contato com o suporte ou tente uma outra imagem");
        }
    }
}
