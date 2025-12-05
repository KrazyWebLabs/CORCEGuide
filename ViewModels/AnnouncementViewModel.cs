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
    public partial class AnnouncementViewModel : ObservableObject
    {
        private readonly AnnouncementRepository _announcementRepository;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _content;

        [ObservableProperty]
        private string? _imagePath;

        [ObservableProperty]
        private DateTime _datePosted = DateTime.Now;

        [ObservableProperty]
        private Category? _category;

        [ObservableProperty]
        private Announcement? _selectedAnnouncement;

        [ObservableProperty]
        private bool _isEnabled = false;

        public ObservableCollection<Announcement> Announcements { get; set; } = [];

        public AnnouncementViewModel(AnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
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

        [RelayCommand]
        public async Task AddAnnouncementAsync()
        {
            if ( string.IsNullOrWhiteSpace(Title) )
            {
                await Shell.Current.DisplayAlert("Validation Error", "Announcement title is required.", "OK");
                return;
            }
            var newAnnouncement = new Announcement
            {
                Title = Title,
                Content = Content,
                ImagePath = ImagePath,
                DatePosted = DatePosted,
                Category = Category,
                IsActive = true
            };
            await _announcementRepository.AddAnnouncementAsync(newAnnouncement);

            await ToastHelper.GetToastAsync("Announcement added successfully.", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
        }
    }
}
