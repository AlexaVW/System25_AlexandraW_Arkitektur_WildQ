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
    
    // Constructor
    public QuizAdminPage(Animal animal)// Can choose to not send in anything or choose to send in an animal to edit.
    {
        InitializeComponent();

        _animalService = new AnimalService();

        Animal = animal;

        if (animal != null) //If Update Animal 
        {
            BindingContext = Animal; //To print out the questions

            AnimalNameEntry.Text = animal.AnimalName; //Takes what's in AnimalName and puts it in EntryText
            ImageSourceEntry.Text = animal.ImageSource;
            OrderEntry.Text = animal.Order;
            SaveButton.Text = "Update Animal"; //New text on the button
        }
    }

    // Properties
    public Animal Animal { get; set; }
    public List<Question> Questions { get; set; }
    
    // Methods
    private async void OnClickedSaveButton(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(AnimalNameEntry.Text) || string.IsNullOrEmpty(ImageSourceEntry.Text) || string.IsNullOrEmpty(OrderEntry.Text))
        {
            await DisplayAlert("Error", "You can't leave any field empty", "OK");
            return;
        }
        
        if (Animal == null) // If it's empty - Create new Animal
        {
            Animal = new Animal()
            {
                // Reading from entry
                Id = Guid.NewGuid().ToString(),
                AnimalName = AnimalNameEntry.Text,
                ImageSource = ImageSourceEntry.Text,
                Order = OrderEntry.Text
            };
            
                // Adding animal to the repository which calls on MongoDb
                await _animalService.CreateAnimalAsync(Animal);
                await DisplayAlert("Succesful", "Animal is saved", "OK");
        }
        else // If anything is changed it is inserted into Animal
        {
            
            Animal.AnimalName = AnimalNameEntry.Text; // Reading from entry  - inserts it in Animal
            Animal.ImageSource = ImageSourceEntry.Text;
            Animal.Order = OrderEntry.Text;
            
            await _animalService.UpdateAnimalAsync(Animal);
        }
    }

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
        
        if (Animal == null) //Have to save the animal before creating questions
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

        // The list with Questions are inside Animal so we can update the whole Animal
        await _animalService.UpdateAnimalAsync(Animal);
        await DisplayAlert("Succesful", "the question is updated", "OK");

        await Navigation.PushAsync(new QuizAdminPage(Animal));
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

    private async void OnSelectedChangedQuestion(object sender, SelectionChangedEventArgs e)
    {
        Question question = ((CollectionView)sender).SelectedItem as Question; //Casting the selected item as question

        ((CollectionView)sender).SelectedItem = null; // So we can go back and click on the same question again

        if (question != null)
        {
            await Navigation.PushAsync(new EditQuestionPage(question, Animal));
        }
    }

    private async void OnClickedGoBackToAnimalQuizPage(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new EndangeredAnimalQuiz());
        await Shell.Current.GoToAsync(nameof(EndangeredAnimalQuizPage));
    }
}