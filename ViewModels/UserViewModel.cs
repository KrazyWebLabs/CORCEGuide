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
    public partial class UserViewModel : ObservableObject
    {
        private readonly UserRepository _userRepository;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string? _imagePath;

        [ObservableProperty]
        private bool _isActive;

        [ObservableProperty]
        private Role? _role;

        // Properties for Collection View binding
        [ObservableProperty]
        private User? _selectedUser;

        [ObservableProperty]
        private bool _isEnabled = false;

        public ObservableCollection<User> Users { get; set;  } = [];

        public UserViewModel(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task LoadUsersAsync()
        {
            Users.Clear();
            var users = await _userRepository.GetAllUsersAsync();
            
            foreach ( var user in users )
            {
                Users.Add(user);
            }
        }

        [RelayCommand]
        public async Task SelectImageAsync()
        {
            var fileResult = await MediaPicker.PickPhotoAsync();

            if ( fileResult == null)
                return;

            string? localPath = await ImageHelper.SaveImageLocaclyAsync(fileResult);

            if ( localPath == null )
                return;

            ImagePath = localPath;
        }

        [RelayCommand]
        public async Task AddUserAsync()
        {
            if ( string.IsNullOrWhiteSpace(Username) )
            {
                await Shell.Current.DisplayAlert("Validation Error", "User name is required.", "OK");
                return;
            }

            var newUser = new User
            {
                UserName = Username,
                ImagePath = ImagePath,
                Role = Role,
                IsActive = IsActive
            };

            await _userRepository.AddUserAsync(newUser);

            await ToastHelper.GetToastAsync("User added successfully.", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
        }
    }
}
