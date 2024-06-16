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
                SupportId = await _supportRepository.getMaxID() + 1,
                UserId = supportDTO.userId,
                titlu = supportDTO.titlu,
                comentariu = supportDTO.comentariu
            };

            await _supportRepository.AddSupport(support);
        }
        public async Task ReplySupport(SupportDTO supportDTO)
        {

            var support = new Support
            {
                SupportId = supportDTO.supportId,
                UserId = supportDTO.userId,
                titlu = supportDTO.titlu,
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
                supportId = sup.SupportId,
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
                supportId = sup.SupportId,
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
            {supportId = sup.SupportId, 
                userId = sup.UserId,
                titlu = sup.titlu,
                comentariu = sup.comentariu
            });

            return rez;
        }



        public async Task replyEmail(SupportDTO support)
        {
            User u = await _supportRepository.UserById(support.userId);
            string clientEmailHtml = await File.ReadAllTextAsync("Templates/ClientSupportEmailTemplate.html");
            clientEmailHtml = clientEmailHtml.Replace("{{titlu}}", support.titlu.ToString())
                .Replace("{{username}}", u.UserName.ToString())
                .Replace("{{link-ticket}}", "http://localhost:4200/ticket?id="+support.supportId.ToString());
            await _emailSender.SendEmailAsync(u.Email, "Support", clientEmailHtml);
        }
        public async Task adminEmail(SupportDTO support)
        {

            User user = await _supportRepository.UserById(support.userId);
            string clientEmailHtml = await File.ReadAllTextAsync("Templates/AdminSupportEmailTemplate.html");
            clientEmailHtml = clientEmailHtml.Replace("{{titlu}}", support.titlu.ToString());
            clientEmailHtml = clientEmailHtml.Replace("{{username}}", user.UserName.ToString());
            clientEmailHtml = clientEmailHtml.Replace("{{comentariu}}", support.comentariu.ToString());
            await _emailSender.SendEmailAsync(user.Email, "Support", clientEmailHtml);
        }
    }
}
