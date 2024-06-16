﻿using ProiectMDS.Models.DTOs;
using ProiectMDS.Models;
using ProiectMDS.Repositories;
using ProiectMDS.Exceptions;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace ProiectMDS.Services.ChirieServices
{
    public class ChirieService : IChirieService
    {
        private readonly IChirieRepository _chirieRepository;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;
        private readonly IGoogleService _googleService;
        

        public ChirieService(IChirieRepository chirieRepository, IEmailSender emailSender, UserManager<User> userManager, IGoogleService googleService)
        {
            _chirieRepository = chirieRepository;
            _emailSender = emailSender;
            _userManager = userManager;
            _googleService = googleService;
        }

        public async Task AddChirie(ChirieDTO chirieDTO)
        {
            var chirie = new Chirie()
            {
                PostareId = chirieDTO.postareId,
                UserId = chirieDTO.userId,
                dataStart = chirieDTO.dataStart,
                dataStop = chirieDTO.dataStop
            };

            if (chirieDTO.userId == await _chirieRepository.UserByPostareId(chirieDTO.postareId))
            {
                throw new Exception("Nu poti inchiria o postare pusa de tine!");
            }

            await _chirieRepository.AddChirie(chirie);

            var user = await _chirieRepository.UserById(chirie.UserId);

            TimeSpan zile = chirie.dataStop - chirie.dataStart;
            if(zile.Days == 0)
            {
                user.puncteFidelitate = user.puncteFidelitate + 1;
            }
            else
            {
                user.puncteFidelitate = user.puncteFidelitate + zile.Days;
            }
            
            await _chirieRepository.UpdatePuncteFidelitate(user);
        }

        public async Task<IEnumerable<ChirieDTO>> ChirieByDataStart(DateTime dataStart)
        {
            var c = await _chirieRepository.ChirieByDataStart(dataStart);

            if (c == null)
            {
                throw new NotFoundException($"Nu exista chirie cu aceasta data de inceput: {dataStart}.");
            }

            IEnumerable<ChirieDTO> rez;
            rez = c.Select(ch => new ChirieDTO
            {
                userId = ch.UserId,
                postareId = ch.PostareId,
                dataStart = ch.dataStart,
                dataStop = ch.dataStop
            });
            return rez;
        }

        public async Task<IEnumerable<ChirieDTO>> ChirieByDataStop(DateTime dataStop)
        {
            var c = await _chirieRepository.ChirieByDataStop(dataStop);

            if (c == null)
            {
                throw new NotFoundException($"Nu exista chirie cu aceasta data de sfarsit: {dataStop}.");
            }

            IEnumerable<ChirieDTO> rez;
            rez = c.Select(ch => new ChirieDTO
            {
                userId = ch.UserId,
                postareId = ch.PostareId,   
                dataStart = ch.dataStart,
                dataStop = ch.dataStop
            });
            return rez;
        }

        public async Task<IEnumerable<ChirieDTO>> ChirieByData(DateTime dataStart, DateTime dataStop)
        {
            var c = await _chirieRepository.ChirieByData(dataStart, dataStop);

            if (c == null)
            {
                throw new NotFoundException($"Nu exista chirie cu aceasta data de sfarsit: {dataStop}.");
            }

            IEnumerable<ChirieDTO> rez;
            rez = c.Select(ch => new ChirieDTO
            {
                userId = ch.UserId,
                postareId = ch.PostareId,
                dataStart = ch.dataStart,
                dataStop = ch.dataStop
            });
            return rez;
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

        public async Task rentConfirmationEmail(ChirieDTO chirie)
        {
            User renter = await _chirieRepository.UserById(chirie.userId);
            int ownerId = await _chirieRepository.UserByPostareId(chirie.postareId);
            Console.WriteLine(ownerId);
            User owner = await _chirieRepository.UserById(ownerId);
            Postare postare = await _chirieRepository.PostareById(chirie.postareId);

            if(postare == null || postare.adresa_formala == null || postare.adresa_formala == "")
            {
                throw new Exception();
            }

            string renterEmailHtml = await File.ReadAllTextAsync("Templates/RenterEmailTemplate.html");
            renterEmailHtml = renterEmailHtml.Replace("{{username}}", renter.UserName);
            renterEmailHtml = renterEmailHtml.Replace("{{firma}}", postare.firma);
            renterEmailHtml = renterEmailHtml.Replace("{{model}}", postare.model);
            renterEmailHtml = renterEmailHtml.Replace("{{seller}}", owner.UserName);
            renterEmailHtml = renterEmailHtml.Replace("{{data-start}}", chirie.dataStart.ToString("yyyy-MM-dd"));
            renterEmailHtml = renterEmailHtml.Replace("{{data-stop}}", chirie.dataStop.ToString("yyyy-MM-dd"));
            renterEmailHtml = renterEmailHtml.Replace("{{maps_img}}", _googleService.getLocationImageFromCoordinates(postare.latitudine,postare.longitudine));
            renterEmailHtml = renterEmailHtml.Replace("{{adresa-text}}", postare.adresa_formala)
                .Replace("{{latitudine}}", postare.latitudine.ToString()).Replace("{{longitudine}}",postare.longitudine.ToString());
            await _emailSender.SendEmailAsync(renter.Email, "Vehicul inchiriat cu succes", renterEmailHtml);

            string ownerEmailHtml = await File.ReadAllTextAsync("Templates/OwnerEmailTemplate.html");
            ownerEmailHtml = ownerEmailHtml.Replace("{{username}}", owner.UserName);
            ownerEmailHtml = ownerEmailHtml.Replace("{{cumparator}}", renter.UserName);
            ownerEmailHtml = ownerEmailHtml.Replace("{{firma}}", postare.firma);
            ownerEmailHtml = ownerEmailHtml.Replace("{{model}}", postare.model);
            ownerEmailHtml = ownerEmailHtml.Replace("{{data-start}}", chirie.dataStart.ToString("yyyy-MM-dd"));
            ownerEmailHtml = ownerEmailHtml.Replace("{{data-stop}}", chirie.dataStop.ToString("yyyy-MM-dd"));
            await _emailSender.SendEmailAsync(owner.Email, "Vehicul inchiriat cu succes", ownerEmailHtml);
        }
    }
}
