using DataContent.DAL.Interfaces;
using DataContent.DAL.UserExceptions;
using Microsoft.EntityFrameworkCore;
using ModelLibrary;
using ModelLibrary.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrypt;
using DataContent.DAL.Helpers;

namespace DataContent.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }
        public async Task<User> DeleteUserAsync(int id)
        {
            var user =await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<List<User>> GetUsersByFavoriteItemIdAsync(int itemId)
        {
            var items = await _context.FavoriteItems.Include(i => i.Item)
                .Include(u => u.User)
                .Where(x => x.Item.Id == itemId).ToListAsync();

            List<User> users = new List<User>();
            items.ForEach(item => users.Add(item.User));
            return users;
        }


        public async Task<User> LoginAsync(string email, string password)
        {
            var validUser = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            if(validUser == null)
            {
                throw new LoginException("Email is not registered");
            }
            ScryptEncoder encoder = new ScryptEncoder();
            bool isValidUser = encoder.Compare(password, validUser.Password);
            if (!isValidUser)
            {
                throw new LoginException("Password for this user is incorrect");
            }
            return validUser;
        }

        public async Task<User> RegisterAsync(User user)
        {
            //check for email in db
            var registeredUser = await _context.Users.Where(x => x.Email == user.Email).FirstOrDefaultAsync();
            if(registeredUser != null)
            {
                throw new RegisterException("Email is already registered");
            }
            //check for username in db
            registeredUser = await _context.Users.Where(x => x.Username == user.Username).FirstOrDefaultAsync();
            if(registeredUser != null)
            {
                throw new RegisterException("Username is already used");
            }

            //register new user with hashed password
            ScryptEncoder encoder = new ScryptEncoder();
            registeredUser = new User();
            registeredUser.Email = user.Email;
            registeredUser.Username = user.Username;
            registeredUser.Password = encoder.Encode(user.Password);

            _context.Users.Add(registeredUser);
            await _context.SaveChangesAsync();
            return registeredUser;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var updatedUser = await _context.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            if(updatedUser != null)
            {
                updatedUser.Email = user.Email;
                updatedUser.Username = user.Username;
                updatedUser.Password = new ScryptEncoder().Encode(user.Password);
                updatedUser.RecoveryPassword = null;
            }
            await _context.SaveChangesAsync();
            return updatedUser;
        }

        public async Task<User> ForgotPasswordAsync(string email)
        {
            var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user.RecoveryDate.AddSeconds(30) < DateTime.Now)
            {
                user.RecoveryPassword = PasswordHelper.RandomString(16);
                user.RecoveryDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            else throw new RecoveryTimeException("30 seconds have not passed since last recovery code was sent");
            
            return user;
        }
    }
}
