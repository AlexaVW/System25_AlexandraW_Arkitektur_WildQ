using Domain.Entities.Interfaces;
using Domain.Entities.Models.MongoDbModels;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation.Views;

public partial class AnimalQuizPage : ContentPage
{
    //IAnimalRepository _animalRepository = new AnimalRepositoryDb();
    IAnimalService _animalService;
    public AnimalQuizPage()
	{
		InitializeComponent();

        _animalService = new AnimalService();
	}

    private async void OnClickedUpdateAnimal(object sender, EventArgs e)
    {
        var animal = ((Button)sender).BindingContext as Animal;
        await Navigation.PushAsync(new QuizAdminPage(animal));
    }

    private async void OnClickedDeleteAnimal(object sender, EventArgs e)
    {
        var animal = ((Button)sender).BindingContext as Animal;
        _animalService.DeleteAnimalAsync(animal);
        await Navigation.PushAsync(new EndangeredAnimalQuiz());

    }
}