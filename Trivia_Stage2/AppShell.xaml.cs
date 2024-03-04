using Trivia_Stage2.ViewModels;
using Trivia_Stage2.Views;
namespace Trivia_Stage2
    
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute("ApproveQuestionsPage",typeof(ApproveQuestionsPage));
            Routing.RegisterRoute("BestScoresPage", typeof(BestScoresPage));
            Routing.RegisterRoute("UserAdminPage", typeof(UserAdminPage));
            Routing.RegisterRoute("UserQuestionsPage", typeof(UserQuestionsPage));
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("EditQuestionsPage",typeof(EditQuestionsPage));
            InitializeComponent();
            
        }
    }
}