using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebipood.Data;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToodeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ToodeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Toode>> GetAll()
        {
            return _context.Tooted
                .Include(t => t.Kategooria)
                .ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Toode> Get(int id)
        {
            var toode = _context.Tooted
                .Include(t => t.Kategooria)
                .FirstOrDefault(t => t.Id == id);

            if (toode == null)
                return NotFound("Toode ei leitud.");

            return toode;
        }

        [HttpPost]
        public ActionResult<Toode> Create([FromBody] Toode toode)
        {
            if (toode.Kategooria == null)
                return BadRequest("Kategooria on kohustuslik.");

            var kategooria = _context.Kategooriad.Find(toode.Kategooria.Id);
            if (kategooria == null)
                return BadRequest("Kategooriat ei leitud.");

            if (toode.Vananemisaeg < DateTime.Now)
                return BadRequest("Toote vananemisaeg ei tohi olla minevikus.");
            if (toode.Hind <= 0)
                return BadRequest("Toote hind peab olema suurem kui 0.");

            toode.Kategooria = kategooria;

            _context.Tooted.Add(toode);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = toode.Id }, toode);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Toode toode)
        {
            if (id != toode.Id)
                return BadRequest("ID ei ühti.");

            if (toode.Vananemisaeg < DateTime.Now)
                return BadRequest("Toote vananemisaeg ei tohi olla minevikus.");
            if (toode.Hind <= 0)
                return BadRequest("Toote hind peab olema suurem kui 0.");

            if (toode.Kategooria != null)
            {
                var kategooria = _context.Kategooriad.Find(toode.Kategooria.Id);
                if (kategooria == null)
                    return BadRequest("Kategooriat ei leitud.");
                toode.Kategooria = kategooria;
            }

            _context.Entry(toode).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var toode = _context.Tooted.Find(id);
            if (toode == null)
                return NotFound("Toode ei leitud.");

            _context.Tooted.Remove(toode);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
