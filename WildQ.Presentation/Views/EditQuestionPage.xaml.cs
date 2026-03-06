using System.Threading.Tasks;
using Domain.Entities.Models.MongoDbModels;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation.Views;

public partial class EditQuestionPage : ContentPage
{

	IAnimalService _animalService;

	
	public Animal Animal { get; set; }
	public Question Question { get; set; }
	public EditQuestionPage(Question question, Animal animal)
	{
		InitializeComponent();

        _animalService = new AnimalService();

        Question = question;

		Animal = animal;
		if (question != null) // If the question exists
		{
			
			QuestionTextEntry.Text = question.QuestionText;

			AnswerText1Entry.Text = question.Answers[0].AnswerText;
			AnswerText2Entry.Text = question.Answers[1].AnswerText;
            AnswerText3Entry.Text = question.Answers[2].AnswerText;
            AnswerText4Entry.Text = question.Answers[3].AnswerText;
        }

	}

    private async void OnClickedSaveQuestionButton(object sender, EventArgs e)
    {
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
		Animal.Questions.Remove(Question);

		await _animalService.UpdateAnimalAsync(Animal);
		await Navigation.PushAsync(new QuizAdminPage(Animal));
    }
}