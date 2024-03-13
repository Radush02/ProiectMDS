using Microsoft.AspNetCore.Identity;

namespace ProiectMDS.Models
{
    public class User:IdentityUser<int>
    {
        public string Nume { get; set; }

        public string NrTelefon { get; set; }
    }
}
