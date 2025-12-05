using CORCEGuideApp.ViewModels;
using System.Threading.Tasks;

namespace CORCEGuideApp.Views;

public partial class MainCategoryView : ContentPage
{
    private readonly CategoryViewModel categoryViewModel;

    public MainCategoryView(CategoryViewModel categoryViewModel)
	{
		InitializeComponent();
        BindingContext = categoryViewModel;
        this.categoryViewModel = categoryViewModel;
    }

    //private async void OnCategorySelected(object sender, SelectionChangedEventArgs e)
    //{
    //    if ( this.categoryViewModel.SelectedCategory != null )
    //        await Shell.Current.GoToAsync($"/{nameof{CategoryDetailsView}}");
    //}

    private async void OnAddNewCategory(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(AddCategoryView)}");
    }
}