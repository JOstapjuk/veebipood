using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebipood.Data;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KontaktandmedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KontaktandmedController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Kontaktandmed>> GetAll()
        {
            return _context.Kontaktandmed.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Kontaktandmed> Get(int id)
        {
            var kontakt = _context.Kontaktandmed.Find(id);
            if (kontakt == null) return NotFound();
            return kontakt;
        }

        [HttpPost]
        public ActionResult<Kontaktandmed> Create(Kontaktandmed kontakt)
        {
            _context.Kontaktandmed.Add(kontakt);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = kontakt.Id }, kontakt);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Kontaktandmed kontakt)
        {
            if (id != kontakt.Id) return BadRequest();
            _context.Entry(kontakt).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var kontakt = _context.Kontaktandmed.Find(id);
            if (kontakt == null) return NotFound();

            _context.Kontaktandmed.Remove(kontakt);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
