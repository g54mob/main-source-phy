using JetBrains.Annotations;

namespace BitCode.Networking
{
	public interface IGameInvitation
	{
		IMultiplayerSessionInfo SessionInfo { get; }

		[CanBeNull]
		byte[] ApplicationData { get; }
	}
}
