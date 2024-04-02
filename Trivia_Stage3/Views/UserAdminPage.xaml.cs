using Trivia_Stage3.ViewModels;

namespace Trivia_Stage3.Views;

public partial class UserAdminPage : ContentPage
{
	public UserAdminPage(UserAdminPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}