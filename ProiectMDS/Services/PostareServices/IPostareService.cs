using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services
{
    public interface IPostareService
    {
        Task AddPostare(PostareDTO postareDTO);
        Task DeletePostare(int id);
    }
}
