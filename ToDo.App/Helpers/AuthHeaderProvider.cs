using System.Text;

namespace ToDo.App.Helpers;

internal static class AuthHeaderProvider
{
    private const string AuthHeader = "AuthHeader";

    public static Task<string> GetAuthHeader()
    {
        const string Username = "Gerlinde";
        const string Password = "Password";

        byte[] AuthHeaderBytes = Encoding.UTF8.GetBytes(Username + ':' + Password);
        string AuthHeader = Convert.ToBase64String(AuthHeaderBytes);

        return Task.FromResult(AuthHeader);
        //return SecureStorage.Default.GetAsync(AuthHeader);
    }

    public static Task SetAuthHeader(string authHeader) =>
       SecureStorage.Default.SetAsync(AuthHeader, authHeader);
}
