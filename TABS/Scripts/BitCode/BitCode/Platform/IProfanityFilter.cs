namespace BitCode.Platform
{
	public interface IProfanityFilter : IPlatformService
	{
		string FilterString(string inputString, string language);
	}
}
