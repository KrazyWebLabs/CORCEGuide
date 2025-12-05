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
    public partial class RoleViewModel : ObservableObject
    {
        private readonly RoleReposity _roleReposity;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private bool _isActive;


        [ObservableProperty]
        private Role? _selectedRole;

        [ObservableProperty]
        private bool _isEnabled = false;

        public ObservableCollection<Role> Roles { get; set; } = [];

        public RoleViewModel(RoleReposity roleReposity)
        {
            _roleReposity = roleReposity;
        }

        public async Task LoadRolesAsync()
        {
            Roles.Clear();

            var roles = await _roleReposity.GetAllRolesAsync();

            foreach ( var role in roles )
            {
                Roles.Add(role);
            }
        }

        [RelayCommand]
        public async Task AddRoleAsync()
        {
            if ( Name == null )
            {
                await Shell.Current.DisplayAlert("Validation Error", "Role name is required.", "OK");
                return;
            }

            var newRole = new Role
            {
                Name = Name,
                IsActive = IsActive
            };

            await _roleReposity.AddRoleAsync(newRole);

            await ToastHelper.GetToastAsync("Role added successfully.", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
        }
    }
}
