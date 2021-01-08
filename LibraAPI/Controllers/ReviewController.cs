using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContent.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLibrary;
using ModelLibrary.DataContexts;

namespace LibraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _repository;

        public ReviewController(IReviewRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets an item's reviews by its ID and category
        /// </summary>
        [HttpGet("/Items/{itemId}/{itemCategory}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews(int itemId, int itemCategory)
        {
            var reviews = await _repository.GetItemReviewsAsync(itemId, itemCategory);
            return reviews;
        }

        /// <summary>
        /// Gets a specific review by its ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _repository.GetReviewByIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        /// <summary>
        /// Gets all reviews made by a user by the user's ID
        /// </summary>
        [HttpGet("Reviews/User/{id}")]
        public async Task<ActionResult<List<Review>>> GetUserReviews(int id)
        {
            var reviews = await _repository.GetReviewsByUserIdAsync(id);
            return reviews;
        }

        /// <summary>
        /// Updates a review by its ID
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            var newReview = await _repository.UpdateReviewAsync(review);

            return NoContent();
        }

        /// <summary>
        /// Posts a review related to an item
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            await _repository.AddReviewAsync(review);

            return Ok(review);
        }

        /// <summary>
        /// Deletes review by its ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _repository.DeleteReviewAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
