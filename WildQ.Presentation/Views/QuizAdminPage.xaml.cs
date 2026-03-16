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
    public QuizAdminPage(Animal animal)// If animal exisits = Edit animal - animal gets passed in. If empty - creating new animal
    {
        InitializeComponent();

        _animalService = new AnimalService();

        Animal = animal;

        EditExistingQuestionLabel.IsVisible = false; // Not visible when creating animal
        ExistingQuestionsBorder.IsVisible = false;

        if (animal != null) // If Edit Animal 
        {
            BindingContext = Animal; // For Binding in UI

            // Getting the data that already exists and inserting it to the entries
            AnimalNameEntry.Text = animal.AnimalName;
            ImageSourceEntry.Text = animal.ImageSource;
            OrderEntry.Text = animal.Order;
            SaveButton.Text = "Update Animal"; // New text on the button

            // Are only visible if there are questions
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
        
        if (Animal == null) // Create new Animal
        {
            await CreateNewAnimal();
        }
        else // When edit - the new entries are inserted into Animal
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
        
        if (Animal == null) // Have to save the animal before creating questions
        {
            await DisplayAlert("Error", "You have to save the animal first", "OK");
            return;
        }
        
        Question question = CreateQuestion();
        await SaveQuestion(question);
    }

    private async void OnClickedGoBackToAnimalQuizPage(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new EndangeredAnimalQuiz());
        await Shell.Current.GoToAsync(nameof(EndangeredAnimalQuizPage));
    }

    // Methods --------------------------------------------------------------------------------------
    // Animal------------------------------------------------------
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
        Animal.AnimalName = AnimalNameEntry.Text;
        Animal.ImageSource = ImageSourceEntry.Text;
        Animal.Order = OrderEntry.Text;

        await _animalService.UpdateAnimalAsync(Animal);
        await DisplayAlert("Successful", "Animal updated", "OK");
    }

    // Question and Answers ------------------------------------------
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

    // OnCollectionViewSelectionChanged ----------------------------------------------------------
    // When clicked on an existing question to edit from the collectionview
    private async void OnSelectedChangedQuestion(object sender, SelectionChangedEventArgs e)
    {
        Question question = ((CollectionView)sender).SelectedItem as Question;

        ((CollectionView)sender).SelectedItem = null; // So we can go back and click on the same question again

        if (question != null)
        {
            await Navigation.PushAsync(new EditQuestionPage(question, Animal));
        }
    }
}