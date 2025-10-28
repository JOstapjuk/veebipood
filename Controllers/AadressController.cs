using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebipood.Data;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AadressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AadressController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Aadress>> GetAll()
        {
            return _context.Aadressid.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Aadress> Get(int id)
        {
            var aadress = _context.Aadressid.Find(id);
            if (aadress == null) return NotFound();
            return aadress;
        }

        [HttpPost]
        public ActionResult<Aadress> Create(Aadress aadress)
        {
            _context.Aadressid.Add(aadress);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = aadress.Id }, aadress);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Aadress aadress)
        {
            if (id != aadress.Id) return BadRequest();
            _context.Entry(aadress).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aadress = _context.Aadressid.Find(id);
            if (aadress == null) return NotFound();

            _context.Aadressid.Remove(aadress);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
