using System;
using BitCode.Graphics;
using BitCode.Users;

namespace TFBGames
{
	public class GameCoreOnlineAccountIdWrapper : IRemoteAccount, IUserAccount
	{
		public ulong? OnlineAccountId { get; private set; }

		public IUserAccountProperty<string> Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IUserAccountProperty<ImageData> AvatarImage
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IUserAccountProperty<string> Presence
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IUserAccountProperty<UserAccountOnlineStatus> OnlineStatus
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public GameCoreOnlineAccountIdWrapper(ulong accountId)
		{
			OnlineAccountId = accountId;
		}
	}
}
