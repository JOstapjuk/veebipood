using Microsoft.EntityFrameworkCore;
using veebipood.Models;

namespace veebipood.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Toode> Tooted { get; set; }
        public DbSet<Kategooria> Kategooriad { get; set; }
        public DbSet<Klient> Kliendid { get; set; }
        public DbSet<Kontaktandmed> Kontaktandmed { get; set; }
        public DbSet<Aadress> Aadressid { get; set; }
        public DbSet<Arve> Arved { get; set; }
        public DbSet<Arverida> Arveread { get; set; }
        public DbSet<Maksestaatus> Maksestaatused { get; set; }
    }
}
