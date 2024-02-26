using Trivia_Stage2.ViewModels;

namespace Trivia_Stage2.Views;

public partial class ApproveQuestionsPage : ContentPage
{
	public ApproveQuestionsPage(ApproveQuestionsPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}