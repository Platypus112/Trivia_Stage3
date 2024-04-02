using Trivia_Stage3.ViewModels;
using Trivia_Stage3.Views;
namespace Trivia_Stage3
    
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
            //LoginPage, UserAdminPage - Idan
            //ApproveQuestionsPage - Lynn 
            //BestScoresPage - Aviv
            //UserQuestionsPage - Ofek
        }
    }
}
