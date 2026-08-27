using MeetHub.Domain.Enums.UserEnum;
using MeetHub.Domain.Exceptions;

namespace MeetHub.Domain.Entities
{
    public class MeetingParticipant
    {
        public Guid Id { get; private set; }
        public Guid MeetingId { get; private set; }
        public Meeting Meeting { get; private set; }
        public string ParticipantName { get; private set; }
        public PermissionProfile PermissionProfile { get; private set; }
        public bool IsPresent { get; private set; }

        public MeetingParticipant(Guid meetingId, string participantName, PermissionProfile permissionProfile = PermissionProfile.Viewer)
        {
            if (string.IsNullOrWhiteSpace(participantName))
                throw new ArgumentException("O nome do participante não pode ser vazio");
            if(meetingId == Guid.Empty)
                throw new ArgumentException("O ID da reunião é inválido");
            Id = Guid.NewGuid();
            MeetingId = meetingId;
            ParticipantName = participantName;
            PermissionProfile = permissionProfile;
            IsPresent = true;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do participante não pode ser vazio");
            ParticipantName = name;
        }

        public void UpdateIsPresent()
        {
            if(!IsPresent)
                throw new BusinessRuleException("O participante já está ausente");
            IsPresent = false;
        }

        public void UpdatePermissionProfile(PermissionProfile profile, PermissionProfile solicitantProfile)
        {
            if (solicitantProfile != PermissionProfile.Admin)
                throw new BusinessRuleException("Somente o administrador pode alterar o usuario");
            if (profile == PermissionProfile.Admin)
                throw new BusinessRuleException("Só pode existir um administrador por reunião");

            PermissionProfile = profile;
        }

    }
}
