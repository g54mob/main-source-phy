using System;
using System.Threading.Tasks;
using BitCode.Networking;
using BitCode.Platform.Steamworks.Networking;
using BitCode.Users;
using UnityEngine;

namespace TFBGames
{
	public class SteamGameInvitationService : BaseGameInvitationService
	{
		public SteamGameInvitationService(bool bufferInvites = true)
			: base(bufferInvites)
		{
		}

		public override void OnAwake()
		{
			base.OnAwake();
			(gameInvitationManager as SteamGameInvitationManager)?.CheckForLaunchInvite(Application.isEditor);
		}

		public override IGameInvitation CreateInviteToMultiplayerSession(IMultiplayerSession session, byte[] applicationData = null)
		{
			return new SteamGameInvitation(new SteamMultiplayerSessionInfo(), applicationData);
		}

		public override void SendGameInviteAsync(ILocalAccount user, int maxInvitees, IGameInvitation invitation, Action<bool, Exception> sentCallback)
		{
			throw new NotImplementedException();
		}

		public override Task<bool> SendGameInviteAsync(ILocalAccount user, int maxInvitees, IGameInvitation invitation)
		{
			throw new NotImplementedException();
		}
	}
}
