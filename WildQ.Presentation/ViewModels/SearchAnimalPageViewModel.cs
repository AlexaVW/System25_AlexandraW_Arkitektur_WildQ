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

        // Constructor ----------------------------------------------------------------------------
        public SearchAnimalPageViewModel(ISearchAnimalService searchAnimalService)
        {
            _searchAnimalService = searchAnimalService;
            
            // When user clicks on Search button
            SearchAnimalCommand = new Command(async () =>
            {
                await LoadAnimalsAsync(AnimalNameInput); // Loading the searched animal from the API
            });
        }

        // Properties -----------------------------------------------------------------------------
        private string _animalNameInput;
        public string AnimalNameInput 
        {
            get { return _animalNameInput; }
            set
            {
                _animalNameInput = value; 
                OnPropertyChanged(nameof(AnimalNameInput)); 
            }
        }
        public ICommand SearchAnimalCommand { get; set; } 

        private ObservableCollection<SearchAnimal> _animals = new ObservableCollection<SearchAnimal>();
        public ObservableCollection<SearchAnimal> Animals 
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

        // Methods --------------------------------------------------------------------------------------
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
