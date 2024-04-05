using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProiectMDS.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string nume { get; set; }

        public string prenume { get; set; }

        public string email {  get; set; }

        public string parola { get; set; }

        public string username { get; set; }

        public string nrTelefon {  get; set; }

        public string carteIdentitate {  get; set; }

        public string permis {  get; set; }

        public ICollection<Card>? card {  get; set; }

        public ICollection<Review>? review {  get; set; }

        public ICollection<Postare>? postare {  get; set; }

        public ICollection<Chirie>? chirie {  get; set; }
    }
}
