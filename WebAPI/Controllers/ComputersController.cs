﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItemLibrary;
using ItemLibrary.DataContexts;
using DataContent;
using DataContent.ReadingDB.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private readonly ComputerDataService _service;

        public ComputersController(IDataItem<Computer> service)
        {
            _service = (ComputerDataService)service;
        }

        // GET: api/Computers
        [HttpGet]
        public IEnumerable<Computer> GetComputers() => _service.ReadData();

        // GET: api/Computers/5
        [HttpGet("{id}")]
        public Computer GetComputer(int id) => _service.GetDataByID(id);


        //// PUT: api/Computers/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutComputer(int id, Computer computer)
        //{
        //    if (id != computer.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(computer).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ComputerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Computers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public void PostComputer(List<Computer> computer)
        {
            _service.WriteData(computer);

        }

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
