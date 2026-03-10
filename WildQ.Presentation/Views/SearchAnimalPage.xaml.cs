using WildQ.Application.Interfaces;

namespace WildQ.Presentation.Views;

public partial class SearchAnimalPage : ContentPage
{
	public SearchAnimalPage(ViewModels.SearchAnimalPageViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }


    private async void OnClickedGoBackToMainPage(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}