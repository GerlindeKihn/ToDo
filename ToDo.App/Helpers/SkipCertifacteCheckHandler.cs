namespace ToDo.App.Helpers;

internal class SkipCertifacteCheckHandler : HttpClientHandler
{
	public SkipCertifacteCheckHandler()
	{
		ServerCertificateCustomValidationCallback = (_,_,_,_) => true;
	}
}
