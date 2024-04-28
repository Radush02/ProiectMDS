using ProiectMDS.Models.DTOs;
using ProiectMDS.Models;
using ProiectMDS.Models.Repositories.ChirieRepositories;
using ProiectMDS.Exceptions;

namespace ProiectMDS.Services
{
    public class ChirieService : IChirieService
    {
        private readonly IChirieRepository _chirieRepository;

        public ChirieService(IChirieRepository chirieRepository)
        {
            _chirieRepository = chirieRepository;
        }

        public async Task AddChirie(ChirieDTO chirieDTO, int postareId, int userId)
        {
            var chirie = new Chirie()
            {
                PostareId = postareId,
                UserId = userId,
                dataStart = chirieDTO.dataStart,
                dataStop = chirieDTO.dataStop
            };
            await _chirieRepository.AddChirie(chirie);
        }

        public async Task<IEnumerable<ChirieDTO>> ChirieByDataStart(DateTime dataStart)
        {
            var c = await _chirieRepository.ChirieByDataStart(dataStart);

            if (c == null)
            {
                throw new NotFoundException($"Nu exista chirie cu aceasta data de inceput: {dataStart}.");
            }

            return c;
        }

        public async Task<IEnumerable<ChirieDTO>> ChirieByDataStop(DateTime dataStop)
        {
            var c = await _chirieRepository.ChirieByDataStop(dataStop);

            if (c == null)
            {
                throw new NotFoundException($"Nu exista chirie cu aceasta data de sfarsit: {dataStop}.");
            }

            return c;
        }

        public async Task<IEnumerable<ChirieDTO>> ChirieByData(DateTime dataStart, DateTime dataStop)
        {
            var c = await _chirieRepository.ChirieByData(dataStart, dataStop);

            if (c == null)
            {
                throw new NotFoundException($"Nu exista chirie cu aceasta data de sfarsit: {dataStop}.");
            }

            return c;
        }

        public async Task DeleteChirie(int id)
        {
            await _chirieRepository.DeleteChirie(id);
        }

        public async Task UpdateChirie(ChirieDTO chirie, int id)
        {
            var c = await _chirieRepository.ChirieById(id);

            if (c == null)
            {
                throw new NotFoundException($"Nu exista chirie cu id-ul {id}.");
            }

            c.dataStart = chirie.dataStart;
            c.dataStop = chirie.dataStop;

            await _chirieRepository.UpdateChirie(c);
        }
    }
}
