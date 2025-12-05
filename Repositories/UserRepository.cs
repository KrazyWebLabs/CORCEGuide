using CORCEGuideApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORCEGuideApp.Repositories
{
    public class UserRepository(DataContext dataContext)
    {
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await dataContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await dataContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task AddUserAsync(User user)
        {
            await dataContext.Users.AddAsync(user);
            await dataContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await dataContext.Users
                .FirstOrDefaultAsync(u => u.UserId == user.UserId);

            if ( existingUser == null )
            {
                await Shell.Current.DisplayAlert("Error", "User not found.", "OK");
                return;
            }

            existingUser.UserName = user.UserName;
            existingUser.ImagePath = user.ImagePath;
            existingUser.Role = user.Role;

            dataContext.Users.Update(existingUser);
            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await dataContext.Users.FindAsync(userId);

            if ( user == null )
            {
                await Shell.Current.DisplayAlert("Error", "User not found.", "OK");
                return;
            }

            user.IsActive = false;

            dataContext.Users.Update(user);
            await dataContext.SaveChangesAsync();
        }
    }
}
