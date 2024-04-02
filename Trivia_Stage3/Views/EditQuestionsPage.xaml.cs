using Trivia_Stage3.ViewModels;

namespace Trivia_Stage3.Views;

public partial class EditQuestionsPage : ContentPage
{
	public EditQuestionsPage(EditQuestionsPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}