using System.ComponentModel.DataAnnotations;

namespace veebipood.Models
{
    public class Toode
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Toote nimetus on kohustuslik")]
        public string Nimetus { get; set; } = null!;

        public Kategooria Kategooria { get; set; } = null!;

        [Range(0.01, double.MaxValue, ErrorMessage = "Hind peab olema suurem kui 0")]
        public decimal Hind { get; set; }

        public string? PildiUrl { get; set; }

        public bool Aktiivne { get; set; } = true;

        public int Laokogus { get; set; }

        public DateTime Vananemisaeg { get; set; }
    }
}
