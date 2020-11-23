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

    }
}
