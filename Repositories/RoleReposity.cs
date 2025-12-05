using CORCEGuideApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORCEGuideApp.Repositories
{
    public class RoleReposity(DataContext dataContext)
    {
        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await dataContext.Roles.AsNoTracking().ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int roleId)
        {
            return await dataContext.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RoleId == roleId);
        }

        public async Task AddRoleAsync(Role role)
        {
            await dataContext.Roles.AddAsync(role);
            await dataContext.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(Role role)
        {
            var existingRole = await dataContext.Roles
                .FirstOrDefaultAsync(r => r.RoleId == role.RoleId);

            if ( existingRole == null )
            {
                await Shell.Current.DisplayAlert("Error", "Role not found.", "OK");
                return;
            }

            existingRole.Name = role.Name;
            
            dataContext.Roles.Update(existingRole);
            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await dataContext.Roles.FindAsync(roleId);

            if ( role == null )
            {
                await Shell.Current.DisplayAlert("Error", "Role not found.", "OK");
                return;
            }

            role.IsActive = false;
            
            dataContext.Roles.Update(role);
            await dataContext.SaveChangesAsync();
        }
    }
}
