using Domain.Entities.Interfaces;
using Domain.Entities.Models.MongoDbModels;
using MongoDB.Bson.Serialization.Serializers;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation.Views;

public partial class AnimalQuizPage : ContentPage
{
    IAnimalService _animalService;

    private Animal _animal;
    public Animal Animal { get; set; } // public - To use in binding for AnimalName and ImageSource in xaml
    
    private int _currentQuestionIndex = 0; // To keep track on which question we are on

    private Question _currentQuestion;
    public Question CurrentQuestion // To be able to change the current question
    {
        get { return _currentQuestion; }
        set
        {
            _currentQuestion = value;
            OnPropertyChanged(nameof(CurrentQuestion)); //If it's the next question - property is changed and it tells the UI to update
        }
    }
    public AnimalQuizPage(Animal animal) // Constructor
	{
		InitializeComponent();
        _animalService = new AnimalService(); 
        
        Animal = animal; 
        _animal = animal;

        // To start
        if(Animal != null && Animal.Questions.Count > 0) //If Animal exists and has more than 0 questions
        {
            CurrentQuestion = Animal.Questions[0]; // Then starting the quiz with the first question
        }
        BindingContext = this;
	}

    private async void NextQuestion() 
    {
        _currentQuestionIndex++; // To go to the next question index

        if (_currentQuestionIndex >= _animal.Questions.Count) // If there are no questions left
        {
            await DisplayAlert("You finished the quiz", "Well done", "OK");
            await Navigation.PopAsync(); //Going back to the EndageredAnimalQuiz page
            return; 
        }

        CurrentQuestion = _animal.Questions[_currentQuestionIndex]; // Going to the next question - updating UI
    }

    private async void OnClickedAnswer(object sender, EventArgs e)
    {
        var button = sender as Button; //Setting the sender as a Button
        var selectedAnswer = button.BindingContext as Answer; //Binding the button as Answer

        if (selectedAnswer.IsTrue)
        {
            await DisplayAlert("Correct answer", "Well done", "Next");

        }
        else
        {
            await DisplayAlert("Wrong answer", "Keep going", "Next");
        }

        NextQuestion(); //Going to the next question when clicking Next
    }

    private async void OnClickedUpdateAnimal(object sender, EventArgs e)
    {
        var animal = ((Button)sender).BindingContext as Animal;
        await Navigation.PushAsync(new QuizAdminPage(animal));
    }

    private async void OnClickedDeleteAnimal(object sender, EventArgs e)
    {
        var animal = ((Button)sender).BindingContext as Animal;
        await _animalService.DeleteAnimalAsync(animal);
        await Navigation.PushAsync(new EndangeredAnimalQuiz());

    }
    
}