using Domain.Entities.Interfaces;
using Domain.Entities.Models.MongoDbModels;
using MongoDB.Bson.Serialization.Serializers;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation.Views;

public partial class AnimalQuizPage : ContentPage
{
    IAnimalService _animalService;

    // Constructor ----------------------------------------------------------------------------
    public AnimalQuizPage(Animal animal) 
    {
        InitializeComponent();
        _animalService = new AnimalService();
        
        _animal = animal; 
        Animal = animal; 

        // Starting the quiz with the first question
        if (Animal != null && Animal.Questions.Count > 0) 
        {
            CurrentQuestion = Animal.Questions[0];
        }
        BindingContext = this; 
    }

    // Properties -----------------------------------------------------------------------------
    
    private Animal _animal;
    public Animal Animal { get; set; } 

    public List <Answer> RandomOrderOnAnswers { get; set; }
    private Random _random = new Random();
    
    int amountOfCorrectAnswers = 0;
    
    private int _currentQuestionIndex = 0; 

    private Question _currentQuestion;
    public Question CurrentQuestion 
    {
        get { return _currentQuestion; }
        set
        {
            _currentQuestion = value; 

            RandomizeOrderOnAnswers();
            
            // Tells the UI to update to the current questiontext
            OnPropertyChanged(nameof(CurrentQuestion));
            
            // Tells the UI to update after the random order on the answers are done
            OnPropertyChanged(nameof(RandomOrderOnAnswers)); 
        }
    }
    
    // On Appearing ------------------------------------------------------------------------------
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

    // Clicks ------------------------------------------------------------------------------------
    private async void OnClickedAnswer(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedAnswer = button.BindingContext as Answer;
        
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
        
        await Shell.Current.GoToAsync(nameof(EndangeredAnimalQuizPage));
    }
    private async void OnClickedGoBackToEndangeredAnimalQuiz(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(EndangeredAnimalQuizPage));
    }

    // Methods --------------------------------------------------------------------------------------
    private async void NextQuestion()
    {
        _currentQuestionIndex++;

        // If there are no questions left
        if (_currentQuestionIndex >= _animal.Questions.Count) 
        {
            await Navigation.PushAsync(new ScorePage(amountOfCorrectAnswers, _animal));
            return;
        }
        
        // Going to the next question - updating UI
        CurrentQuestion = _animal.Questions[_currentQuestionIndex]; 
    }
    private void RandomizeOrderOnAnswers()
    {
        RandomOrderOnAnswers = _currentQuestion.Answers.OrderBy(answer => _random.Next()).ToList();
    }
}