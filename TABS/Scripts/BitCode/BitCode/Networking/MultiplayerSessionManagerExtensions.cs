namespace BitCode.Networking
{
	public static class MultiplayerSessionManagerExtensions
	{
		public static bool IsSessionActive(this IMultiplayerSessionManager multiplayerSessionManager)
		{
			return multiplayerSessionManager.ActiveSession != null;
		}
	}
}
