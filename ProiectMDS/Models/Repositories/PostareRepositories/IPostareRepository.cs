using ProiectMDS.Models;

namespace ProiectMDS.Models.Repositories.PostareRepositories
{
    public interface IPostareRepository
    {
        Task AddPostare(Postare postare);
        Task DeletePostare(int id);
    }
}
