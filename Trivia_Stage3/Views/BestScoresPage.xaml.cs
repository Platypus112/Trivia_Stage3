using Trivia_Stage3.ViewModels;

namespace Trivia_Stage3.Views;

public partial class BestScoresPage : ContentPage
{
	public BestScoresPage(BestScoresPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }

    
}