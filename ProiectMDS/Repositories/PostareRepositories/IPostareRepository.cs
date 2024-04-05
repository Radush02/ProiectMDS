using ProiectMDS.Models;

namespace ProiectMDS.Repositories.PostareRepositories
{
    public interface IPostareRepository
    {
        Task AddPostare(Postare postare);
        Task DeletePostare(int id);
    }
}
