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
    private async void OnClickedSaveButton(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(AnimalNameEntry.Text) || 
            string.IsNullOrEmpty(ImageSourceEntry.Text) || 
            string.IsNullOrEmpty(OrderEntry.Text))
        {
            await DisplayAlert("Error", "You can't leave any field empty", "OK");
            return;
        }
        
        if (Animal == null) // Create new Animal
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
                await DisplayAlert("Succesful", "Animal is saved", "OK");
        }
        else // When edit - the new entries are inserted into Animal
        {
            Animal.AnimalName = AnimalNameEntry.Text; 
            Animal.ImageSource = ImageSourceEntry.Text;
            Animal.Order = OrderEntry.Text;
            
            await _animalService.UpdateAnimalAsync(Animal);
            await DisplayAlert("Succesful", "Animal updated", "OK");
        }
    }

    // Question and Answers
    private async void OnClickedSaveQuestionButton(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(QuestionTextEntry.Text))
        {
            await DisplayAlert("Error", "You can't leave any field empty", "OK");
            return;
        }

        if(string.IsNullOrEmpty(AnswerText1Entry.Text) || 
            string.IsNullOrEmpty(AnswerText2Entry.Text) ||
            string.IsNullOrEmpty(AnswerText3Entry.Text) ||
            string.IsNullOrEmpty(AnswerText4Entry.Text))
        {
            await DisplayAlert("Error", "You can't leave any field empty", "OK");
            return;
        }
        
        if (Animal == null) // Have to save the animal before creating questions
        {
            await DisplayAlert("Error", "You have to save the animal first", "OK");
            return;
        }
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
        Animal.Questions.Add(question);

        await _animalService.UpdateAnimalAsync(Animal);
        await DisplayAlert("Succesful", "the question is updated", "OK");

        await Navigation.PushAsync(new QuizAdminPage(Animal));
    }

    private async void OnClickedGoBackToAnimalQuizPage(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new EndangeredAnimalQuiz());
        await Shell.Current.GoToAsync(nameof(EndangeredAnimalQuizPage));
    }

    // Methods --------------------------------------------------------------------------------------
    private static Answer CreateNewAnswer(string answerText, bool isTrue)
    {
        return new Answer
        {
            Id = Guid.NewGuid().ToString(),
            AnswerText = answerText,
            IsTrue = isTrue
        };
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