using CORCEGuideApp.ViewModels;

namespace CORCEGuideApp.Views;

public partial class AddAnnouncementView : ContentPage
{
	public AddAnnouncementView(AnnouncementViewModel announcementViewModel)
	{
		InitializeComponent();
		BindingContext = announcementViewModel;
    }
}