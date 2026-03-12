using Domain.Entities.Interfaces;
using Domain.Entities.Models.MongoDbModels;
using MongoDB.Bson.Serialization.Serializers;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation.Views;

public partial class AnimalQuizPage : ContentPage
{
    IAnimalService _animalService;

    // Constructor -----------------------------------------------------------------
    public AnimalQuizPage(Animal animal) 
    {
        InitializeComponent();
        _animalService = new AnimalService();
        
        _animal = animal; // To be used in navigation and methods
        Animal = animal; // For binding in UI

        // Starting the quiz with the first question
        if (Animal != null && Animal.Questions.Count > 0) 
        {
            CurrentQuestion = Animal.Questions[0];
        }
        BindingContext = this; // Binding this page for UI
    }

    // Properties -----------------------------------------------------------------------------
    
    private Animal _animal;
    public Animal Animal { get; set; } // public - To use in binding for AnimalName and ImageSource in xaml

    public List <Answer> RandomOrderOnAnswers { get; set; }
    private Random _random = new Random();
    
    int amountOfCorrectAnswers = 0;
    
    private int _currentQuestionIndex = 0; // To keep track on which question the user is on

    private Question _currentQuestion;
    public Question CurrentQuestion // To be able to change the current question
    {
        get { return _currentQuestion; }
        set
        {
            _currentQuestion = value; // Updating the current question

            RandomizeOrderOnAnswers(); // Randomizing the order of answers on the current question

            OnPropertyChanged(nameof(CurrentQuestion)); // Tells the UI to update to the current questiontext

            OnPropertyChanged(nameof(RandomOrderOnAnswers)); // Tells the UI to update after the random order on the answers are done
        }
    }
    
    // On Appearing --------------------------------------------------------------------------------
    protected override void OnAppearing()
    {
        base.OnAppearing();

        var userSession = UserSession.GetUserSession();
        if (!userSession.IsAdmin)
        {
            EditAnimalButton.IsVisible = false;
            DeleteAnimalButton.IsVisible= false;
        }
    }

    // Clicks -------------------------------------------------------------------------------------------
    private async void OnClickedAnswer(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedAnswer = button.BindingContext as Answer; // Binding the button as Answer
        
        if (selectedAnswer.IsTrue)
        {
            amountOfCorrectAnswers += 1;
            await DisplayAlert("Correct answer", "Well done", "Next");
        }
        else
        {
            await DisplayAlert("Wrong answer", "Keep going", "Next");
        }

        NextQuestion(); 
    }

    private async void OnClickedUpdateAnimal(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QuizAdminPage(_animal));
    }

    private async void OnClickedDeleteAnimal(object sender, EventArgs e)
    {
        bool delete = await DisplayAlert("Deleting animal", "Are you sure you want to delete this quiz?", "Yes", "No");
        if (!delete)
        {
            return;
        }
        await _animalService.DeleteAnimalAsync(_animal);
        
        //await Navigation.PushAsync(new EndangeredAnimalQuiz());
        await Shell.Current.GoToAsync(nameof(EndangeredAnimalQuizPage));
    }
    private async void OnClickedGoBackToEndangeredAnimalQuiz(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new EndangeredAnimalQuiz());
        await Shell.Current.GoToAsync(nameof(EndangeredAnimalQuizPage));
    }

    // Methods ----------------------------------------------------------------------------------
    private async void NextQuestion()
    {
        _currentQuestionIndex++; // To go to the next question

        if (_currentQuestionIndex >= _animal.Questions.Count) // If there are no questions left
        {
            await Navigation.PushAsync(new ScorePage(amountOfCorrectAnswers, _animal));
            return;
        }

        CurrentQuestion = _animal.Questions[_currentQuestionIndex]; // Going to the next question - updating UI
    }
    private void RandomizeOrderOnAnswers()
    {
        RandomOrderOnAnswers = _currentQuestion.Answers.OrderBy(answer => _random.Next()).ToList();
        // Random.Next gives random numbers to each answer. Ordering by those numbers
    }
}