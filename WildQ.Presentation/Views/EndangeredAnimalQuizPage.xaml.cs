using System.Threading.Tasks;
using Domain.Entities.Models.MongoDbModels;
using WildQ.Application.Services;
using WildQ.Presentation.ViewModels;

namespace WildQ.Presentation.Views;

public partial class EndangeredAnimalQuizPage : ContentPage
{
    public EndangeredAnimalQuizPage(ViewModels.EndangeredAnimalQuizViewModel viewModel)
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

    private async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e) // When an animal is selected
    {
        var selectedAnimal = ((CollectionView)sender).SelectedItem as Animal;

        ((CollectionView)sender).SelectedItem = null; // To be able to klick on the same animal again if we go back

        if (selectedAnimal != null)
        {
            // Binding AnimalQuizPage with the animal that is clicked on
            var page = new AnimalQuizPage(selectedAnimal);
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

    private void OnClickedCarnivoraButton(object sender, EventArgs e)
    {
        var viewModel = BindingContext as EndangeredAnimalQuizViewModel;
        if (viewModel != null)
        {
            viewModel.FilterByOrder("Carnivora");
        }
        
    }

    private void OnClickedPrimatesButton(object sender, EventArgs e)
    {
        var viewModel = BindingContext as EndangeredAnimalQuizViewModel;
        if (viewModel != null)
        {
            viewModel.FilterByOrder("Primates");
        }
    }

    private void OnClickedOddToedUngulatesButton(object sender, EventArgs e)
    {
        var viewModel = BindingContext as EndangeredAnimalQuizViewModel;
        if (viewModel != null)
        {
            viewModel.FilterByOrder("Odd-Toed Ungulates");
        }
    }

    private void OnClickedEvenToedUngulatesButton(object sender, EventArgs e)
    {
        var viewModel = BindingContext as EndangeredAnimalQuizViewModel;
        if (viewModel != null)
        {
            viewModel.FilterByOrder("Even-Toed Ungulates");
        }
    }

    private void OnClickedAllAnimalsButton(object sender, EventArgs e)
    {
        var viewModel = BindingContext as EndangeredAnimalQuizViewModel;
        if (viewModel != null)
        {
            viewModel.ShowAllAnimalsInQuiz();
        }
    }
}