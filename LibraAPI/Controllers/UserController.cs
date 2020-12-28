using System;
using System.Collections.Generic;
using System.IO;
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
using Newtonsoft.Json;

namespace LibraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Checks a user's login credentials
        /// </summary>
        [HttpGet("/login/{email}/{password}")]
        public async Task<ActionResult<User>> Login(string email, string password)
        {
            User user;
            try
            {
                user = await _repository.LoginAsync(email, password);
            }
            catch(LoginException e)
            {
                return NotFound(e.Message);
            }
            return user;
        }

        /// <summary>
        /// Gets a specific user by ID
        /// </summary>
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

        /// <summary>
        /// Updates a user's information
        /// </summary>
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

        /// <summary>
        /// Registers a user and adds user information to the database 
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<User>> Register(User user)
        {

            try
            {
                await _repository.RegisterAsync(user);
            }
            catch (RegisterException e)
            {
                return NotFound(e.Message);
            }

            return Ok(user);
        }

        /// <summary>
        /// Deletes a user by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _repository.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
