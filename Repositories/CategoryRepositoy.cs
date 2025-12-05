using CORCEGuideApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORCEGuideApp.Repositories
{
    public class CategoryRepositoy(DataContext dataContext)
    {
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await dataContext.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await dataContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await dataContext.Categories.AddAsync(category);
            await dataContext.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var existingCategory = await dataContext.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId);

            if ( existingCategory == null )
            {
                await Shell.Current.DisplayAlert("Error", "Category not found.", "OK");
                return;
            }

            existingCategory.Name = category.Name;

            dataContext.Categories.Update(existingCategory);
            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await dataContext.Categories.FindAsync(categoryId);

            if ( category == null )
            {
                await Shell.Current.DisplayAlert("Error", "Category not found.", "OK");
                return;
            }

            category.IsActive = false;

            dataContext.Categories.Update(category);
            await dataContext.SaveChangesAsync();
        }
    }
}
