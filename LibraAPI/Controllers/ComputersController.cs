using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItemLibrary;
using ItemLibrary.DataContexts;
using DataContent.DAL.Interfaces;

namespace LibraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private readonly IComputerRepository _repository;

        public ComputersController(IComputerRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Computers
        [HttpGet]
        public async Task<ActionResult<List<Computer>>> GetComputers()
        {
            var computers = await _repository.GetAllComputersAsync();
            return computers;
        }

        // GET: api/Computers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Computer>> GetComputer(int id)
        {
            var computer = await _repository.GetComputerByIdAsync(id);

            if (computer == null)
            {
                return NotFound();
            }

            return computer;
        }

        // PUT: api/Computers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutComputer(Computer computer)
        {
            if (computer == null)
            {
                return BadRequest();
            }

            await _repository.UpdateComputerAsync(computer);

            return NoContent();
        }

        //// POST: api/Computers
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Computer>> PostComputer(Computer computer)
        //{
        //    _context.Computers.Add(computer);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetComputer", new { id = computer.Id }, computer);
        //}

        //// DELETE: api/Computers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteComputer(int id)
        //{
        //    var computer = await _context.Computers.FindAsync(id);
        //    if (computer == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Computers.Remove(computer);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ComputerExists(int id)
        //{
        //    return _context.Computers.Any(e => e.Id == id);
        //}
    }
}
