namespace BitCode.Networking
{
	public interface IMultiplayerSession
	{
		IMultiplayerSessionInfo SessionInfo { get; }

		bool CanSendInvites { get; }
	}
}
