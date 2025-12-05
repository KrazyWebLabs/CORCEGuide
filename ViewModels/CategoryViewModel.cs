using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CORCEGuideApp.Helpers;
using CORCEGuideApp.Models;
using CORCEGuideApp.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORCEGuideApp.ViewModels
{
    public partial class CategoryViewModel : ObservableObject
    {
        private readonly CategoryRepositoy _categoryRepository;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private bool _isActive;

        [ObservableProperty]
        private Category? _selectedCategory;

        [ObservableProperty]
        private bool _isEnabled = false;

        public ObservableCollection<Category> Categories { get; set; } = [];

        public CategoryViewModel(CategoryRepositoy categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task LoadCategoriesAsync()
        {
            Categories.Clear();

            var categories = await _categoryRepository.GetAllCategoriesAsync();
            foreach ( var category in categories )
            {
                Categories.Add(category);
            }
        }

        [RelayCommand]
        public async Task AddCategoryAsync()
        {
            if ( string.IsNullOrWhiteSpace(Name) )
            {
                await Shell.Current.DisplayAlert("Validation Error", "Category name is required.", "OK");
                return;
            }

            var newCategory = new Category
            {
                Name = Name,
                IsActive = IsActive
            };
            
            await _categoryRepository.AddCategoryAsync(newCategory);

            await ToastHelper.GetToastAsync("Category added successfully.", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
        }
    }
}
