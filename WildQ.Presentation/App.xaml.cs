namespace WildQ.Presentation
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {
            InitializeComponent();

            Microsoft.Maui.Controls.Application.Current.UserAppTheme = AppTheme.Light; // Forcing lightmode
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}