using System.Threading.Tasks;
using Domain.Entities.Models.MongoDbModels;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;
using WildQ.Presentation.Views;

namespace WildQ.Presentation
{
    public partial class MainPage : ContentPage
    {
        // Not using --------------------
        IAnimalService _animalService; 
        ISearchAnimalService _searchAnimalService;

        // Constructor --------------------------------------------------------
        public MainPage(IAnimalService animalService, ISearchAnimalService searchAnimalService)
        {
            InitializeComponent();
            _animalService = animalService;
            _searchAnimalService = searchAnimalService;
        }

        // Will always show when MainPage runs --------------------------------
        protected override void OnAppearing() 
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
        
        // Clicks -----------------------------------------------------------
        private async void OnClickedSearchAnimal(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Views.SearchAnimalPage());
            await Shell.Current.GoToAsync(nameof(SearchAnimalPage));
        }

        private async void OnClickedEndangeredAnimalQuiz(object sender, EventArgs e)
        {
            var userSession = UserSession.GetUserSession();
            
            if(userSession.IsLoggedIn == false)
            {
                await DisplayAlert("Error", "You have to log in to get access to this page", "OK");
                return;
            }
            
            //await Navigation.PushAsync(new Views.EndangeredAnimalQuiz());
            await Shell.Current.GoToAsync(nameof(EndangeredAnimalQuizPage));
        }

        private async void OnClickedLogIn(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.LogInPage());
        }

        private async void OnClickedLoginAndLogout(object sender, EventArgs e)
        {
            var userSession = UserSession.GetUserSession();
            
            if (userSession.IsLoggedIn == true) 
            {
                // If logged in - the button logs out the user
                userSession.LogOut();

                LoginAndLogoutButton.Text = "Login";
                WelcomeTextLabel.Text = "Welcome!";
            }
            else
            {
                // If logged out the button sends the user to LoginPage
                await Navigation.PushAsync(new Views.LogInPage()); 
            }
        }
    }
}
