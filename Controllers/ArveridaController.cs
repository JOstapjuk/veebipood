using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebipood.Data;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArveridaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArveridaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Arverida>> GetAll()
        {
            return _context.Arveread
                .Include(a => a.Toode)
                .ThenInclude(t => t.Kategooria)
                .ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Arverida> Get(int id)
        {
            var arverida = _context.Arveread
                .Include(a => a.Toode)
                .ThenInclude(t => t.Kategooria)
                .FirstOrDefault(a => a.Id == id);

            if (arverida == null)
                return NotFound("Arverida ei leitud.");

            return arverida;
        }

        [HttpPost]
        public ActionResult<Arverida> Create([FromBody] Arverida arverida)
        {
            if (arverida.Toode == null)
                return BadRequest("Toode on kohustuslik.");

            var toode = _context.Tooted.Find(arverida.Toode.Id);
            if (toode == null)
                return BadRequest("Toode ei leitud.");

            if (arverida.Kogus <= 0)
                return BadRequest("Kogus peab olema suurem kui 0.");

            arverida.Toode = toode;
            _context.Arveread.Add(arverida);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = arverida.Id }, arverida);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Arverida arverida)
        {
            if (id != arverida.Id)
                return BadRequest("ID ei ühti.");

            if (arverida.Kogus <= 0)
                return BadRequest("Kogus peab olema suurem kui 0.");

            if (arverida.Toode != null)
            {
                var toode = _context.Tooted.Find(arverida.Toode.Id);
                if (toode == null)
                    return BadRequest("Toode ei leitud.");
                arverida.Toode = toode;
            }

            _context.Entry(arverida).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var arverida = _context.Arveread.Find(id);
            if (arverida == null)
                return NotFound("Arverida ei leitud.");

            _context.Arveread.Remove(arverida);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
