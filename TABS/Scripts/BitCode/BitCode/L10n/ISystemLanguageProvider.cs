namespace BitCode.L10n
{
	public interface ISystemLanguageProvider : IPlatformService
	{
		string GetLanguageCode();
	}
}
