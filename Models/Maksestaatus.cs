namespace veebipood.Models
{
    public class Maksestaatus
    {
        public int Id { get; set; }

        public bool Makstud { get; set; } = false;

        public DateTime Maksetahtaeg { get; set; }

        public decimal MakstudSumma { get; set; }

        public DateTime? MaksmiseKuupaev { get; set; }
    }
}
