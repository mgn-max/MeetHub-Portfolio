using MeetHub.Domain.Enums.UserEnum;
using MeetHub.Domain.Exceptions;

namespace MeetHub.Domain.Entities
{
    public class Meeting
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }
        public List<MeetingParticipant> MeetingParticipants { get; private set; } = new List<MeetingParticipant>();

        private Meeting() { }
        public Meeting(string name, string creatorName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome da reunião não pode ser vazio");
            if(string.IsNullOrWhiteSpace(creatorName))
                throw new ArgumentException("O nome do criador da reunião não pode ser vazio");

            Name = name;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
            var creator = new MeetingParticipant(this.Id, creatorName, PermissionProfile.Admin);
            MeetingParticipants.Add(creator);
        }
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome da reunião não pode ser vazio");

            Name = name;
        }
        public void FinishMeeting()
        {
            if (!IsActive)
                throw new BusinessRuleException("A reunião já está encerrada");

            IsActive = false;
        }
        public MeetingParticipant AddParticipant(string participantName)
        {
            if (string.IsNullOrWhiteSpace(participantName))
                throw new ArgumentException("é preciso informar um nome para o participante");
            var participant = new MeetingParticipant(this.Id, participantName);
            MeetingParticipants.Add(participant);
            return participant;
        }
    }
}
