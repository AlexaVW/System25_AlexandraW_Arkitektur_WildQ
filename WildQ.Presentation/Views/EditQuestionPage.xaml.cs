using System.Threading.Tasks;
using Domain.Entities.Models.MongoDbModels;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation.Views;

public partial class EditQuestionPage : ContentPage
{
	IAnimalService _animalService;

    // Constructor ----------------------------------------------------------------------------
    // To edit an existing question with answers
    public EditQuestionPage(Question question, Animal animal)
    {
        InitializeComponent();

        _animalService = new AnimalService();
        
        Animal = animal;
        
        Question = question;

        if (question != null) 
        {
            // Populating entries with existing data
            QuestionTextEntry.Text = question.QuestionText;

            AnswerText1Entry.Text = question.Answers[0].AnswerText;
            AnswerText2Entry.Text = question.Answers[1].AnswerText;
            AnswerText3Entry.Text = question.Answers[2].AnswerText;
            AnswerText4Entry.Text = question.Answers[3].AnswerText;
        }
    }

    // Properties -----------------------------------------------------------------------------
    public Animal Animal { get; set; }
	public Question Question { get; set; }

    // Clicks ---------------------------------------------------------------------------------
    private async void OnClickedSaveQuestionButton(object sender, EventArgs e)
    {
		// Updating the question and answers in Animal 
        Question.QuestionText = QuestionTextEntry.Text;
		
		Question.Answers[0].AnswerText = AnswerText1Entry.Text;
        Question.Answers[1].AnswerText = AnswerText2Entry.Text;
        Question.Answers[2].AnswerText = AnswerText3Entry.Text;
        Question.Answers[3].AnswerText = AnswerText4Entry.Text;

        await _animalService.UpdateAnimalAsync(Animal);
        await Navigation.PushAsync(new QuizAdminPage(Animal));
    }

    private async void OnClickedDeleteQuestion(object sender, EventArgs e)
    {
		// Deleting the Question and updating Animal
        Animal.Questions.Remove(Question);

        await _animalService.UpdateAnimalAsync(Animal);
        await Navigation.PushAsync(new QuizAdminPage(Animal));
    }

    private async void OnClickedGoBackToQuizAdminPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QuizAdminPage(Animal));
    }
}