using MeetHub.Application.Interfaces;
using MeetHub.Domain.Entities;
using MeetHub.Domain.Interfaces;
using MeetHub.Application.Exceptions;
using MeetHub.Domain.Exceptions;
using MeetHub.Application.DTOs.MeetingDto;
using MeetHub.Application.DTOs;
using MeetHub.Domain.Enums.UserEnum;

namespace MeetHub.Application.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingService(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public async Task<CreateMeetingDto> CreateMeeting(string name, string creatorName)
        {
            var normalizedName = name.Trim().ToLowerInvariant();
            var allMeetings = await _meetingRepository.GetAll();

            if (allMeetings.Any(m => m.Name.Trim().ToLowerInvariant() == normalizedName))
                throw new BusinessRuleException("Já existe uma reunião com esse nome");
            Meeting meeting = new Meeting(name, creatorName);
            await _meetingRepository.Add(meeting);
            return new CreateMeetingDto
            {
                IdMeeting = meeting.Id,
                MeetingName = meeting.Name,
                CreatedAt = meeting.CreatedAt,
                IsActive = meeting.IsActive
            };
        }

        private async Task<Meeting> GetSupportMeeting(Guid id)
        {
            var meeting = await _meetingRepository.GetById(id);
            if (meeting == null)
                throw new NotFoundException("Reunião não encontrada");
            return meeting;
        }

        public async Task<MeetingDetailsDto> GetMeetingDetails(Guid id)
        {
            var meeting = await GetSupportMeeting(id);
            var meetingDetailsDto = new MeetingDetailsDto
            {
                Id = meeting.Id,
                Name = meeting.Name,
                CreatedAt = meeting.CreatedAt,
                IsActive = meeting.IsActive
            };
            return meetingDetailsDto;
        }
        public async Task<List<MeetingListDto>> GetAllMeetings()
        {
            var meetings = await _meetingRepository.GetAll();
            var meetingsDto = meetings.Select(meeting => new MeetingListDto { Id = meeting.Id, Name = meeting.Name }).ToList();
            return meetingsDto;
        }

        public async Task<PagedResultDto<MeetingListDto>> GetPaged(int pageNumber, int pageSize)
        {
            var (pageMeetings, totalCount) = await _meetingRepository.GetPaged(pageNumber, pageSize);
            var pageMeetingsDto = pageMeetings.Select(meeting => new MeetingListDto { Id = meeting.Id, Name = meeting.Name }).ToList();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            return new PagedResultDto<MeetingListDto>
            {
                Items = pageMeetingsDto,
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }
        public async Task<MeetingListDto> UpdateMeeting(string name, Guid id)
        {
            var meeting = await GetSupportMeeting(id);

            meeting.UpdateName(name);
            await _meetingRepository.Update(meeting);

            return new MeetingListDto {Id = meeting.Id, Name = meeting.Name };
        }

        public async Task FinishMeeting(Guid id)
        {
            Meeting meeting = await GetSupportMeeting(id);
            meeting.FinishMeeting();
            await _meetingRepository.Update(meeting);
        }

        private async Task<MeetingParticipant> GetParticipantForUpdate(Guid id, bool requirePresent = true)
        {
            var participant = await _meetingRepository.GetParticipantByIdWithMeeting(id);

            if (participant == null)
                throw new NotFoundException("Participante não encontrado");

            if (!participant.Meeting.IsActive)
                throw new BusinessRuleException("Reunião encerrada");

            if (requirePresent && !participant.IsPresent)
                throw new BusinessRuleException("Participante não está mais presente");

            return participant;
        }

        public async Task<AddParticipantResponseDto> AddParticipant(Guid meetingId, string participantName)
        {
            var meeting = await _meetingRepository.GetByIdWithParticipants(meetingId);
            if (meeting == null)
                throw new NotFoundException("Reunião não encontrada");
            if (meeting.IsActive == false)
                throw new BusinessRuleException("Não é possivel adicionar participantes a reuniões encerradas");
            var participant = meeting.AddParticipant(participantName);
            await _meetingRepository.AddMeetingParticipant(participant);
            return new AddParticipantResponseDto
            {
                MeetingId = meeting.Id,
                ParticipantName = participantName,
                Message = $"Participante '{participantName}' adicionado à reunião '{meeting.Name}' com sucesso."
            };
        }

        public async Task<List<ParticipantDto>> GetAllParticipants(Guid id)
        {
            var meeting = await _meetingRepository.GetById(id);
            if (meeting == null)
                throw new NotFoundException("Reunião não encontrada");

            var participants = await _meetingRepository.GetParticipantsByMeetingId(id);
            if (!participants.Any())
                throw new BusinessRuleException("Não existem participantes na reunião");

            var participantsDto = participants.Select(mp =>
            new ParticipantDto
            {
                IdParticipant = mp.Id,
                ParticipantName = mp.ParticipantName,
                IsPresent = mp.IsPresent,
                Profile = mp.PermissionProfile
            }).ToList();
            return participantsDto;
        }

        public async Task UpdateParticipantIsPresent(Guid id)
        {
            var participant = await GetParticipantForUpdate(id);
            participant.UpdateIsPresent();
            await _meetingRepository.Update(participant);
        }
        public async Task UpdateParticipantProfile(Guid id, PermissionProfile profile, PermissionProfile solicitantProfile)
        {
            var participant = await GetParticipantForUpdate(id);
            participant.UpdatePermissionProfile(profile, solicitantProfile);
            await _meetingRepository.Update(participant);

        }

        public async Task UpdateParticipantName(Guid id, string name)
        {
            var participant = await GetParticipantForUpdate(id);
            participant.UpdateName(name);
            await _meetingRepository.Update(participant);
        }
    }
}
