using Trivia_Stage2.ViewModels;

namespace Trivia_Stage2.Views;

public partial class UserQuestionsPage : ContentPage
{
	public UserQuestionsPage(UserQuestionsPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}