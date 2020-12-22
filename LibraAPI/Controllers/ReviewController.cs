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

        // GET: api/Review
        [HttpGet("/Items/{itemId}/{itemCategory}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews(int itemId, int itemCategory)
        {
            var reviews = await _repository.GetItemReviewsAsync(itemId, itemCategory);
            return reviews;
        }

        // GET: api/Review/5
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

        [HttpGet("Reviews/User/{id}")]
        public async Task<ActionResult<List<Review>>> GetUserReviews(int id)
        {
            var reviews = await _repository.GetReviewsByUserIdAsync(id);
            return reviews;
        }

        // PUT: api/Review/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/Review
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            await _repository.AddReviewAsync(review);

            return Ok(review);
        }

        // DELETE: api/Review/5
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
