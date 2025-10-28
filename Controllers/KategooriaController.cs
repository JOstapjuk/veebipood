using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebipood.Data;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategooriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public KategooriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Kategooria>> GetAll() => _context.Kategooriad.ToList();

        [HttpGet("{id}")]
        public ActionResult<Kategooria> Get(int id)
        {
            var kategooria = _context.Kategooriad.Find(id);
            if (kategooria == null) return NotFound();
            return kategooria;
        }

        [HttpPost]
        public ActionResult<Kategooria> Create(Kategooria kategooria)
        {
            _context.Kategooriad.Add(kategooria);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = kategooria.Id }, kategooria);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Kategooria kategooria)
        {
            if (id != kategooria.Id) return BadRequest();
            _context.Entry(kategooria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var kategooria = _context.Kategooriad.Find(id);
            if (kategooria == null) return NotFound();

            _context.Kategooriad.Remove(kategooria);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
