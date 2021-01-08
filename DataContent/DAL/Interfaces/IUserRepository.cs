using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContent.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<User> RegisterAsync(User user);
        Task<User> LoginAsync(string email, string password);
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetUsersByFavoriteItemIdAsync(int itemId);
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(int id);
        Task<User> ForgotPasswordAsync(string email);

    }
}
