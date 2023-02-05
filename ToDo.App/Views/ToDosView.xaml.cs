using ToDo.App.ViewModels;

namespace ToDo.App.Views;

public partial class ToDosView : ContentPage
{
	public ToDosView(ToDosViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		
		viewModel.GetToDos.Execute(this);
	}
}