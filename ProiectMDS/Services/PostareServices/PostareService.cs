using ProiectMDS.Models.DTOs;
using ProiectMDS.Models;
using ProiectMDS.Models.Repositories.PostareRepositories;

namespace ProiectMDS.Services
{
    public class PostareService : IPostareService
    {
        private readonly IPostareRepository _postareRepository;

        public PostareService(IPostareRepository postareRepository)
        {
            _postareRepository = postareRepository;
        }

        public async Task AddPostare(PostareDTO postareDTO)
        {
            var postare = new Postare()
            {
                UserId = postareDTO.UserId,
                titlu = postareDTO.titlu,
                descriere = postareDTO.descriere,
                pret =  postareDTO.pret,
                firma = postareDTO.firma,
                model = postareDTO.model,
                kilometraj = postareDTO.kilometraj,
                anFabricatie = postareDTO.anFabricatie,
                talon = postareDTO.talon,
                carteIdentitateMasina = postareDTO.carteIdentitateMasina,
                asigurare = postareDTO.asigurare
            };
            await _postareRepository.AddPostare(postare);
        }

        public async Task DeletePostare(int id)
        {
            await _postareRepository.DeletePostare(id);
        }
    }
}
