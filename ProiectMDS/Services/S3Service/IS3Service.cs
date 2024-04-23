namespace ProiectMDS.Services
{
    public interface IS3Service
    {
        Task UploadFileAsync(string key, IFormFile poza);
        string GetFileUrl(string key);
        Task DeleteFileAsync(string key);
    }
}
