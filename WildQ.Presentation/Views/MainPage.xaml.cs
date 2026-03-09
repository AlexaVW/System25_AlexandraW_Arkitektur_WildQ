using System.Threading.Tasks;
using Domain.Entities.Models.MongoDbModels;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation
{
    public partial class MainPage : ContentPage
    {
        IAnimalService _animalService;
        ISearchAnimalService _searchAnimalService;
        public MainPage(IAnimalService animalService, ISearchAnimalService searchAnimalService)
        {
            InitializeComponent();
            _animalService = animalService;
            _searchAnimalService = searchAnimalService;

            
        }

        protected override void OnAppearing() //Will always run when mainpage runs
        {
            base.OnAppearing();
            
            var userSession = UserSession.GetUserSession();
            if (userSession.IsLoggedIn)
            {
                string name = userSession.UserName;
                WelcomeTextLabel.Text = "Welcome " + name + "!"; // If logged in
                LoginAndLogoutButton.Text = "Logout";
            }
            else
            {
                WelcomeTextLabel.Text = "Welcome!"; // If logged out
                LoginAndLogoutButton.Text = "Login";
            }

        }
        private async void OnClickedSearchAnimal(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.SearchAnimalPage());
        }

        private async void OnClickedEndangeredAnimalQuiz(object sender, EventArgs e)
        {
            var userSession = UserSession.GetUserSession();
            
            if(userSession.IsLoggedIn == false)
            {
                await DisplayAlert("Error", "You have to log in to get access to this page", "OK");
                return;
            }
            
            await Navigation.PushAsync(new Views.EndangeredAnimalQuiz());
        }

        private async void OnClickedLogIn(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.LogInPage());
        }

        private async void OnClickedLoginAndLogout(object sender, EventArgs e)
        {
            var userSession = UserSession.GetUserSession();

            if (userSession.IsLoggedIn == true) // If logged in the button logs out the user
            {
                userSession.LogOut();

                LoginAndLogoutButton.Text = "Login";
                WelcomeTextLabel.Text = "Welcome!";

            }
            else
            {
                await Navigation.PushAsync(new Views.LogInPage()); //If logged out the button sends the user to loginpage
            }
        }
    }
}
