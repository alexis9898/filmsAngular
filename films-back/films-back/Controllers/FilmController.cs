using BLL.Interface;
using BLL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace films_data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService ;
        private readonly HttpClient _http;

        public FilmController(IFilmService filmService, HttpClient http)
        {
            _filmService = filmService;
            _http = http;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetImages()
        {
            try
            {
                var films= await _filmService.GetFilmsAsync();
                return Ok(films);
            }
            catch (Exception)
            {
                throw;
            }
        }

      
        [HttpGet("by-catrgoryId/{id}")]
        public async Task<IActionResult> GetFimsByCategoryId([FromRoute] int id)
        {
            try
            {
                var films = await _filmService.GetFilmsByCregoryIdAsync(id);
                if (films == null)
                {
                    return NotFound();
                }
                return Ok(films);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetFimsByName([FromRoute] string name)
        {
            try
            {
                var films = await _filmService.GetFilmByNameAsync(name);
                if (films == null)
                {
                    return NotFound();
                }
                return Ok(films);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneImage([FromRoute] int id)
        {
            try
            {
                var film = await _filmService.GetFilmAsync(id);
                if (film == null)
                    return NotFound();
                return Ok(film);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[Authorize(Roles ="Admin")]
        [HttpPost("")]
        public async Task<IActionResult> AddNewFilm([FromBody] FilmModel filmModel)
        {
            try
            {
                var newFilm = await _filmService.AddFilmAsync(filmModel);
                return Ok(newFilm);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFilm([FromRoute] int id, [FromBody] FilmModel filmModel)
        {
            try
            {
                var updateFilm= await _filmService.UpdateFilmAsync(id, filmModel);
                if (updateFilm == null)
                    return NotFound();
                return Ok(updateFilm);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> updateFilmPatch([FromRoute] int id, [FromBody] JsonPatchDocument imagePatch)
        {
            try
            {
                var updateFilm = await _filmService.UpdatePatchFilmAsync(id, imagePatch);
                if (updateFilm == null)
                    return NotFound();
                return Ok(updateFilm);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var resault = await _filmService.DeleteFilmAsync(id);
                if (resault)
                    return NoContent();
                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
