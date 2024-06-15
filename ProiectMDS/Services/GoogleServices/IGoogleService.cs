namespace ProiectMDS.Services
{
    public interface IGoogleService
    {
        Task<string> check(string text);
        Task<(double Latitude, double Longitude)> GetCoordinatesAsync(string location);
        Task<string> getLocationFromCoordinates(double latitude, double longitude);
        Task<string> GetIdfromLocationAsync(string location);
        string getLocationURL(string location);
        string getLocationImageFromCoordinates(double latitude, double longitude);
    }
}
