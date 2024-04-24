using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services
{
    public interface IPostareService
    {
        Task AddPostare(PostareDTO postareDTO, int userId);
        Task DeletePostare(int id);
        Task UpdatePostare(PostareDTO postare, int postareId);
    }
}
