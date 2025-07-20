namespace Tradez.Maui
{
    public partial class App : IApplication
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
    }
}
