using Trivia_Stage2.ViewModels;

namespace Trivia_Stage2.Views;

public partial class UserAdminPage : ContentPage
{
	public UserAdminPage(UserAdminPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}