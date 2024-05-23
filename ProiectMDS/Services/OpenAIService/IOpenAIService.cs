using ProiectMDS.Models;

namespace ProiectMDS.Services
{
    public interface IOpenAIService { 
       Task<Models.OpenAIDTO> GetDescription(Models.OpenAIDTO prompt);
       Task<Models.OpenAIDTO> profilePictureFilter(IFormFile file);
        Task<IEnumerable<Postare>> GetInfo(string prompt);
    }
}
