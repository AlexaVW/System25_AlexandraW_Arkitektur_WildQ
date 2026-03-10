using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Models.MongoDbModels;

namespace WildQ.Presentation.ViewModels
{
    public class ScorePageViewModel
    {
        public int AmountOfCorrectAnswers { get; set; }
        public int AmountOfQuestions { get; set; }
        public Animal Animal { get; set; }

        public ScorePageViewModel(int correctAnswers, Animal animal)
        {
            Animal = animal;
            AmountOfCorrectAnswers = correctAnswers;
            AmountOfQuestions = animal.Questions.Count;
        }
    }
}
