using CORCEGuideApp.Views;

namespace CORCEGuideApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddCategoryView), typeof(AddCategoryView));
            Routing.RegisterRoute(nameof(AddAnnouncementView), typeof(AddAnnouncementView));
            Routing.RegisterRoute(nameof(AddFavoriteView), typeof(AddFavoriteView));
            Routing.RegisterRoute(nameof(AddRoleView), typeof(AddRoleView));
            Routing.RegisterRoute(nameof(AddUserView), typeof(AddUserView));

            Routing.RegisterRoute(nameof(AddCategoryView), typeof(AddCategoryView));
            Routing.RegisterRoute(nameof(AddAnnouncementView), typeof(AddAnnouncementView));
            Routing.RegisterRoute(nameof(AddFavoriteView), typeof(AddFavoriteView));
            Routing.RegisterRoute(nameof(AddRoleView), typeof(AddRoleView));
            Routing.RegisterRoute(nameof(AddUserView), typeof(AddUserView));
        }
    }
}
