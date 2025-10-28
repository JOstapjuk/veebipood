using System.ComponentModel.DataAnnotations;

namespace veebipood.Models
{
    public class Arve
    {
        public int Id { get; set; }

        public DateTime Kuupaev { get; set; } = DateTime.UtcNow;

        public List<Arverida> Arveread { get; set; } = new();

        public decimal Kogusumma { get; set; }

        public Maksestaatus Maksestaatus { get; set; } = new();

        public Klient Klient { get; set; } = null!;
    }
}
