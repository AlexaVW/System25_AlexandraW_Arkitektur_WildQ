using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Entities.Models.MongoDbModels;

namespace WildQ.Presentation.ViewModels
{
    public class ScorePageViewModel
    {
        // Constructor ----------------------------------------------------------------------------
        public ScorePageViewModel(int correctAnswers, Animal animal)
        {
            Animal = animal;
            AmountOfCorrectAnswers = correctAnswers;
            AmountOfQuestions = animal.Questions.Count;
        }

        // Properties -----------------------------------------------------------------------------
        public int AmountOfCorrectAnswers { get; set; }
        public int AmountOfQuestions { get; set; }
        public Animal Animal { get; set; }
    }
}
