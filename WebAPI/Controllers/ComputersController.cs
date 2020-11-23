using System;
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


        // PUT: api/Computers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public void PutComputer(Computer computer)
        {
            _service.UpdateData(computer);
        }

        // POST: api/Computers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public void PostComputer(List<Computer> computer)
        {
            _service.WriteData(computer);
        }

        // DELETE: api/Computers/5
        [HttpDelete("{id}")]
        public void DeleteComputer(int id)
        {
            _service.DeleteData(id);
        }

        //private bool ComputerExists(int id)
        //{
        //    return _context.Computers.Any(e => e.Id == id);
        //}
    }
}
