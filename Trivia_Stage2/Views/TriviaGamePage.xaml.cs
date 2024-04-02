using Trivia_Stage3.ViewModels;

namespace Trivia_Stage3.Views;

public partial class TriviaGamePage : ContentPage
{
	public TriviaGamePage(TriviaGamePageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}