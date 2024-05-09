namespace ProiectMDS.Services
{
    public interface IOpenAIService { 
       Task<Models.OpenAIDTO> GetDescription(Models.OpenAIDTO prompt);
    }
}
