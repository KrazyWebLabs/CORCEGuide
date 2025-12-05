using CORCEGuideApp.ViewModels;
using System.Threading.Tasks;

namespace CORCEGuideApp.Views;

public partial class MainAnnouncementView : ContentPage
{
    private readonly AnnouncementViewModel announcementViewModel;

    public MainAnnouncementView(AnnouncementViewModel announcementViewModel)
	{
		InitializeComponent();
		BindingContext = announcementViewModel;
		this.announcementViewModel = announcementViewModel;
    }
	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await announcementViewModel.LoadAnnouncementsAsync();
		await announcementViewModel.LoadCategoriesAsync();

    }

    private async void OnAddNewAnnouncement(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"/{nameof(AddAnnouncementView)}");
    }
}