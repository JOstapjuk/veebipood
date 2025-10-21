using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;

namespace veebipood.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
