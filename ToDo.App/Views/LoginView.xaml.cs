using ToDo.App.ViewModels;

namespace ToDo.App.Views;

public partial class LoginView : ContentPage
{
    private readonly LoginViewModel viewModel;

    public LoginView(LoginViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
        this.viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        await viewModel.OnAppearing();
    }
}