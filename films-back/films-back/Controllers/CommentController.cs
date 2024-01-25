using BLL.Interface;
using BLL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Delivery_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        //all
        [HttpGet("")]
        public async Task<IActionResult> GetComments()
        {
            try
            {
                var comments = await _commentService.GetComments();
                return Ok(comments);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        //one by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComments([FromRoute] int id)
        {
            try
            {
                var comment = await _commentService.GetComment(id);
                if (comment == null)
                    Ok(null);
                return Ok(comment);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        //add
        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> PostComment([FromBody] CommentModel commentModel)
        {
            try
            {
                var comment = await _commentService.AddComment(commentModel);
                if (comment == null)
                    Ok(null);
                return Ok(comment);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        // Get list by filter
        [HttpPost("filter")]
        public async Task<IActionResult> GetCommentsByFilter([FromBody] CommentModel commentModel)
        {
            try
            {
                var comment = await _commentService.GetCommentsByFilter(commentModel);
                if (comment == null)
                    Ok(null);
                return Ok(comment);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        // put
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComments(int id, [FromBody] CommentModel commentModel)
        {
            try
            {
                var comment = await _commentService.UpdateComment(id, commentModel);
                if (comment == null)
                    Ok(null);
                return Ok(comment);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        // patch
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatchComments(int id, [FromBody] JsonPatchDocument commentPatch)
        {
            try
            {
                var comment = await _commentService.UpdatePatchComment(id, commentPatch);
                if (comment == null)
                    Ok(null);
                return Ok(comment);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        //delete
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> UpdateComments(int id)
        {
            try
            {
                var result = await _commentService.DeleteComment(id);
                if (result == false)
                    Ok(null);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
