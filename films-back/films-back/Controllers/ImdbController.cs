using BLL.Interface;
using BLL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace films_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImdbController : Controller
    {
        private readonly IImdbService _imdbService;

        public ImdbController(IImdbService imdbService)
        {
            _imdbService = imdbService;
        }

        [HttpGet("{filmId}/{userId}")]
        public async Task<IActionResult> GetImdbByFilmIdAndUserId([FromRoute] int filmId, [FromRoute] int userId)
        {
            try
            {
                var imdb=await _imdbService.GetImdbAsync(filmId, userId);
                if (imdb == null)
                    return NotFound();
                return Ok(imdb);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost("")]
        public async Task<IActionResult> AddImdb([FromBody] ImdbModel imdbModel)
        {
            try
            {
                var imdb=await _imdbService.AddImdbAsync(imdbModel);
                if (imdb == null) 
                    return BadRequest();
                return Ok(imdb);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> EditImdb([FromBody] ImdbModel imdbModel)
        {
            try
            {
                var imdb = await _imdbService.ChangeImdbAsync(imdbModel);
                if (imdb == null)
                    return BadRequest();
                return Ok(imdb);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpDelete("")]
        public async Task<IActionResult> RemoveImdb([FromBody] ImdbModel imdbModel)
        {
            try
            {
                var imdb = await _imdbService.RemoveImdbAsync(imdbModel);
                if (imdb == null)
                    return BadRequest();
                return NoContent();
            }
            catch (System.Exception)
            {

                throw;
            }
        }


    }
}
