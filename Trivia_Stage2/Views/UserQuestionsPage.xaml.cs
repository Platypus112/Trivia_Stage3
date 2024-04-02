using Trivia_Stage3.ViewModels;

namespace Trivia_Stage3.Views;

public partial class UserQuestionsPage : ContentPage
{
	public UserQuestionsPage(UserQuestionsPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}