using System.Text;

namespace ToDo.App.Helpers;

internal class AuthenticationHandler : DelegatingHandler
{
    private readonly ISecureStorage secureStorage;
    public AuthenticationHandler(ISecureStorage secureStorage) =>
        this.secureStorage = secureStorage;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        string authHeader = await secureStorage.GetAsync("AuthHeader");

        if (authHeader is not null)
            request.Headers.Authorization = new("Basic", authHeader);

        return await base.SendAsync(request, cancellationToken);
    }
}