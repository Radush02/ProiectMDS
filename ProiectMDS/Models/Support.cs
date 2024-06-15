using System.ComponentModel.DataAnnotations;
namespace ProiectMDS.Models
{
    public class Support
    {
        [Key]
        public int SupportId {  get; set; }
        public int UserId { get; set; }
        public string titlu {  get; set; }
        public string comentariu {  get; set; }
        public virtual User User { get; set; }
    }
}
