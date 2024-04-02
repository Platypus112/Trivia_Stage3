using Trivia_Stage3.ViewModels;

namespace Trivia_Stage3.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}