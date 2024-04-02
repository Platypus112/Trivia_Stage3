namespace Trivia_Stage3.Views;
using Trivia_Stage3.ViewModels;
public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}