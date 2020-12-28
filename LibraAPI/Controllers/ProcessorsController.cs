using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLibrary;
using ModelLibrary.DataContexts;
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

        /// <summary>
        /// Gets all processors from the database
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Processor>>> GetProcessors()
        {
            return await _repository.GetAllProcessorsAsync();
        }

        /// <summary>
        /// Gets a specific processor from the database by ID
        /// </summary>
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

        /// <summary>
        /// Gets a specific processor by its model
        /// </summary>
        [HttpGet("Models/{model}")]
        public async Task<ActionResult<Processor>> GetProcessorByModel(string model)
        {
            var processor = await _repository.GetProcessorByNameAsync(model);

            if (processor == null)
            {
                return NotFound();
            }

            return processor;
        }

        /// <summary>
        /// Updates a specific processor
        /// </summary>
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

        /// <summary>
        /// Adds a processor to the database
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<List<Processor>>> PostProcessor(List<Processor> processors)
        {
            await _repository.AddProcessorsAsync(processors);

            return Ok(processors);
        }

        /// <summary>
        /// Deletes a processor from the database by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcessor(int id)
        {
            var processor = await _repository.DeleteProcessorAsync(id);
            if (processor == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
