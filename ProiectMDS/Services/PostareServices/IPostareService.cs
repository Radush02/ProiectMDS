using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services.PostareServices
{
    public interface IPostareService
    {
        Task AddPostare(PostareDTO postareDTO);
        Task DeletePostare(int id);
    }
}
