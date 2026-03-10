using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Interfaces;
using Domain.Entities.Models.MongoDbModels;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation.ViewModels
{
    public class AnimalQuizViewModel : INotifyPropertyChanged
    {
        private readonly IAnimalService _animalService;

        public AnimalQuizViewModel(IAnimalService animalService) //Constructor
        {
            _animalService = animalService;
            LoadAnimalsInQuizAsync();
        }

        // Creating an ObservableCollection on Animals instead of a list so that the system can see if anything has changed or not
        private ObservableCollection<Animal> _animals;
        public ObservableCollection<Animal> Animals
        {
            get { return _animals; }
            set
            {
                _animals = value;
                OnPropertyChanged(nameof(Animals));
            }
        }
        

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private async void LoadAnimalsInQuizAsync()
        {
            Animals = new ObservableCollection<Animal>();
            var data = await _animalService.GetAllAnimalsAsync(); //Getting the data from repository
            foreach (var animal in data) //Adding every animal from the list to the ObservableCollection. OnProperyChanged uppdates
            {
                Animals.Add(animal);
            }
        }

        // Try instead?
        // AnimalsInQuiz = new ObservableCollection<AnimalInQuiz>(await _animalsInQuizRepository.ReadAnimalInQuizAsync());


    }
}
