using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services
{
    public interface IOpenAIService { 
       Task<Models.OpenAIDTO> GetDescription(Models.OpenAIDTO prompt);
       Task<Models.OpenAIDTO> profilePictureFilter(IFormFile file);
        Task<IEnumerable<PostareDTO>> GetInfo(string prompt);
    }
}
