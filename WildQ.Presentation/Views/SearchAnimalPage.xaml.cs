using WildQ.Application.Interfaces;

namespace WildQ.Presentation.Views;

public partial class SearchAnimalPage : ContentPage
{
    // Constructor ----------------------------------------------------------------------------
    public SearchAnimalPage(ViewModels.SearchAnimalPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel; 
    }

    // Clicks ---------------------------------------------------------------------------------
    private async void OnClickedGoBackToMainPage(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}