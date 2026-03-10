namespace WildQ.Presentation.Views;

public partial class ScorePage : ContentPage
{
    public int AmountOfCorrectAnswers { get; set; }
    public ScorePage(int amountOfCorrectAnswers)
	{
		InitializeComponent();

		AmountOfCorrectAnswers = amountOfCorrectAnswers;
		BindingContext = this;
	}
}