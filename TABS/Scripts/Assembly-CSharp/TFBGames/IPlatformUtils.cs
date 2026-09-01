namespace TFBGames
{
	public interface IPlatformUtils : IService
	{
		bool IsUIOpenOrLostFocus { get; }

		bool IsRunningInBackground { get; }
	}
}
