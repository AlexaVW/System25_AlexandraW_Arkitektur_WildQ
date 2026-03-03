using Domain.Entities.Interfaces;
using Domain.Entities.Models.MongoDbModels;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation.Views;

public partial class QuizAdminPage : ContentPage
{
    //IAnimalRepository _animalRepository = new AnimalRepositoryDb();

    IAnimalService _animalService;
    public Animal Animal { get; set; }
    public QuizAdminPage(Animal animal)// Can choose to not send in anything or choose to send in an animal to edit.
    {
		InitializeComponent();

        _animalService = new AnimalService();

        Animal = animal;

        if (animal != null) //If Update Animal 
        {
            AnimalNameEntry.Text = animal.AnimalName; //Takes what's in AnimalName and puts it in EntryText
            ImageSourceEntry.Text = animal.ImageSource;
            SaveButton.Text = "Update Animal"; //New text on the button

            //QuestionTextEntry.Text = animal.Questions;
        }
    }

    private async void OnClickedSaveButton(object sender, EventArgs e)
    {
        if (Animal == null) // If it's empty - Create new Animal
        {
            Animal = new Animal()
            {
                // Reading from entry
                Id = Guid.NewGuid().ToString(),
                AnimalName = AnimalNameEntry.Text,
                ImageSource = ImageSourceEntry.Text

            };
            // Adding animal to the repository which calls on MongoDb
            await _animalService.CreateAnimalAsync(Animal);
        }
        else // If anything is changed it is inserted into Animal
        {
            Animal.AnimalName = AnimalNameEntry.Text; // Reading from entry  - inserts it in Animal
            Animal.ImageSource = ImageSourceEntry.Text;

            await _animalService.UpdateAnimalAsync(Animal);
        }


    }

    private async void OnClickedSaveQuestionButton(object sender, EventArgs e)
    {

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

        await Navigation.PushAsync(new EndangeredAnimalQuiz());

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
}