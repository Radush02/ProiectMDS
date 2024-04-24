using ProiectMDS.Models;

namespace ProiectMDS.Models.Repositories.PostareRepositories
{
    public interface IPostareRepository
    {
        Task AddPostare(Postare postare);
        Task DeletePostare(int id);
        Task<Postare> PostareById(int postareId);
        Task UpdatePostare(Postare postare);
    }
}
