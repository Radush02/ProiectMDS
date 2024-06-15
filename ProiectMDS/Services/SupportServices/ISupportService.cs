using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services.SupportServices
{
    public interface ISupportService
    {
        Task AddSupport(SupportDTO supportDTO);
        Task<IEnumerable<SupportDTO>> getAllSupports();
        Task<IEnumerable<SupportDTO>> getSupportByUserId(int userId);
        Task<IEnumerable<SupportDTO>> getSupportBySupportId(int supportId);
        Task clientEmail(SupportDTO support);
        Task adminEmail(SupportDTO support);
    }
}
