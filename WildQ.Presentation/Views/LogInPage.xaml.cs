using WildQ.Application.Interfaces;
using WildQ.Application.Services;
namespace WildQ.Presentation.Views;

public partial class LogInPage : ContentPage
{
    // Constructor ----------------------------------------------------------------------------
    public LogInPage()
	{
		InitializeComponent();
	}

    // Clicks ---------------------------------------------------------------------------------
    private async void OnClickedLogIn(object sender, EventArgs e)
    {
        string userName = UserNameEntry.Text;
        string passWord = PasswordEntry.Text;

        if(string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(passWord))
        {
            await DisplayAlert("Error", "You have to enter a username and password", "OK");
            return;
        }

        if(userName == "Admin" &&  passWord == "admin123")
        {
            var userSession = UserSession.GetUserSession();
            userSession.LogIn(userName, passWord, true);
            await Navigation.PopToRootAsync();
        }
        else
        {
            // Not admin
            var userSession = UserSession.GetUserSession();
            userSession.LogIn(userName, passWord, false);
            await Navigation.PopToRootAsync();
        }
    }

    private async void OnClickedGoBack(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}