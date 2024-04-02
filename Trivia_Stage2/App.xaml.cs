using Trivia_Stage3.ViewModels;
namespace Trivia_Stage3

{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}