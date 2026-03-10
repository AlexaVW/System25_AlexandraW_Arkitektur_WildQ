using System.Threading.Tasks;
using Domain.Entities.Models.MongoDbModels;
using WildQ.Application.Services;

namespace WildQ.Presentation.Views;

public partial class EndangeredAnimalQuiz : ContentPage
{
	
    public EndangeredAnimalQuiz(ViewModels.AnimalQuizViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var userSession = UserSession.GetUserSession();
        if (!userSession.IsAdmin)
        {
            AddQuizButton.IsVisible = false;
        }
    }

    private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e) // When an animal is selected
    {
        var animal = ((ListView)sender).SelectedItem as Animal;
        ((ListView)sender).SelectedItem = null; // To be able to klick on the same animal again if we go back

        if (animal != null)
        {
            // Binding AnimalQuizPage with the animal that is clicked on
            var page = new AnimalQuizPage(animal);
            await Navigation.PushAsync(page);
        }
    }

    private async void OnClickedGoAdminQuizPage(object sender, EventArgs e)
    {
        
        
        await Navigation.PushAsync(new QuizAdminPage(null)); //null so we don't have an to have an animal yet
    }

    private async void OnClickedGoBackToMainPage(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}