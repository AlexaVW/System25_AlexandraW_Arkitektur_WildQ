using WildQ.Presentation.Views;

namespace WildQ.Presentation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SearchAnimalPage), typeof(SearchAnimalPage));

            Routing.RegisterRoute(nameof(EndangeredAnimalQuizPage), typeof(EndangeredAnimalQuizPage));
        }
    }
}
