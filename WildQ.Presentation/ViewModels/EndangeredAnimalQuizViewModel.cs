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

        // Constructor ----------------------------------------------------------------------------
        public EndangeredAnimalQuizViewModel(IAnimalService animalService) 
        {
            _animalService = animalService;
            LoadAnimalsInQuizAsync();
        }

        // Properties -----------------------------------------------------------------------------
        private List<Animal> _allAnimalsInQuiz; 


        private ObservableCollection<Animal> _animals;
        public ObservableCollection<Animal> Animals // Binds to CollectionView ItemsSource
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

        // Methods --------------------------------------------------------------------------------------
        private async Task LoadAnimalsInQuizAsync()
        {
            var data = await _animalService.GetAllAnimalsAsync();

            _allAnimalsInQuiz = data.ToList();
            Animals = new ObservableCollection<Animal>(_allAnimalsInQuiz);
        }
        
        public void FilterByOrder(string order)
        {
            // Filtering by animal-order
            var filterByOrder = _allAnimalsInQuiz.Where(animal => animal.Order == order);

            Animals = new ObservableCollection<Animal>(filterByOrder);
        }

        public void ShowAllAnimalsInQuiz() 
        {
            Animals = new ObservableCollection<Animal>(_allAnimalsInQuiz);
        }
    }
}
