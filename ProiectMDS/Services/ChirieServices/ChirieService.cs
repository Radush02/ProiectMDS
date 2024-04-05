using ProiectMDS.Models.DTOs;
using ProiectMDS.Models;
using ProiectMDS.Repositories.ChirieRepositories;

namespace ProiectMDS.Services.ChirieServices
{
    public class ChirieService : IChirieService
    {
        private readonly IChirieRepository _chirieRepository;

        public ChirieService(IChirieRepository chirieRepository)
        {
            _chirieRepository = chirieRepository;
        }

        public async Task AddChirie(ChirieDTO chirieDTO)
        {
            var chirie = new Chirie()
            {
                PostareId = chirieDTO.PostareId,
                UserId = chirieDTO.UserId,
                dataStart = chirieDTO.dataStart,
                dataStop = chirieDTO.dataStop
            };
            await _chirieRepository.AddChirie(chirie);
        }

        public async Task DeleteChirie(int id)
        {
            await _chirieRepository.DeleteChirie(id);
        }
    }
}
