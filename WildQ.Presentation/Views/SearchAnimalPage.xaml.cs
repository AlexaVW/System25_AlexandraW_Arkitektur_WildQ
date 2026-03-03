namespace WildQ.Presentation.Views;

public partial class SearchAnimalPage : ContentPage
{
	public SearchAnimalPage()
	{
		InitializeComponent();

        BindingContext = new ViewModels.SearchAnimalPageViewModel();
    }
}