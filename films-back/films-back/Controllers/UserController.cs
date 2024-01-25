using BLL.Interface;
using BLL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Delivery_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsers();
                return Ok(users);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByAccountId([FromRoute] string id)
        {
            try
            {
                var user=await _userService.GetUserByAccountId(id);
                if(user == null)
                    return NotFound();
                return Ok(user);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost("")]
        public async Task<IActionResult> AddUser([FromBody] UserModel userModel)
        {
            try
            {
                var user = await _userService.AddUser(userModel);
                if(user == null)
                    return BadRequest();
                return Ok(user);

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetUserByFilter([FromBody] UserModel userModel)
        {
            try
            {
                var workers=await _userService.GetFilterUsers(userModel);
                if (workers == null)
                    return Ok("bad req");

                return Ok(workers);
            }
            catch (System.Exception)
            {
                //return BadRequest();
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserModel userModel)
        {
            try
            {
                var updateUser = await _userService.UpdateUser(id, userModel); 
                return Ok(updateUser);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("token")]
        [Authorize]
        public async Task<IActionResult> tok()
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
