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
        public async Task<ActionResult<List<Smartphone>>> GetSmartphones()
        {
            return await _repository.GetAllSmartphonesAsync();
        }

        // GET: api/Smartphones/5
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

        // PUT: api/Smartphones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmartphone(Smartphone smartphone)
        {
            if (smartphone == null)
            {
                return BadRequest();
            }

            await _repository.UpdateSmartphoneAsync(smartphone);

            return NoContent();
        }

        // POST: api/Smartphones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List<Smartphone>>> PostSmartphone(List<Smartphone> smartphones)
        {
            await _repository.AddSmartphonesAsync(smartphones);

            return Ok(smartphones);
        }

        // DELETE: api/Smartphones/5
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
    }
}
