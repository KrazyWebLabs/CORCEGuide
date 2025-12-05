using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CORCEGuideApp.Helpers;
using CORCEGuideApp.Models;
using CORCEGuideApp.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORCEGuideApp.ViewModels
{
    public partial class AnnouncementViewModel : ObservableObject
    {
        private readonly AnnouncementRepository _announcementRepository;
        private readonly CategoryRepositoy _categoryRepository;
        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _content;

        [ObservableProperty]
        private string? _imagePath;

        [ObservableProperty]
        private DateTime _datePosted = DateTime.Now;

        [ObservableProperty]
        private Category? _selectedCategory;

        [ObservableProperty]
        private Announcement? _selectedAnnouncement;

        [ObservableProperty]
        private bool _isEnabled = false;

        public ObservableCollection<Announcement> Announcements { get; set; } = [];
        public ObservableCollection<Category> Categories { get; set; } = [];

        public AnnouncementViewModel(AnnouncementRepository announcementRepository,
    CategoryRepositoy categoryRepository)
        {
            _announcementRepository = announcementRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task LoadAnnouncementsAsync()
        {
            Announcements.Clear();
            var announcements = await _announcementRepository.GetAllActiveAnnouncementsAsync();
            foreach ( var announcement in announcements )
            {
                Announcements.Add(announcement);
            }
        }

        public async Task LoadCategoriesAsync()
        {
            Categories.Clear();
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            foreach ( var category in categories )
            {
                Categories.Add(category);
            }

            Debug.WriteLine($"Loaded categories: {categories.Count}");

        }

        [RelayCommand]
        public async Task SelectImageAsync()
        {
            var fileResult = await MediaPicker.PickPhotoAsync();

            if ( fileResult == null )
                return;

            string? localPath = await ImageHelper.SaveImageLocaclyAsync(fileResult);

            if ( localPath == null )
                return;

            ImagePath = localPath;
        }

        [RelayCommand]
        public async Task AddAnnouncementAsync()
        {
            if ( string.IsNullOrWhiteSpace(Title) )
            {
                await Shell.Current.DisplayAlert("Validation Error", "Announcement title is required.", "OK");
                return;
            }

            if ( SelectedCategory == null )
            {
                await Shell.Current.DisplayAlert("Validation Error", "Please select a category.", "OK");
                return;
            }

            var newAnnouncement = new Announcement
            {
                Title = Title,
                Content = Content,
                ImagePath = ImagePath,
                DatePosted = DatePosted,
                CategoryId = SelectedCategory.CategoryId,
                IsActive = true
            };
            await _announcementRepository.AddAnnouncementAsync(newAnnouncement);

            await ToastHelper.GetToastAsync("Announcement added successfully.", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);

            Debug.WriteLine($"Category assigned: {SelectedCategory.Name}");
        }
    }
}
