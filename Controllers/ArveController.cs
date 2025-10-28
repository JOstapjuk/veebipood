using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebipood.Data;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArveController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ArveController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Kõik arved
        [HttpGet]
        public ActionResult<List<Arve>> GetAll()
        {
            return _context.Arved
                .Include(a => a.Arveread)
                    .ThenInclude(r => r.Toode)
                        .ThenInclude(t => t.Kategooria)
                .Include(a => a.Klient)
                    .ThenInclude(k => k.Kontaktandmed)
                .Include(a => a.Klient)
                    .ThenInclude(k => k.Aadress)
                .Include(a => a.Maksestaatus)
                .ToList();
        }

        // POST: lisa uus arve
        [HttpPost]
        public ActionResult<Arve> Create([FromBody] Arve arve)
        {
            if (arve.Klient == null || arve.Maksestaatus == null || arve.Arveread == null || !arve.Arveread.Any())
                return BadRequest("Klient, Maksestaatus ja Arveread on kohustuslikud.");

            // Load related entities from DB
            var klient = _context.Kliendid
                .Include(k => k.Kontaktandmed)
                .Include(k => k.Aadress)
                .FirstOrDefault(k => k.Id == arve.Klient.Id);
            var maks = _context.Maksestaatused.Find(arve.Maksestaatus.Id);

            if (klient == null) return BadRequest("Klient ei leitud");
            if (maks == null) return BadRequest("Maksestaatus ei leitud");

            arve.Klient = klient;
            arve.Maksestaatus = maks;

            // Process each Arverida
            foreach (var rida in arve.Arveread)
            {
                var toode = _context.Tooted
                    .Include(t => t.Kategooria)
                    .FirstOrDefault(t => t.Id == rida.Toode.Id);

                if (toode == null) return BadRequest($"Toode Id {rida.Toode.Id} ei leitud");

                rida.Id = 0; // Ensure EF treats it as a new entity
                rida.Toode = toode;
            }

            _context.Arved.Add(arve);
            _context.SaveChanges();

            // Reload the inserted Arve with all includes
            var result = _context.Arved
                .Include(a => a.Klient)
                    .ThenInclude(k => k.Kontaktandmed)
                .Include(a => a.Klient)
                    .ThenInclude(k => k.Aadress)
                .Include(a => a.Maksestaatus)
                .Include(a => a.Arveread)
                    .ThenInclude(r => r.Toode)
                        .ThenInclude(t => t.Kategooria)
                .FirstOrDefault(a => a.Id == arve.Id);

            return CreatedAtAction(nameof(GetAll), new { id = arve.Id }, result);
        }

        // Kõik maksmata arved
        [HttpGet("uletahtaja")]
        public ActionResult<List<Arve>> GetUletahtaja()
        {
            return _context.Arved
                .Include(a => a.Arveread)
                    .ThenInclude(r => r.Toode)
                        .ThenInclude(t => t.Kategooria)
                .Include(a => a.Klient)
                    .ThenInclude(k => k.Kontaktandmed)
                .Include(a => a.Klient)
                    .ThenInclude(k => k.Aadress)
                .Include(a => a.Maksestaatus)
                .Where(a => !a.Maksestaatus.Makstud && a.Maksestaatus.Maksetahtaeg < DateTime.Now)
                .ToList();
        }

        // Kõik ühe kliendi arved
        [HttpGet("klient/{klientId}")]
        public ActionResult<List<Arve>> GetByKlient(int klientId)
        {
            return _context.Arved
                .Include(a => a.Arveread)
                    .ThenInclude(r => r.Toode)
                        .ThenInclude(t => t.Kategooria)
                .Include(a => a.Klient)
                    .ThenInclude(k => k.Kontaktandmed)
                .Include(a => a.Klient)
                    .ThenInclude(k => k.Aadress)
                .Include(a => a.Maksestaatus)
                .Where(a => a.Klient.Id == klientId)
                .ToList();
        }

        // Ühe kliendi arvete kogusumma
        [HttpGet("klient/{klientId}/summa")]
        public ActionResult<decimal> GetKlientSumma(int klientId)
        {
            var summa = _context.Arved
                .Include(a => a.Arveread)
                    .ThenInclude(r => r.Toode)
                .Where(a => a.Klient.Id == klientId)
                .Sum(a => a.Kogusumma);
            return summa;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var arve = _context.Arved
                .Include(a => a.Arveread)
                .FirstOrDefault(a => a.Id == id);

            if (arve == null)
                return NotFound("Arve ei leitud.");

            // Remove all invoice lines first (if cascade delete isn't set)
            _context.Arveread.RemoveRange(arve.Arveread);

            _context.Arved.Remove(arve);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
