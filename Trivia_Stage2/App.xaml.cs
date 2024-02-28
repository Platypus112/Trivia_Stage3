using Trivia_Stage2.ViewModels;
namespace Trivia_Stage2

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