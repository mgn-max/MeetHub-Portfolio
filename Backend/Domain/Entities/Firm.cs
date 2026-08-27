using MeetHub.Domain.Arguments;
using MeetHub.Domain.Exceptions;
using MeetHub.Domain.Validators;

namespace MeetHub.Domain.Entities
{
    public class Firm
    {
        public Guid Id { get; private set; }
        public string CorporateReason { get; private set; } = string.Empty;
        public string FantasyName { get; private set; } = string.Empty;
        public string? Email { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Cnpj { get; private set; }
        public string? LogoUrl { get; private set; }
        public bool IsActive { get; private set; }

        #region [address]
        public string? ZipCode { get; private set; }
        public string? Street { get; private set; }
        public string? AddressNumber { get; private set; }
        public string? Neighborhood { get; private set; }
        public string? City { get; private set; }
        public string? State { get; private set; }
        public string Country { get; private set; } = string.Empty;
        #endregion

        public DateTime CreatedAt { get; private set; }

        private Firm() { }

        public Firm(FirmCreationData data)
        {
            if (string.IsNullOrWhiteSpace(data.CorporateReason))
                throw new ArgumentException("O campo razão social não pode estar vazio");
            if (string.IsNullOrWhiteSpace(data.FantasyName))
                throw new ArgumentException("O campo nome fantasia não pode estar vazio");
            if (!string.IsNullOrWhiteSpace(data.Cnpj) && !CnpjValidator.IsValidCnpj(data.Cnpj))
                throw new BusinessRuleException("O campo CNPJ está inválido");
            if (string.IsNullOrWhiteSpace(data.Country))
                throw new ArgumentException("O campo País não pode estar vazio");
            if (!string.IsNullOrEmpty(data.Email) && !EmailValidator.IsValidEmail(data.Email))
                throw new ArgumentException("O email é inválido");
            if (!string.IsNullOrWhiteSpace(data.PhoneNumber) && !PhoneNumberValidator.IsValidPhoneNumber(data.PhoneNumber))
                throw new BusinessRuleException("O numero de telefone está inválido");

            #region [Max Length]
            ValidateLength(data.CorporateReason, 100);
            ValidateLength(data.FantasyName, 100);
            ValidateLength(data.Email, 200);
            ValidateLength(data.PhoneNumber, 15);
            ValidateLength(data.LogoUrl, 500);
            ValidateLength(data.ZipCode, 15);
            ValidateLength(data.Street, 255);
            ValidateLength(data.AddressNumber, 20);
            ValidateLength(data.Neighborhood, 100);
            ValidateLength(data.City, 100);
            ValidateLength(data.State, 50);
            ValidateLength(data.Country, 60);
            #endregion

            Id = Guid.NewGuid();
            CorporateReason = data.CorporateReason.Trim();
            FantasyName = data.FantasyName.Trim();
            Email = string.IsNullOrWhiteSpace(data.Email) ? null : data.Email.Trim();
            PhoneNumber = string.IsNullOrWhiteSpace(data.PhoneNumber) ? null : new string(data.PhoneNumber.Where(char.IsDigit).ToArray());
            Cnpj = string.IsNullOrWhiteSpace(data.Cnpj) ? null : new string(data.Cnpj.Where(char.IsDigit).ToArray());
            LogoUrl = string.IsNullOrWhiteSpace(data.LogoUrl) ? null : data.LogoUrl.Trim();
            IsActive = true;
            ZipCode = string.IsNullOrWhiteSpace(data.ZipCode) ? null : data.ZipCode.Trim();
            Street = string.IsNullOrWhiteSpace(data.Street) ? null : data.Street.Trim();
            AddressNumber = string.IsNullOrWhiteSpace(data.AddressNumber) ? null : data.AddressNumber.Trim();
            Neighborhood = string.IsNullOrWhiteSpace(data.Neighborhood) ? null : data.Neighborhood.Trim();
            City = string.IsNullOrWhiteSpace(data.City) ? null : data.City.Trim();
            State = string.IsNullOrWhiteSpace(data.State) ? null : data.State.Trim();
            Country = data.Country.Trim();
            CreatedAt = DateTime.UtcNow;
        }


        public void UpdateCorporateReason(string corporateReason)
        {
            if (string.IsNullOrWhiteSpace(corporateReason))
                throw new ArgumentException("O campo razão social não pode estar vazio");

            ValidateLength(corporateReason, 100);

            CorporateReason = corporateReason.Trim();
        }

        public void UpdateFantasyName(string fantasyName)
        {
            if (string.IsNullOrWhiteSpace(fantasyName))
                throw new ArgumentException("O campo nome fantasia não pode estar vazio");

            ValidateLength(fantasyName, 100);

            FantasyName = fantasyName.Trim();
        }

        public void UpdateEmail(string? email)
        {
            if (!string.IsNullOrWhiteSpace(email) && !EmailValidator.IsValidEmail(email))
                throw new ArgumentException("O email é inválido");

            ValidateLength(email, 200);

            Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        }

        public void UpdatePhoneNumber(string? phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber) && !PhoneNumberValidator.IsValidPhoneNumber(phoneNumber))
                throw new ArgumentException("O numero de telefone está inválido");

            ValidateLength(phoneNumber, 15);

            PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : new string(phoneNumber.Where(char.IsDigit).ToArray());
        }

        public void UpdateCNPJ(string? cnpj)
        {
            if (!string.IsNullOrWhiteSpace(cnpj) && !CnpjValidator.IsValidCnpj(cnpj))
                throw new BusinessRuleException("O campo CNPJ está inválido");
            Cnpj = string.IsNullOrWhiteSpace(cnpj) ? null : new string(cnpj.Where(char.IsDigit).ToArray());
        }

        public void UpdateLogoUrl(string? logo)
        {
            ValidateLength(logo, 500);

            LogoUrl = string.IsNullOrWhiteSpace(logo) ? null : logo.Trim();
        }

        public void UpdateStatus(bool status)
        {
            if (this.IsActive == status)
                throw new BusinessRuleException("Empresa já se encontra no status informado");
            IsActive = status;
        }


        public void UpdateAddress(AddressUpdateData data)
        {
            ValidateLength(data.ZipCode, 15);
            ValidateLength(data.Street, 255);
            ValidateLength(data.AddressNumber, 20);
            ValidateLength(data.Neighborhood, 100);
            ValidateLength(data.City, 100);
            ValidateLength(data.State, 50);
            ValidateLength(data.Country, 60);

            ZipCode = data.ZipCode?.Trim();
            Street = data.Street?.Trim();
            AddressNumber = data.AddressNumber?.Trim();
            Neighborhood = data.Neighborhood?.Trim();
            City = data.City?.Trim();
            State = data.State?.Trim();
            if (!string.IsNullOrWhiteSpace(data.Country))
                Country = data.Country.Trim();
        }
        private void ValidateLength(string? value, int max)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length > max)
                throw new BusinessRuleException("numero de caracteres excedido para esse campo");
        }
    }
}