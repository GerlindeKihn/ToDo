using System.Text;
using ToDo.App.Interfaces;

namespace ToDo.App.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly ISecureStorage secureStorage;

    public LoginViewModel(
        ISecureStorage secureStorage,
        IUserApiClient userApiClient)
    {
        this.secureStorage = secureStorage;

        Login = new(async () =>
        {
            if (!ShouldBeEnabled) return;

            try
            {
                await secureStorage.SetAsync("AuthHeader", AuthHeader);
                await userApiClient.GetCurrent();

                Application.Current!.MainPage = new AppShell();
            }
            catch(Exception ex)
            {
                secureStorage.Remove("AuthHeader");
                ErrorVisible = true;
            }
        });
    }

    public async Task OnAppearing()
    {
        var authHeader = await secureStorage.GetAsync("AuthHeader");
        if (authHeader is null) return;

        Application.Current!.MainPage = new AppShell();
    }

    private string username;
    public string Username
    {
        get { return username; }
        set { SetProperty(ref username, value); }
    }

    private string password;
    public string Password
    {
        get { return password; }
        set { SetProperty(ref password, value); }
    }

    private bool errorVisible;
    public bool ErrorVisible
    {
        get { return errorVisible; }
        set { SetProperty(ref errorVisible, value); }
    }

    private string AuthHeader => Convert.ToBase64String(
        Encoding.UTF8.GetBytes(username + ':' + password));

    private bool ShouldBeEnabled =>
        !string.IsNullOrEmpty(username) &&
        !string.IsNullOrEmpty(password);

    public Command Login { get; }
}