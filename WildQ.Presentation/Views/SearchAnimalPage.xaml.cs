namespace WildQ.Presentation.Views;

public partial class SearchAnimalPage : ContentPage
{
	public SearchAnimalPage()
	{
		InitializeComponent();

        BindingContext = new ViewModels.SearchAnimalPageViewModel();
    }

    private async void OnClickedGoBackToMainPage(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}