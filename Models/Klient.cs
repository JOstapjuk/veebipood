using System.ComponentModel.DataAnnotations;

namespace veebipood.Models
{
    public class Klient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kliendi nimi on kohustuslik")]
        public string Nimi { get; set; } = null!;

        public Kontaktandmed Kontaktandmed { get; set; } = null!;
        public Aadress Aadress { get; set; } = null!;
    }
}
