using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truelayer.Models;
using Truelayer.Services;
using Truelayer.Utils;
using static Truelayer.Utils.Generic;

namespace Truelayer.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string name)
        {
            try
            {
                Wrapper<Pokemon> result = new PokemonServices().GetPokemon(name,false);
                if (result.Status == Consts.SUCCESS)
                    return Ok(result.SingleObject);
                else
                    return BadRequest(result.Message);
            }
            catch
            {
                return BadRequest(Consts.GENERIC_ERROR);
            }
        }

        [HttpGet]
        public IActionResult GetWithTranslation(string name)
        {
            try
            {
                Wrapper<Pokemon> result = new PokemonServices().GetPokemon(name, true);
                if (result.Status == Consts.SUCCESS)
                    return Ok(result.SingleObject);
                else
                    return BadRequest(result.Message);
            }
            catch
            {
                return BadRequest(Consts.GENERIC_ERROR);
            }
        }
    }
}
