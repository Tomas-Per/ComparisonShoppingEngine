using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContent.DAL.Interfaces;
using DataContent.DAL.Repositories;
using DataContent.DAL.UserExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLibrary;
using ModelLibrary.DataContexts;

namespace LibraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(UserRepository repository)
        {
            _repository = repository;
        }

        // GET: api/User
        [HttpGet("/login/{email}/{password}")]
        public async Task<ActionResult<User>> Login(string email, string password)
        {
            User user;
            try
            {
                user = await _repository.LoginAsync(email, password);
            }
            catch(UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(WrongUserPasswordException e)
            {
                return NotFound(e.Message);
            }
            return user;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            await _repository.UpdateUserAsync(user);

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> Register(User user)
        {

            try
            {
                await _repository.RegisterAsync(user);
            }
            catch (EmailAlreadyRegisteredException e)
            {
                return NotFound(e.Message);
            }
            catch (UsernameAlreadyRegisteredException e)
            {
                return NotFound(e.Message);
            }

            return Ok(user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _repository.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
