using ProiectMDS.Models.DTOs;
using ProiectMDS.Models;
using ProiectMDS.Repositories;
using ProiectMDS.Exceptions;

namespace ProiectMDS.Services
{
    public class PostareService : IPostareService
    {
        private readonly IPostareRepository _postareRepository;
        private readonly IS3Service _s3Service;

        public PostareService(IPostareRepository postareRepository, IS3Service s3Service)
        {
            _postareRepository = postareRepository;
            _s3Service = s3Service;
        }

        public async Task AddPostare(PostareDTO postareDTO)
        {
            for(int i = 0; i < postareDTO.imagini.Count; i++)
            {
                var fileName = postareDTO.imagini[i].FileName;
                var extension = Path.GetExtension(fileName);
                var numeFisier = $"{postareDTO.userId}_{i + 1}{extension}";
                await _s3Service.UploadFileAsync(numeFisier, postareDTO.imagini[i]);
            }   
            var postare = new Postare()
            {
                UserId = postareDTO.userId,
                titlu = postareDTO.titlu,
                descriere = postareDTO.descriere,
                pret =  postareDTO.pret,
                firma = postareDTO.firma,
                model = postareDTO.model,
                kilometraj = postareDTO.kilometraj,
                anFabricatie = postareDTO.anFabricatie,
                talon = postareDTO.talon,
                carteIdentitateMasina = postareDTO.carteIdentitateMasina,
                asigurare = postareDTO.asigurare,
                nrImagini = postareDTO.imagini.Count
            };
            await _postareRepository.AddPostare(postare);
        }

        public async Task DeletePostare(int id)
        {
            await _postareRepository.DeletePostare(id);
        }

        public async Task<IEnumerable<PostareDTO>> PostareByAn(int anMinim, int anMaxim)
        {
            var p = await _postareRepository.PostareByAn(anMinim, anMaxim);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare in range ul acesta de ani {anMinim} - {anMaxim}.");
            }

            IEnumerable<PostareDTO> rez;
            rez = p.Select(po => new PostareDTO
            {
                userId = po.UserId,
                titlu = po.titlu,
                descriere = po.descriere,
                pret = po.pret,
                firma = po.firma,
                model = po.model,
                kilometraj = po.kilometraj,
                anFabricatie = po.anFabricatie,
                talon = po.talon,
                carteIdentitateMasina = po.carteIdentitateMasina,
                asigurare = po.asigurare
            });
            return rez;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByFirma(string firma)
        {
            var p = await _postareRepository.PostareByFirma(firma);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare cu firma {firma}.");
            }

            IEnumerable<PostareDTO> rez;
            rez = p.Select(po => new PostareDTO
            {
                userId = po.UserId,
                titlu = po.titlu,
                descriere = po.descriere,
                pret = po.pret,
                firma = po.firma,
                model = po.model,
                kilometraj = po.kilometraj,
                anFabricatie = po.anFabricatie,
                talon = po.talon,
                carteIdentitateMasina = po.carteIdentitateMasina,
                asigurare = po.asigurare
            });
            return rez;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByKm(int kmMinim, int kmMaxim)
        {
            var p = await _postareRepository.PostareByKm(kmMinim, kmMaxim);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare in range ul acesta de km {kmMinim} - {kmMaxim}.");
            }

            IEnumerable<PostareDTO> rez;
            rez = p.Select(po => new PostareDTO
            {
                userId = po.UserId,
                titlu = po.titlu,
                descriere = po.descriere,
                pret = po.pret,
                firma = po.firma,
                model = po.model,
                kilometraj = po.kilometraj,
                anFabricatie = po.anFabricatie,
                talon = po.talon,
                carteIdentitateMasina = po.carteIdentitateMasina,
                asigurare = po.asigurare
            });
            return rez;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByModel(string model)
        {
            var p = await _postareRepository.PostareByModel(model);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare cu modelul {model}.");
            }

            IEnumerable<PostareDTO> rez;
            rez = p.Select(po => new PostareDTO
            {
                userId = po.UserId,
                titlu = po.titlu,
                descriere = po.descriere,
                pret = po.pret,
                firma = po.firma,
                model = po.model,
                kilometraj = po.kilometraj,
                anFabricatie = po.anFabricatie,
                talon = po.talon,
                carteIdentitateMasina = po.carteIdentitateMasina,
                asigurare = po.asigurare
            });
            return rez;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByPret(int pretMinim, int pretMaxim)
        {
            var p = await _postareRepository.PostareByPret(pretMinim, pretMaxim);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare in range ul acesta de pret {pretMinim} - {pretMaxim}.");
            }
            IEnumerable<PostareDTO> rez;
            rez = p.Select(po => new PostareDTO
            {
                userId = po.UserId,
                titlu = po.titlu,
                descriere = po.descriere,
                pret = po.pret,
                firma = po.firma,
                model = po.model,
                kilometraj = po.kilometraj,
                anFabricatie = po.anFabricatie,
                talon = po.talon,
                carteIdentitateMasina = po.carteIdentitateMasina,
                asigurare = po.asigurare
            });
            return rez;
        }
        public async Task<IEnumerable<PostareDTO>> getAllPostari()
        {
            var p = await _postareRepository.getPostare();
            IEnumerable<PostareDTO> rez;
            rez = p.Select(po => new PostareDTO
            {
                userId = po.UserId,
                titlu = po.titlu,
                descriere = po.descriere,
                pret = po.pret,
                firma = po.firma,
                model = po.model,
                kilometraj = po.kilometraj,
                anFabricatie = po.anFabricatie,
                talon = po.talon,
                carteIdentitateMasina = po.carteIdentitateMasina,
                asigurare = po.asigurare
            });
            return rez;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByTitlu(string titlu)
        {
            var p = await _postareRepository.PostareByTitlu(titlu);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare cu titlul {titlu}.");
            }

            IEnumerable<PostareDTO> rez;
            rez = p.Select(po => new PostareDTO
            {
                userId = po.UserId,
                titlu = po.titlu,
                descriere = po.descriere,
                pret = po.pret,
                firma = po.firma,
                model = po.model,
                kilometraj = po.kilometraj,
                anFabricatie = po.anFabricatie,
                talon = po.talon,
                carteIdentitateMasina = po.carteIdentitateMasina,
                asigurare = po.asigurare
            });
            return rez;
        }

        public async Task UpdatePostare(PostareDTO postareDTO)
        {
            var p = await _postareRepository.PostareById(postareDTO.userId);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare cu id-ul {postareDTO.userId}.");
            }

            p.titlu = postareDTO.titlu;
            p.descriere = postareDTO.descriere;
            p.pret = postareDTO.pret;
            p.firma = postareDTO.firma;
            p.model = postareDTO.model;
            p.kilometraj = postareDTO.kilometraj;
            p.anFabricatie = postareDTO.anFabricatie;
            p.talon = postareDTO.talon;
            p.carteIdentitateMasina = postareDTO.carteIdentitateMasina;
            p.asigurare = postareDTO.asigurare;

            await _postareRepository.UpdatePostare(p);
        }
    }
}
