using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContent.DAL.Interfaces;
using DataContent.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLibrary;
using ModelLibrary.DataContexts;

namespace LibraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteItemController : ControllerBase
    {
        private readonly IFavoriteItemRepository _repository;

        public FavoriteItemController(IFavoriteItemRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Gets a user's favorite item list by the user's ID
        /// </summary>
        [HttpGet("/UserFavorites/{userId}")]
        public async Task<ActionResult<IEnumerable<FavoriteItem>>> GetFavoriteItems(int userId)
        {
            return await _repository.GetUserFavoriteItemsAsync(userId);
        }

        /// <summary>
        /// Gets a specific favorite item by its ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteItem>> GetFavoriteItem(int id)
        {
            var favoriteItem = await _repository.GetFavoriteItemById(id);

            if (favoriteItem == null)
            {
                return NotFound();
            }

            return favoriteItem;
        }

        /// <summary>
        /// Adds a favorite item to the user's favorite item list
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<FavoriteItem>> PostFavoriteItem(FavoriteItem favoriteItem)
        {
            await _repository.AddFavoriteItemToUser(favoriteItem);

            return Ok(favoriteItem);
        }

        /// <summary>
        /// Deletes a specific favorite item
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteItem(int id)
        {
            var favoriteItem = await _repository.DeleteFavoriteItem(id);
            if (favoriteItem == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
