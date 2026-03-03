using Domain.Entities.Models.MongoDbModels;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation
{
    public partial class MainPage : ContentPage
    {
        IAnimalService _animalService;
        ISearchAnimalService _searchAnimalService;
        public MainPage(IAnimalService animalService, ISearchAnimalService searchAnimalService)
        {
            InitializeComponent();
            _animalService = animalService;
            _searchAnimalService = searchAnimalService;
        }

        

        

        private async void OnClickedSearchAnimal(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.SearchAnimalPage());
        }

        private async void OnClickedEndangeredAnimalQuiz(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.EndangeredAnimalQuiz());
        }

        private async void OnClickedProfile(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ProfilePage());
        }
    }
}
