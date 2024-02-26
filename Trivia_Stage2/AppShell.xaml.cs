using Trivia_Stage2.Views;
namespace Trivia_Stage2
    
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("ApproveQuestionsPage",typeof(ApproveQuestionsPage));
            Routing.RegisterRoute("BestScoresPage", typeof(BestScoresPage));
            Routing.RegisterRoute("UserAdminPage", typeof(UserAdminPage));
            Routing.RegisterRoute("UserQuestionsPage", typeof(UserQuestionsPage));
        }
    }
}