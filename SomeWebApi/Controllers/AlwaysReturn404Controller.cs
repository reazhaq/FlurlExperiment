using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SomeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlwaysReturn404Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSomething()
        {
            return NotFound("GET - can't find it");
        }

        [HttpPut]
        public IActionResult PutSomething()
        {
            return NotFound("PUT - can't find it");
        }

        [HttpPost]
        public IActionResult PostSomething()
        {
            return NotFound("POST - can't find it");
        }

        [HttpDelete]
        public IActionResult DeleteSomething()
        {
            return NotFound("DELETE - can't find it");
        }
    }
}
