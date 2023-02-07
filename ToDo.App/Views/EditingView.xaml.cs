using ToDo.App.ViewModels;

namespace ToDo.App.Views;

public partial class EditingView : ContentPage
{
	public EditingView(EditingViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;

    }
}