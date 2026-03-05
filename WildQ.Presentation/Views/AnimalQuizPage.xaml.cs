using Domain.Entities.Interfaces;
using Domain.Entities.Models.MongoDbModels;
using MongoDB.Bson.Serialization.Serializers;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation.Views;

public partial class AnimalQuizPage : ContentPage
{
    IAnimalService _animalService;

    int amountOfCorrectAnswers = 0;

    private Animal _animal;
    public Animal Animal { get; set; } // public - To use in binding for AnimalName and ImageSource in xaml

    public List <Answer> RandomOrderOnAnswers { get; set; }

    private Random _random = new Random();

    private int _currentQuestionIndex = 0; // To keep track on which question we are on


    private Question _currentQuestion;
    public Question CurrentQuestion // To be able to change the current question
    {
        get { return _currentQuestion; }
        set
        {
            _currentQuestion = value; // Updating the current question

            RandomizeOrderOnAnswers(); // Randomizing the answers on the current question

            OnPropertyChanged(nameof(CurrentQuestion)); //Tells the UI to update to the current questiontext

            OnPropertyChanged(nameof(RandomOrderOnAnswers)); // Tells the UI to update after the random order on the answers are done
        }
    }
    public AnimalQuizPage(Animal animal) // Constructor
	{
		InitializeComponent();
        _animalService = new AnimalService(); 
        
        Animal = animal; 
        _animal = animal; //Telling that the animal sent in to the construct is this animal

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
            
            await DisplayAlert("You finished the quiz", $"You answered correct on {amountOfCorrectAnswers} questions out of {_animal.Questions.Count}!" , "OK");
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
            amountOfCorrectAnswers += 1;
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
        //var animal = ((Button)sender).BindingContext as Animal; //Dont need this when sending in private animal. 
        await Navigation.PushAsync(new QuizAdminPage(_animal));
    }

    private async void OnClickedDeleteAnimal(object sender, EventArgs e)
    {
        //var animal = ((Button)sender).BindingContext as Animal; //Dont need this when sending in private animal
        await _animalService.DeleteAnimalAsync(_animal);
        await Navigation.PushAsync(new EndangeredAnimalQuiz());

    }

    private void RandomizeOrderOnAnswers()
    {
        RandomOrderOnAnswers = _currentQuestion.Answers.OrderBy(answer => _random.Next()).ToList();
        //Random.Next gives random numbers to each answer. Ordering by those numbers - Making it to a list
    }

    
    
}