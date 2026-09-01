using System.Collections.Generic;
using BitCode.Networking;

namespace TFBGames
{
	public sealed class EpicGameInvitationWithSessionData : IGameInvitation
	{
		public IDictionary<string, string> CustomData { get; set; }

		public IMultiplayerSessionInfo SessionInfo { get; set; }

		public byte[] ApplicationData { get; }
	}
}
