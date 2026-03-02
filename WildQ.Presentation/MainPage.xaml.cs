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

        private async void ClickedOnGetData(object sender, EventArgs e)
        {
            CollectionViewAnimals.ItemsSource = await _animalService.GetAllAnimalsAsync();

            //CollectionViewAnimals.ItemsSource = await _searchAnimalService.GetAnimalsAsync("lion");
        }

        private async void OnClickedAddAnimal(object sender, EventArgs e)
        {
            await _animalService.CreateAnimalAsync(new Animal() { Id = Guid.NewGuid().ToString(), AnimalName = EntryAnimalName.Text, ImageSource = EntryImageUrl.Text});
            EntryAnimalName.Text = "";
            EntryImageUrl.Text = "";
        }
    }
}
