namespace TFBGames
{
	public interface INetworkQuitController : IService
	{
		bool DidQuit { get; }

		bool DidOpponentQuit { get; }

		NetworkQuitControllerDialogState DialogState { get; }

		void QuitMultiplayerGame(bool loadMainMenu = true);
	}
}
