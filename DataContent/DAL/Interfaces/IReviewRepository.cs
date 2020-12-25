using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;

namespace DataContent.DAL.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetItemReviewsAsync(int itemId, int itemCategory);
        Task<Review> AddReviewAsync(Review review);
        Task<List<Review>> GetReviewsByUserIdAsync(int userId);
        Task<Review> GetReviewByIdAsync(int id);
        Task<Review> UpdateReviewAsync(Review review);
        Task<Review> DeleteReviewAsync(int id);
    }
}
