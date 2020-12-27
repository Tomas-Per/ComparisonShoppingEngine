using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;

namespace DataContent.DAL.Interfaces
{
    public interface IFavoriteItemRepository
    {
        Task<List<FavoriteItem>> GetUserFavoriteItemsAsync(int userId);
        Task<FavoriteItem> AddFavoriteItemToUser(FavoriteItem favoriteItem);
        Task<FavoriteItem> DeleteFavoriteItem(int id);
        Task<FavoriteItem> GetFavoriteItemById(int id);

    }
}
