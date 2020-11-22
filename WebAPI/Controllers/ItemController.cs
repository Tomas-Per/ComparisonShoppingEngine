using DataContent;
using ItemLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController<T> : ControllerBase where T : Item
    {
        private readonly IDataItem<T> _service;

        public ItemController(IDataItem<T> service)
        {
            _service = service;
        }

        // GET: api/<ItemController>
        [HttpGet]
        public IEnumerable<T> Get() => _service.ReadData();


        // GET api/<ItemController>/5
        [HttpGet("{id}")]
        public T Get(int id) => _service.GetDataByID(id);


        // POST api/<ItemController>
        [HttpPost]
        public void Post([FromBody] IEnumerable<T> values) => _service.WriteData(values);

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] T value) => _service.UpdateData(value);

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _service.DeleteData(id);
    }
}
