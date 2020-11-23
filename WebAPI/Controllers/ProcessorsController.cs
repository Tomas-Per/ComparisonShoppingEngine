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
    public class ProcessorsController : ControllerBase
    {
        private readonly ProcessorDataService _service;

        public ProcessorsController(IDataComponent<Processor> service)
        {
            _service = (ProcessorDataService)service;
        }

        // GET: api/Processors
        [HttpGet]
        public IEnumerable<Processor> GetProcessors() => _service.ReadData();

        [HttpGet("{id}")]
        public Processor GetProcessor(int id) => _service.GetDataByID(id);

        // PUT: api/Processors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public void PutProcessor(Processor processor) => _service.UpdateData(processor);

    }
}
