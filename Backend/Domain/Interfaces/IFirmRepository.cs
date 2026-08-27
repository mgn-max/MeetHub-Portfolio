using MeetHub.Domain.Entities;

namespace MeetHub.Domain.Interfaces
{
    public interface IFirmRepository
    {
        Task Add(Firm firm);
        Task<Firm?> GetById(Guid id);
        Task Update(Firm firm);
        

        Task<bool> ExistsByCnpj(string cnpj);
        Task<bool> ExistsByCnpjExceptId(string cnpj, Guid id);
    }
}
