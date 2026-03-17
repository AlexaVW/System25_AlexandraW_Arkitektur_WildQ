using System.Diagnostics;
using System.Threading.Tasks;
using Domain.Entities.Interfaces;
using Domain.Entities.Models.MongoDbModels;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation.Views;

public partial class QuizAdminPage : ContentPage
{
    IAnimalService _animalService;

    // Constructor ----------------------------------------------------------------------------
    
    public QuizAdminPage(Animal animal) 
    {
        InitializeComponent();

        _animalService = new AnimalService();

        Animal = animal;
        
        // Not visible when creating animal
        EditExistingQuestionLabel.IsVisible = false; 
        ExistingQuestionsBorder.IsVisible = false;
        
        // Edit Animal 
        if (animal != null) 
        {
            BindingContext = Animal; 

            // Populating entries with existing data
            AnimalNameEntry.Text = animal.AnimalName;
            ImageSourceEntry.Text = animal.ImageSource;
            OrderEntry.Text = animal.Order;
            SaveButton.Text = "Update Animal";

            // Only visible if there are questions
            bool hasQuestions = animal.Questions != null && animal.Questions.Count > 0;
            EditExistingQuestionLabel.IsVisible = hasQuestions; 
            ExistingQuestionsBorder.IsVisible = hasQuestions;
        }
    }

    // Properties -----------------------------------------------------------------------------
    public Animal Animal { get; set; }
    public List<Question> Questions { get; set; }

    // Clicks ---------------------------------------------------------------------------------
    // Animal
    private async void OnClickedSaveButton(object sender, EventArgs e)
    {
        if(!await ValidAnimalInput())
        {
            return;
        }
        
        if (Animal == null) 
        {
            await CreateNewAnimal();
        }
        else 
        {
            await UpdateExistingAnimal();
        }
    }

    // Question and Answers
    private async void OnClickedSaveQuestionButton(object sender, EventArgs e)
    {
        if(!await ValidQuestionAndAnswerInputs())
        {
            return;
        }
        
        if (Animal == null) 
        {
            await DisplayAlert("Error", "You have to save the animal first", "OK");
            return;
        }
        
        Question question = CreateQuestion();
        await SaveQuestion(question);
    }

    private async void OnClickedGoBackToAnimalQuizPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(EndangeredAnimalQuizPage));
    }

    // Methods --------------------------------------------------------------------------------------
    // Animal ------------------------------------------------------
    private async Task<bool> ValidAnimalInput()
    {
        if (string.IsNullOrWhiteSpace(AnimalNameEntry.Text) ||
            string.IsNullOrWhiteSpace(ImageSourceEntry.Text) ||
            string.IsNullOrWhiteSpace(OrderEntry.Text))
        {
            await DisplayAlert("Error", "You can't leave any field empty", "OK");
            return false;
        }
        return true;
    }
    private async Task CreateNewAnimal()
    {
        Animal = new Animal()
        {
            // Reading from entry
            Id = Guid.NewGuid().ToString(),
            AnimalName = AnimalNameEntry.Text,
            ImageSource = ImageSourceEntry.Text,
            Order = OrderEntry.Text
        };

        await _animalService.CreateAnimalAsync(Animal);
        await DisplayAlert("Successful", "Animal is saved", "OK");
    }

    private async Task UpdateExistingAnimal()
    {
        // Reading from entry
        Animal.AnimalName = AnimalNameEntry.Text;
        Animal.ImageSource = ImageSourceEntry.Text;
        Animal.Order = OrderEntry.Text;

        await _animalService.UpdateAnimalAsync(Animal);
        await DisplayAlert("Successful", "Animal updated", "OK");
    }

    // Question and Answers ----------------------------------------
    private async Task<bool> ValidQuestionAndAnswerInputs()
    {
        if (string.IsNullOrWhiteSpace(QuestionTextEntry.Text))
        {
            await DisplayAlert("Error", "You can't leave any field empty", "OK");
            return false;
        }

        if (string.IsNullOrWhiteSpace(AnswerText1Entry.Text) ||
            string.IsNullOrWhiteSpace(AnswerText2Entry.Text) ||
            string.IsNullOrWhiteSpace(AnswerText3Entry.Text) ||
            string.IsNullOrWhiteSpace(AnswerText4Entry.Text))
        {
            await DisplayAlert("Error", "You can't leave any field empty", "OK");
            return false;
        }
        return true;
    }
    private Question CreateQuestion()
    {
        Question question = new Question()
        {
            Id = Guid.NewGuid().ToString(),
            QuestionText = QuestionTextEntry.Text,
            Answers = new List<Answer>
            {
                CreateNewAnswer(AnswerText1Entry.Text, true),
                CreateNewAnswer(AnswerText2Entry.Text, false),
                CreateNewAnswer(AnswerText3Entry.Text, false),
                CreateNewAnswer(AnswerText4Entry.Text, false)
            }
        };
        return question;
    }
    private static Answer CreateNewAnswer(string answerText, bool isTrue)
    {
        return new Answer
        {
            Id = Guid.NewGuid().ToString(),
            AnswerText = answerText,
            IsTrue = isTrue
        };
    }
    private async Task SaveQuestion(Question question)
    {
        Animal.Questions.Add(question);

        await _animalService.UpdateAnimalAsync(Animal);
        await DisplayAlert("Successful", "the question is updated", "OK");

        await Navigation.PushAsync(new QuizAdminPage(Animal));
    }
    

    // OnCollectionViewSelectionChanged ----------------------------------------------------------
    // When clicked on an existing question to edit 
    private async void OnSelectedChangedQuestion(object sender, SelectionChangedEventArgs e)
    {
        Question question = ((CollectionView)sender).SelectedItem as Question;

        ((CollectionView)sender).SelectedItem = null; 

        if (question != null)
        {
            await Navigation.PushAsync(new EditQuestionPage(question, Animal));
        }
    }
}