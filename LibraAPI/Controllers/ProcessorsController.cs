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
    public class ProcessorsController : ControllerBase
    {
        private readonly IProcessorRepository _repository;

        public ProcessorsController(IProcessorRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Processors
        [HttpGet]
        public async Task<ActionResult<List<Processor>>> GetProcessors()
        {
            return await _repository.GetAllProcessorsAsync();
        }

        // GET: api/Processors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Processor>> GetProcessor(int id)
        {
            var processor = await _repository.GetProcessorByIdAsync(id);

            if (processor == null)
            {
                return NotFound();
            }

            return processor;
        }

        // PUT: api/Processors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutProcessor(Processor processor)
        {
            if (processor == null)
            {
                return BadRequest();
            }

            await _repository.UpdateProcessorAsync(processor);

            return NoContent();
        }

        //// POST: api/Processors
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Processor>> PostProcessor(Processor processor)
        //{
        //    _context.Processors.Add(processor);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetProcessor", new { id = processor.Id }, processor);
        //}

        //// DELETE: api/Processors/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProcessor(int id)
        //{
        //    var processor = await _context.Processors.FindAsync(id);
        //    if (processor == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Processors.Remove(processor);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ProcessorExists(int id)
        //{
        //    return _context.Processors.Any(e => e.Id == id);
        //}
    }
}
