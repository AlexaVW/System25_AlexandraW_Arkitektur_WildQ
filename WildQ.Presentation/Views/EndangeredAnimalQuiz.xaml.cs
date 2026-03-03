using Domain.Entities.Models.MongoDbModels;

namespace WildQ.Presentation.Views;

public partial class EndangeredAnimalQuiz : ContentPage
{
	public EndangeredAnimalQuiz()
	{
		InitializeComponent();

        BindingContext = new ViewModels.AnimalQuizViewModel();
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
        await Navigation.PushAsync(new QuizAdminPage(null)); //null because we don't have an animal yet
    }
}