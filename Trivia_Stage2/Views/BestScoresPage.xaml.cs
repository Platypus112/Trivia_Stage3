using Trivia_Stage2.ViewModels;

namespace Trivia_Stage2.Views;

public partial class BestScoresPage : ContentPage
{
	public BestScoresPage(BestScoresPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }

    
}