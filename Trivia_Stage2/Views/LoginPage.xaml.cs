namespace Trivia_Stage2.Views;
using Trivia_Stage2.ViewModels;
public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}