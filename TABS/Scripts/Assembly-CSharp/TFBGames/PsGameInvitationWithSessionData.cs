using System;
using BitCode.Networking;

namespace TFBGames
{
	public struct PsGameInvitationWithSessionData : IGameInvitation
	{
		public string BoltSessionId;

		public string RegionCode;

		public IMultiplayerSessionInfo SessionInfo { get; set; }

		public byte[] ApplicationData
		{
			get
			{
				throw new NotSupportedException();
			}
		}
	}
}
