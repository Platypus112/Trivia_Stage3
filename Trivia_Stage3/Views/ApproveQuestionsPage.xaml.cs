using Trivia_Stage3.ViewModels;

namespace Trivia_Stage3.Views;

public partial class ApproveQuestionsPage : ContentPage
{
	public ApproveQuestionsPage(ApproveQuestionsPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}