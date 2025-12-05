using CORCEGuideApp.ViewModels;

namespace CORCEGuideApp.Views;

public partial class AddCategoryView : ContentPage
{
	public AddCategoryView(CategoryViewModel categoryViewModel)
	{
		InitializeComponent();
		BindingContext = categoryViewModel;
    }
}