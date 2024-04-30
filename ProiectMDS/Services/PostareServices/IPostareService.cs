using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services
{
    public interface IPostareService
    {
        Task AddPostare(PostareDTO postareDTO);
        Task DeletePostare(int id);
        Task UpdatePostare(PostareDTO postare);
        Task<IEnumerable<PostareDTO>> getAllPostari();
        Task<IEnumerable<PostareDTO>> PostareByTitlu(String titlu);
        Task<IEnumerable<PostareDTO>> PostareByPret(int pretMinim, int pretMaxim);
        Task<IEnumerable<PostareDTO>> PostareByKm(int kmMinim, int kmMaxim);
        Task<IEnumerable<PostareDTO>> PostareByAn(int anMinim, int anMaxim);
        Task<IEnumerable<PostareDTO>> PostareByFirma(String firma);
        Task<IEnumerable<PostareDTO>> PostareByModel(String model);

    }
}
