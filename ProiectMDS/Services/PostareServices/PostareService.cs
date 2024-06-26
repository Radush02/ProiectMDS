﻿using ProiectMDS.Models.DTOs;
using ProiectMDS.Models;
using ProiectMDS.Repositories;
using ProiectMDS.Exceptions;

namespace ProiectMDS.Services
{
    public class PostareService : IPostareService
    {
        private readonly IPostareRepository _postareRepository;
        private readonly IS3Service _s3Service;
        private readonly IGoogleService _googleService;

        public PostareService(IPostareRepository postareRepository, IS3Service s3Service,IGoogleService googleService)
        {
            _postareRepository = postareRepository;
            _s3Service = s3Service;
            _googleService = googleService;
        }

        public async Task<int> AddPostare(PostareDTO postareDTO)
        {
            int idx;
            try
            {
                idx = await _postareRepository.CountPostare() + 1;
            }
            catch
            {
                idx = 1;
            }
            string aux;
            double lat,longitudine;
            try
            {
                Console.WriteLine(postareDTO.culoare);
                aux = await _googleService.check(postareDTO.culoare);
                (lat,longitudine) = await _googleService.GetCoordinatesAsync(postareDTO.locatie);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                culoare = aux,
                nrImagini = postareDTO.imagini.Count,
                adresa_user = postareDTO.locatie,
                latitudine = lat,
                longitudine = longitudine,
                adresa_formala = await _googleService.getLocationFromCoordinates(lat, longitudine)
             
            };
            await _postareRepository.AddPostare(postare);

            for (int i = 0; i < postareDTO.imagini.Count; i++)
            {
                var fileName = $"post{idx}/{postareDTO.imagini[i].FileName}";
                await _s3Service.UploadFileAsync(fileName, postareDTO.imagini[i]);
            }
            return idx;
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
                id = po.PostareId,
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
                culoare = po.culoare,
                asigurare = po.asigurare,
                locatie = po.adresa_user,
                locatie_formala = po.adresa_formala,
                linkMaps = _googleService.getLocationURL(po.adresa_formala)
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
                id= po.PostareId,
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
                culoare = po.culoare,
                asigurare = po.asigurare,
                locatie = po.adresa_user,
                locatie_formala = po.adresa_formala,
                linkMaps = _googleService.getLocationURL(po.adresa_formala)
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
                id=po.PostareId,
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
                culoare = po.culoare,
                asigurare = po.asigurare,
                locatie = po.adresa_user,
                locatie_formala = po.adresa_formala,
                linkMaps = _googleService.getLocationURL(po.adresa_formala)
            });
            return rez;
        }
        public async Task<int> NrPostareByUser(int userId)
        {
            return await _postareRepository.NrPostareByUser(userId);
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
            {id = po.PostareId,
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
                culoare = po.culoare,
                asigurare = po.asigurare,
                locatie = po.adresa_user,
                locatie_formala = po.adresa_formala,
                linkMaps = _googleService.getLocationURL(po.adresa_formala)
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
            {id = po.PostareId,
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
                culoare = po.culoare,
                asigurare = po.asigurare,
                locatie = po.adresa_user,
                locatie_formala = po.adresa_formala,
                linkMaps = _googleService.getLocationURL(po.adresa_formala)
            });
            return rez;
        }
        public async Task<IEnumerable<PostareDTO>> getAllPostari()
        {
            var p = await _postareRepository.getPostare();
            IEnumerable<PostareDTO> rez;
            rez = p.Select(po => new PostareDTO
            {
                id=po.PostareId,
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
                culoare = po.culoare,
                asigurare = po.asigurare,
                locatie = po.adresa_user,
                locatie_formala = po.adresa_formala,
                linkMaps = _googleService.getLocationURL(po.adresa_formala)
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
            {id = po.PostareId,
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
                culoare = po.culoare,
                asigurare = po.asigurare,
                locatie = po.adresa_user,
                locatie_formala = po.adresa_formala,
                linkMaps = _googleService.getLocationURL(po.adresa_formala)
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
            p.culoare = postareDTO.culoare;

            await _postareRepository.UpdatePostare(p);
        }

        public async Task<IEnumerable<PostareDTO>> PostareByUserId(int userId)
        {
            var p = await _postareRepository.PostareByUserId(userId);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare cu userId {userId}.");
            }

            IEnumerable<PostareDTO> rez;
            rez = p.Select(po => new PostareDTO
            {
              
                id = po.PostareId,
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
                culoare = po.culoare,
                asigurare = po.asigurare,
                locatie = po.adresa_user,
                locatie_formala = po.adresa_formala,
                linkMaps = _googleService.getLocationURL(po.adresa_formala)
            });
            return rez;
        }
        public async Task<PostareDTO> postareById(int id)
        {
            var post = await _postareRepository.PostareById(id);
            if (post == null)
            {
                throw new NotFoundException($"Nu exista postare cu id-ul {id}");
            }
            var postDTO = new PostareDTO
            {
                id=post.PostareId,
                userId = post.UserId,
                titlu = post.titlu,
                descriere = post.descriere,
                pret = post.pret,
                firma = post.firma,
                model = post.model,
                kilometraj = post.kilometraj,
                anFabricatie = post.anFabricatie,
                talon = post.talon,
                carteIdentitateMasina = post.carteIdentitateMasina,
                culoare = post.culoare,
                asigurare = post.asigurare,
                locatie = post.adresa_user,
                locatie_formala = post.adresa_formala,
                linkMaps = _googleService.getLocationURL(post.adresa_formala)
            };
            return postDTO;
        }
    }

}
