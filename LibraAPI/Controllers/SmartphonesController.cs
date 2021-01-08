using DataContent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLibrary;
using ModelLibrary.DataContexts;

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

        /// <summary>
        /// Gets all smartphones from the database (page 0 for all, 1 to n for chunks of 20)
        /// </summary>
        [HttpGet("Page/{page}")]
        public async Task<ActionResult<List<Smartphone>>> GetSmartphones(int page)
        {
            return await _repository.GetAllSmartphonesAsync(page);
        }

        /// <summary>
        /// Gets a specific smartphone by its ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Smartphone>> GetSmartphone(int id)
        {
            var smartphone = await _repository.GetSmartphoneByIdAsync(id);

            if (smartphone == null)
            {
                return NotFound();
            }

            return smartphone;
        }

        /// <summary>
        /// Updates a specific smartphone
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> PutSmartphone(Smartphone smartphone)
        {
            if (smartphone == null)
            {
                return BadRequest();
            }

            await _repository.UpdateSmartphoneAsync(smartphone);

            return NoContent();
        }

        /// <summary>
        /// Adds a smartphone to the database
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<List<Smartphone>>> PostSmartphone(List<Smartphone> smartphones)
        {
            await _repository.AddSmartphonesAsync(smartphones);

            return Ok(smartphones);
        }

        /// <summary>
        /// Deletes a smartphone from the database by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmartphone(int id)
        {
            var smartphone = await _repository.DeleteSmartphoneAsync(id);
            if (smartphone == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Finds similar smartphones to a given smartphone
        /// </summary>
        [HttpPost("FindSimilar")]
        public async Task<ActionResult<List<Smartphone>>> FindSimilarSmartphones(Smartphone smartphone)
        {
            var smartphones = await _repository.FindSimilarAsync(smartphone);

            return Ok(smartphones);
        }
    }
}
