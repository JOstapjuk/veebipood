using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebipood.Data;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaksestaatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MaksestaatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Maksestaatus>> GetAll()
        {
            return _context.Maksestaatused.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Maksestaatus> Get(int id)
        {
            var maksestaatus = _context.Maksestaatused.Find(id);
            if (maksestaatus == null) return NotFound();
            return maksestaatus;
        }

        [HttpPost]
        public ActionResult<Maksestaatus> Create(Maksestaatus maksestaatus)
        {
            _context.Maksestaatused.Add(maksestaatus);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = maksestaatus.Id }, maksestaatus);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Maksestaatus maksestaatus)
        {
            if (id != maksestaatus.Id) return BadRequest();
            _context.Entry(maksestaatus).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var maksestaatus = _context.Maksestaatused.Find(id);
            if (maksestaatus == null) return NotFound();

            _context.Maksestaatused.Remove(maksestaatus);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
