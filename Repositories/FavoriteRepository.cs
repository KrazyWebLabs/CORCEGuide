using CORCEGuideApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORCEGuideApp.Repositories
{
    public class FavoriteRepository(DataContext dataContext)
    {
        public async Task<List<Favorite>> GetAllFavorites()
        {
            return await dataContext.Favorites.AsNoTracking().ToListAsync();
        }

        public async Task<Favorite?> GetFavoriteByIdAsync(int favoriteId)
        {
            return await dataContext.Favorites
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.FavoriteId == favoriteId);
        }

        public async Task AddFavoriteAsync(Favorite favorite)
        {
            dataContext.Favorites.Add(favorite);
            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteFavoriteAsync(int favoriteId)
        {
            var favorite = await dataContext.Favorites.FindAsync(favoriteId);
            
            if ( favorite == null )
            {
                await Shell.Current.DisplayAlert("Error", "Favorite not found.", "OK");
                return;
            }

            dataContext.Favorites.Remove(favorite);
            
            await dataContext.SaveChangesAsync();
        }
    }
}
