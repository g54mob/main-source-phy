namespace TFBGames
{
	public interface INetworkUserAuthenticator : IService
	{
		void AuthenticateUserAsync(string regionCode, AuthenticateUserCallback callback);
	}
}
