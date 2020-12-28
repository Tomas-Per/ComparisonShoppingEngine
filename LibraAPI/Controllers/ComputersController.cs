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
using static ModelLibrary.Categories;

namespace LibraAPI.Controllers
{
    //localhost:port/api/computers
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private readonly IComputerRepository _repository;

        public ComputersController(IComputerRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all desktop computers from database
        /// </summary>
        [HttpGet("Desktops")]
        public async Task<ActionResult<List<Computer>>> GetDesktops()
        {
            var computers = await _repository.GetAllComputersAsync(ItemCategory.DesktopComputer);
            return computers;
        }

        /// <summary>
        /// Gets all laptop computers from database
        /// </summary>
        [HttpGet("Laptops")]
        public async Task<ActionResult<List<Computer>>> GetLaptops()
        {
            var computers = await _repository.GetAllComputersAsync(ItemCategory.Laptop);
            return computers;
        }

        /// <summary>
        /// Gets a specific computer from database by ID
        /// </summary>
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

        /// <summary>
        /// Updates a specific computer
        /// </summary>
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

        /// <summary>
        /// Adds a computer to the database
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<List<Computer>>> PostComputer(List<Computer> computers)
        {
            await _repository.AddComputersAsync(computers);

            return Ok(computers);
        }

        /// <summary>
        /// Deletes a computer from the database by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputer(int id)
        {
            var computer = await _repository.DeleteComputerAsync(id);
            if (computer == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
