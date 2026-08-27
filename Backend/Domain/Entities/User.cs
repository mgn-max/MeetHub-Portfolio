using MeetHub.Domain.Enums.UserEnum;
using MeetHub.Domain.Exceptions;
using MeetHub.Domain.Validators;

namespace MeetHub.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? DisplayName { get; private set; }
        public string Login { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public UserStatus UserStatus { get; private set; }
        public string? PhotoUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private User() { }
        public User(string name, string? displayName, string login, string email, string passwordHash, string? photoUrl)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do usuário não pode ser vazio");
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("O login do usuário não pode ser vazio");
            if (string.IsNullOrWhiteSpace(email) || !EmailValidator.IsValidEmail(email))
                throw new ArgumentException("O email do usuário é inválido");
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("A senha do usuário é inválida");

            Id = Guid.NewGuid();
            Name = name.Trim();
            DisplayName = string.IsNullOrWhiteSpace(displayName) ? null : displayName.Trim();
            Login = login.Trim().ToLower();
            Email = email.Trim().ToLower();
            PasswordHash = passwordHash;
            UserStatus = UserStatus.Active;
            PhotoUrl = string.IsNullOrWhiteSpace(photoUrl) ? null : photoUrl.Trim();
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do usuário não pode ser vazio");
            Name = name.Trim();
        }

        public void UpdateDisplayName(string? displayName)
        {
            DisplayName = string.IsNullOrWhiteSpace(displayName) ? null : displayName.Trim();
        }

        public void UpdateLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("O login do usuário não pode ser vazio");
            Login = login.Trim().ToLower();
        }

        public void UpdatePassword(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("A senha do usuário é inválida");
            PasswordHash = passwordHash;
        }

        public void UpdateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !EmailValidator.IsValidEmail(email))
                throw new ArgumentException("O email do usuário é inválido");
            Email = email.Trim().ToLower();
        }

        public void Activate()
        {
            ValidateUserStatus(UserStatus.Active);

            UserStatus = UserStatus.Active;
        }

        public void Inactivate()
        {
            ValidateUserStatus(UserStatus.Inactive);

            UserStatus = UserStatus.Inactive;
        }

        public void Suspend()
        {
            ValidateUserStatus(UserStatus.Suspended);

            UserStatus = UserStatus.Suspended;
        }

        public void UpdatePhotoUrl(string? photoUrl)
        {
            PhotoUrl = string.IsNullOrWhiteSpace(photoUrl) ? null : photoUrl.Trim();
        }

        private void ValidateUserStatus(UserStatus status)
        {
            if (UserStatus == status)
                throw new BusinessRuleException("O status do usuário já está definido para o valor especificado");
        }

    }
}
