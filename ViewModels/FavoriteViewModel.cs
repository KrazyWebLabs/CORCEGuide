using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORCEGuideApp.ViewModels
{
    public partial class FavoriteViewModel : ObservableObject
    {
        [ObservableProperty]
        private int favoriteId;

        [ObservableProperty]
        private int userId;

        [ObservableProperty]
        private int announcementId;
    }
}
