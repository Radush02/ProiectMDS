using Microsoft.AspNetCore.Identity.UI.Services;
using ProiectMDS.Exceptions;
using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;
using ProiectMDS.Repositories.SupportRepositories;

namespace ProiectMDS.Services.SupportServices
{
    public class SupportService : ISupportService
    {
        private readonly ISupportRepository _supportRepository;
        private readonly IEmailSender _emailSender;
        public SupportService(ISupportRepository supportRepository, IEmailSender emailSender)
        {
            _supportRepository = supportRepository;
            _emailSender = emailSender;
        }
        public async Task AddSupport(SupportDTO supportDTO)
        {
            var support = new Support
            {
                UserId = supportDTO.userId,
                titlu = supportDTO.titlu + " from client",
                comentariu = supportDTO.comentariu
            };

            await _supportRepository.AddSupport(support);
        }

        public async Task<IEnumerable<SupportDTO>> getAllSupports()
        {
            IEnumerable<SupportDTO> rez;
            var s = await _supportRepository.getAllSupports();

            rez = s.Select(sup => new SupportDTO
            {
                userId = sup.UserId,
                titlu = sup.titlu,
                comentariu = sup.comentariu
            });

            return rez;
        }

        public async Task<IEnumerable<SupportDTO>> getSupportByUserId(int userId)
        {
            
            var s = await _supportRepository.getSupportByUserId(userId);

            if (s == null || !s.Any())
            {
                throw new NotFoundException("Nu exista support de la un user cu acest id");
            }

            IEnumerable<SupportDTO> rez;
            rez = s.Select(sup => new SupportDTO
            {
                userId = sup.UserId,
                titlu = sup.titlu,
                comentariu = sup.comentariu
            });

            return rez;
        }

        public async Task<IEnumerable<SupportDTO>> getSupportBySupportId(int supportId)
        {

            var s = await _supportRepository.getSupportBySupportId(supportId);

            if (s == null || !s.Any())
            {
                throw new NotFoundException("Nu exista support cu acest id");
            }

            IEnumerable<SupportDTO> rez;
            rez = s.Select(sup => new SupportDTO
            {
                userId = sup.UserId,
                titlu = sup.titlu,
                comentariu = sup.comentariu
            });

            return rez;
        }

        public async Task clientEmail(SupportDTO support)
        {

            var sup = new Support
            {
                UserId = support.userId,
                titlu = support.titlu + " from client",
                comentariu = support.comentariu
            };

            await _supportRepository.AddSupport(sup);

            User user = await _supportRepository.UserById(support.userId);
            string clientEmailHtml = await File.ReadAllTextAsync("Templates/ClientSupportEmailTemplate.html");
            clientEmailHtml = clientEmailHtml.Replace("{{titlu}}", support.titlu.ToString());
            clientEmailHtml = clientEmailHtml.Replace("{{username}}", user.UserName.ToString());
            clientEmailHtml = clientEmailHtml.Replace("{{userid}}", user.Id.ToString());
            clientEmailHtml = clientEmailHtml.Replace("{{comentariu}}", support.comentariu.ToString());
            await _emailSender.SendEmailAsync("cipriangabriel07@gmail.com", "Support", clientEmailHtml);
        }

        public async Task adminEmail(SupportDTO support)
        {
            var sup = new Support
            {
                UserId = support.userId,
                titlu = support.titlu + " from admin",
                comentariu = support.comentariu
            };

            await _supportRepository.AddSupport(sup);

            User user = await _supportRepository.UserById(support.userId);
            string clientEmailHtml = await File.ReadAllTextAsync("Templates/AdminSupportEmailTemplate.html");
            clientEmailHtml = clientEmailHtml.Replace("{{titlu}}", support.titlu.ToString());
            clientEmailHtml = clientEmailHtml.Replace("{{username}}", user.UserName.ToString());
            clientEmailHtml = clientEmailHtml.Replace("{{userid}}", user.Id.ToString());
            clientEmailHtml = clientEmailHtml.Replace("{{comentariu}}", support.comentariu.ToString());
            await _emailSender.SendEmailAsync(user.Email, "Support", clientEmailHtml);
        }
    }
}
