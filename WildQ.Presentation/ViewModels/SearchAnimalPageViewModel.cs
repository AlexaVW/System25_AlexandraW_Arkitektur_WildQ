using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Entities.Models.MongoDbModels;
using Domain.Entities.Models.ApiModels;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation.ViewModels
{
    public class SearchAnimalPageViewModel : INotifyPropertyChanged
    {
        readonly private ISearchAnimalService _searchAnimalService;

        public SearchAnimalPageViewModel(ISearchAnimalService searchAnimalService)
        {
            _searchAnimalService = searchAnimalService;
            GetAnimal = new Command(async () =>
            {
                await LoadAnimalsAsync(AnimalNameInput);
            });
        }

        private string _animalNameInput;
        public string AnimalNameInput
        {
            get { return _animalNameInput; }
            set
            {
                _animalNameInput = value; // Takes the new value and sets it in the private property
                OnPropertyChanged(nameof(AnimalNameInput)); //UI is updating
            }
        }
        public ICommand GetAnimal { get; set; } //ICommand property


        //Can't use a list for property changed so creating an ObservableCollection instead
        private ObservableCollection<SearchAnimal> _animals = new ObservableCollection<SearchAnimal>();
        public ObservableCollection<SearchAnimal> Animals //The property we want to change in our view
        {
            get { return _animals; }
            set
            {
                _animals = value;
                OnPropertyChanged(nameof(Animals));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadAnimalsAsync(string animalName = "")
        {
            ObservableCollection<SearchAnimal> animals = new ObservableCollection<SearchAnimal>();
            if (!string.IsNullOrWhiteSpace(animalName))
            {
                var animalList = await _searchAnimalService.GetAnimalsAsync(animalName);
                animals = new ObservableCollection<SearchAnimal>(animalList);
            }

            Animals = animals;

        }
    }
}
