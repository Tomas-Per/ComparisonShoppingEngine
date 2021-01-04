using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using DataContent.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelLibrary;
using ModelLibrary.DataContexts;

namespace DataContent.DAL.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly UserContext _context;

        public ReviewRepository(UserContext context)
        {
            _context = context;
        }
        
        public async Task<Review> AddReviewAsync(Review review)
        {
            review.User = _context.Users.Find(review.User.Id);
            review.Item = _context.Items.Find(review.Item.Id, review.Item.ItemCategory);
            _context.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review> DeleteReviewAsync(int id)
        {
            var review =await _context.Reviews.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<List<Review>> GetItemReviewsAsync(int itemId, int itemCategory)
        {
            var reviews = await _context.Reviews.Include(i => i.Item).Include(i => i.User)
                                                            .Where(x => x.Item.Id == itemId
                                                            && (int) x.Item.ItemCategory == itemCategory).ToListAsync();
            return reviews;
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            var review = await _context.Reviews.Include(i => i.Item).Include(i => i.User).Where(x => x.Id == id).FirstOrDefaultAsync();
            return review;
        }

        public async Task<List<Review>> GetReviewsByUserIdAsync(int userId)
        {
            var reviews = await _context.Reviews.Include(i => i.Item).Include(i => i.User)
                                                        .Where(x => x.User.Id == userId).ToListAsync();
            return reviews;
        }

        public async Task<Review> UpdateReviewAsync(Review review)
        {
            var reviewInDb = await _context.Reviews.Where(x => x.Id == review.Id).FirstOrDefaultAsync();
            if (reviewInDb != null)
            {
                reviewInDb.Message = review.Message;
                reviewInDb.Score = review.Score;
            }

            await _context.SaveChangesAsync();

            return reviewInDb;
        }
    }
}
