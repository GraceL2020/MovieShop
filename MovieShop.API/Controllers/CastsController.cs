using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastsController : ControllerBase
    {
        private readonly ICastService _castService;
        public CastsController(ICastService castService)
        {
            _castService = castService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCastById(int id)
        {
            var cast = await _castService.GetCastById(id);
            return Ok(cast);
        }

        [HttpGet]
        [Route("{id}/movies")]
        public async Task<IActionResult> GetMoviesForCast(int id)
        {
            var movies = await _castService.GetMoviesForCast(id);
            return Ok(movies);
        }

    }
}