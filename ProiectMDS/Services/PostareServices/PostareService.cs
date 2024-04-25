﻿using ProiectMDS.Models.DTOs;
using ProiectMDS.Models;
using ProiectMDS.Models.Repositories.PostareRepositories;
using ProiectMDS.Exceptions;

namespace ProiectMDS.Services
{
    public class PostareService : IPostareService
    {
        private readonly IPostareRepository _postareRepository;

        public PostareService(IPostareRepository postareRepository)
        {
            _postareRepository = postareRepository;
        }

        public async Task AddPostare(PostareDTO postareDTO, int userId)
        {
            var postare = new Postare()
            {
                UserId = userId,
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

        public async Task<IEnumerable<PostareDTO>> PostareByAn(int anMinim, int anMaxim)
        {
            var p = await _postareRepository.PostareByAn(anMinim, anMaxim);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare in range ul acesta de ani {anMinim} - {anMaxim}.");
            }

            return p;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByFirma(string firma)
        {
            var p = await _postareRepository.PostareByFirma(firma);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare cu firma {firma}.");
            }

            return p;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByKm(int kmMinim, int kmMaxim)
        {
            var p = await _postareRepository.PostareByKm(kmMinim, kmMaxim);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare in range ul acesta de km {kmMinim} - {kmMaxim}.");
            }

            return p;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByModel(string model)
        {
            var p = await _postareRepository.PostareByModel(model);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare cu modelul {model}.");
            }

            return p;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByPret(int pretMinim, int pretMaxim)
        {
            var p = await _postareRepository.PostareByPret(pretMinim, pretMaxim);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare in range ul acesta de pret {pretMinim} - {pretMaxim}.");
            }

            return p;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByTitlu(string titlu)
        {
            var p = await _postareRepository.PostareByTitlu(titlu);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare cu titlul {titlu}.");
            }

            return p;
        }

        public async Task UpdatePostare(PostareDTO postareDTO, int postareId)
        {
            var p = await _postareRepository.PostareById(postareId);

            if (p == null)
            {
                throw new NotFoundException($"Nu exista postare cu id-ul {postareId}.");
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
