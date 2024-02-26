using Trivia_Stage2.ViewModels;

namespace Trivia_Stage2.Views;

public partial class MenuPage : ContentPage
{
	public MenuPage(MenuPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext= vm;
	}
}