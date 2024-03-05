using Trivia_Stage2.ViewModels;

namespace Trivia_Stage2.Views;

public partial class EditQuestionsPage : ContentPage
{
	public EditQuestionsPage(EditQuestionsPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}