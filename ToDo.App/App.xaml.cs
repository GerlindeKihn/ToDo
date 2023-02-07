using ToDo.App.Interfaces;
using ToDo.App.Views;

namespace ToDo.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute(nameof(EditingView), typeof(EditingView));
        }
    }
}