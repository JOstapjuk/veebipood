using System.ComponentModel.DataAnnotations;

namespace veebipood.Models
{
    public class Kontaktandmed
    {
        public int Id { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
    }
}
