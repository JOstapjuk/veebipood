using System.ComponentModel.DataAnnotations;

namespace veebipood.Models
{
    public class Arverida
    {
        public int Id { get; set; }

        public Toode Toode { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Kogus peab olema vähemalt 1")]
        public int Kogus { get; set; }
    }
}
