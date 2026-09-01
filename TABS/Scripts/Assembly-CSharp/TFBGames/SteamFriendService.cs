using System;
using BitCode.Users;

namespace TFBGames
{
	public class SteamFriendService : BaseFriendService
	{
		public override void GetFriendListAsync(ILocalAccount user, Action<IRemoteAccount[], Exception> callback)
		{
			if (!base.FriendManager.IsInitializedForUser(user))
			{
				base.FriendManager.InitializeForUser(user);
			}
			base.FriendManager.GetFriendListAsync(user, callback);
		}
	}
}
