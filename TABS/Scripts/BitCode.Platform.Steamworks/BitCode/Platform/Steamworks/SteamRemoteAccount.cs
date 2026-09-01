using BitCode.Users;
using Steamworks;

namespace BitCode.Platform.Steamworks
{
	public class SteamRemoteAccount : SteamUserAccount, IUserAccount, IRemoteAccount
	{
		public SteamRemoteAccount(CSteamID steamId, SteamService steamService)
			: base(steamId, steamService)
		{
		}

		public override void UpdateName()
		{
			CheckDisposed();
			base.Name.SetValue(SteamFriends.GetFriendPersonaName(base.SteamId));
		}

		public override void UpdateOnlineStatus()
		{
			CheckDisposed();
			base.OnlineStatus.SetValue(Utilities.ConvertToOnlineStatus(SteamFriends.GetFriendPersonaState(base.SteamId)));
		}
	}
}
