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
    public class EndangeredAnimalQuizViewModel : INotifyPropertyChanged
    {
        private readonly IAnimalService _animalService;

        public EndangeredAnimalQuizViewModel(IAnimalService animalService) //Constructor
        {
            _animalService = animalService;
            LoadAnimalsInQuizAsync();
        }

        private List<Animal> _allAnimalsInQuiz; //To store all the animals so we can filter by order

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
            //Animals = new ObservableCollection<Animal>();
            //var data = await _animalService.GetAllAnimalsAsync(); //Getting the data from repository
            //foreach (var animal in data) //Adding every animal from the list to the ObservableCollection. OnProperyChanged uppdates
            //{
            //    Animals.Add(animal);
            //}
            var data = await _animalService.GetAllAnimalsAsync();

            _allAnimalsInQuiz = data.ToList();
            Animals = new ObservableCollection<Animal>(_allAnimalsInQuiz);

        }
        // Try instead?
        // AnimalsInQuiz = new ObservableCollection<AnimalInQuiz>(await _animalsInQuizRepository.ReadAnimalInQuizAsync());


        public void FilterByOrder(string order)
        {
            var filterByOrder = _allAnimalsInQuiz.Where(animal => animal.Order == order);

            Animals = new ObservableCollection<Animal>(filterByOrder);
        }

        public void ShowAllAnimalsInQuiz() //Using the already stored list. Don't have to call the database again
        {
            Animals = new ObservableCollection<Animal>(_allAnimalsInQuiz);
        }
    }
}
