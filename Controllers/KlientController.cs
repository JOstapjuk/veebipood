using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebipood.Data;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KlientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Klient>> GetAll()
        {
            return _context.Kliendid
                .Include(k => k.Kontaktandmed)
                .Include(k => k.Aadress)
                .ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Klient> Get(int id)
        {
            var klient = _context.Kliendid
                .Include(k => k.Kontaktandmed)
                .Include(k => k.Aadress)
                .FirstOrDefault(k => k.Id == id);

            if (klient == null) return NotFound();
            return klient;
        }

        [HttpPost]
        public ActionResult<Klient> Create([FromBody] Klient klient)
        {
            if (klient.Kontaktandmed == null || klient.Aadress == null)
                return BadRequest("Kontaktandmed ja Aadress on kohustuslikud.");

            var kontakt = _context.Kontaktandmed.Find(klient.Kontaktandmed.Id);
            var aadress = _context.Aadressid.Find(klient.Aadress.Id);

            if (kontakt != null) klient.Kontaktandmed = kontakt;
            if (aadress != null) klient.Aadress = aadress;

            _context.Kliendid.Add(klient);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = klient.Id }, klient);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Klient klient)
        {
            if (id != klient.Id) return BadRequest();
            _context.Entry(klient).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var klient = _context.Kliendid
                .Include(k => k.Kontaktandmed)
                .Include(k => k.Aadress)
                .FirstOrDefault(k => k.Id == id);

            if (klient == null) return NotFound();

            _context.Kliendid.Remove(klient);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
