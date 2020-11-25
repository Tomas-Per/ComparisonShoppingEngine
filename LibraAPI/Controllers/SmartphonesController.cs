using DataContent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItemLibrary;
using ItemLibrary.DataContexts;

namespace LibraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartphonesController : ControllerBase
    {
        private readonly ISmartphoneRepository _repository;

        public SmartphonesController(ISmartphoneRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Smartphones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Smartphone>>> GetSmartphones()
        {
            return await _context.Smartphones.ToListAsync();
        }

        // GET: api/Smartphones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Smartphone>> GetSmartphone(int id)
        {
            var smartphone = await _context.Smartphones.FindAsync(id);

            if (smartphone == null)
            {
                return NotFound();
            }

            return smartphone;
        }

        // PUT: api/Smartphones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmartphone(int id, Smartphone smartphone)
        {
            if (id != smartphone.Id)
            {
                return BadRequest();
            }

            _context.Entry(smartphone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmartphoneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Smartphones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Smartphone>> PostSmartphone(Smartphone smartphone)
        {
            _context.Smartphones.Add(smartphone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSmartphone", new { id = smartphone.Id }, smartphone);
        }

        // DELETE: api/Smartphones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmartphone(int id)
        {
            var smartphone = await _context.Smartphones.FindAsync(id);
            if (smartphone == null)
            {
                return NotFound();
            }

            _context.Smartphones.Remove(smartphone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SmartphoneExists(int id)
        {
            return _context.Smartphones.Any(e => e.Id == id);
        }
    }
}
