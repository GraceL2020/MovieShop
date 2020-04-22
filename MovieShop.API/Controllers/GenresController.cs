using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetAllGenres()
        {
            return Ok("test data");
        }

        [HttpGet]
        [Route("test")]
        public IActionResult GetTest()
        {
            return Ok("test data");
        }

    }
}