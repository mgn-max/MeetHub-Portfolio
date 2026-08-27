using MeetHub.Domain.Enums.FirmMembershipEnum;
using MeetHub.Domain.Exceptions;

namespace MeetHub.Domain.Entities
{
    public class FirmMembership
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid FirmId { get; private set; }

        public MembershipProfile Profile { get; private set; }
        public MembershipStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private FirmMembership() { }

        public FirmMembership(Guid userId, Guid firmId, MembershipProfile profile = MembershipProfile.Basic)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("O id do usuário é inválido");
            if (firmId == Guid.Empty)
                throw new ArgumentException("O id da empresa é inválido");

            Id = Guid.NewGuid();
            UserId = userId;
            FirmId = firmId;
            Profile = profile;
            Status = MembershipStatus.Active;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateMembershipStatus(MembershipStatus status, MembershipProfile requestingProfile)
        {
            if (requestingProfile != MembershipProfile.Admin && requestingProfile != MembershipProfile.SuperAdmin)
                throw new BusinessRuleException("Apenas administradores podem alterar o status do usuário");
            if(Profile == MembershipProfile.SuperAdmin && requestingProfile != MembershipProfile.SuperAdmin)
                throw new BusinessRuleException("Apenas um super administrador pode alterar o status de outro super administrador");
            if (Status == status)
                throw new BusinessRuleException("O usuário já está com o status informado");

            Status = status;
        }

        public void UpdateMembershipProfile(MembershipProfile profile, MembershipProfile requestingProfile)
        {
            if (Profile == MembershipProfile.SuperAdmin && requestingProfile != MembershipProfile.SuperAdmin)
                throw new BusinessRuleException("Apenas um super administrador pode alterar o perfil de outro super administrador");
            if (Profile == MembershipProfile.Admin && requestingProfile != MembershipProfile.SuperAdmin)
                throw new BusinessRuleException("Apenas um super administrador pode alterar o perfil de um administrador");
            if (requestingProfile != MembershipProfile.Admin && requestingProfile != MembershipProfile.SuperAdmin)
                throw new BusinessRuleException("Apenas administradores podem alterar o perfil dos usuários");
            if (Profile == profile)
                throw new BusinessRuleException("O usuário já possui o perfil informado");

            Profile = profile;
        }
    }
}
