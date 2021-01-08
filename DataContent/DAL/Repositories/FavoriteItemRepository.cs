using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContent.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelLibrary;
using ModelLibrary.DataContexts;
using Newtonsoft.Json;

namespace DataContent.DAL.Repositories
{
    public class FavoriteItemRepository : IFavoriteItemRepository
    {
        private readonly UserContext _context;

        public FavoriteItemRepository(UserContext context)
        {
            _context = context;
        }
        public async Task<FavoriteItem> AddFavoriteItemToUser(FavoriteItem favoriteItem)
        {
            _context.Add(favoriteItem);
            await _context.SaveChangesAsync();
            return favoriteItem;
        }

        public async Task<FavoriteItem> DeleteFavoriteItem(int id)
        {
            var item = await _context.FavoriteItems.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<FavoriteItem> GetFavoriteItemById(int id)
        {
            var item = await _context.FavoriteItems.Include(i => i.Item)
                .Include(u => u.User)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            return item;
        }

        public async Task<List<FavoriteItem>> GetUserFavoriteItemsAsync(int userId)
        {
            var items = await _context.FavoriteItems.Include(i => i.Item)
                                                                    .Include(u => u.User)
                                                                     .Where(x => x.User.Id == userId).ToListAsync();
            return items;
        }
    }
}
