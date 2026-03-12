using Domain.Entities.Models.MongoDbModels;
using WildQ.Presentation.ViewModels;

namespace WildQ.Presentation.Views;

public partial class ScorePage : ContentPage
{
    // Constructor -----------------------------------------------------
    public ScorePage(int amountOfCorrectAnswers, Animal animal)
	{
		InitializeComponent();
		
        BindingContext = new ScorePageViewModel(amountOfCorrectAnswers, animal);
	}

    // Clicks --------------------------------------------------------
    private async void OnClickedTryAgain(object sender, EventArgs e)
    {
		var viewModel = BindingContext as ScorePageViewModel; // To access properties
        await Navigation.PushAsync(new AnimalQuizPage(viewModel.Animal));
    }

    private async void OnClickedGoToMainPage(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}