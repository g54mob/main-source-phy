using BitCode.Graphics;

namespace BitCode.Users
{
	public interface IUserAccount
	{
		ulong? OnlineAccountId { get; }

		IUserAccountProperty<string> Name { get; }

		IUserAccountProperty<ImageData> AvatarImage { get; }

		IUserAccountProperty<string> Presence { get; }

		IUserAccountProperty<UserAccountOnlineStatus> OnlineStatus { get; }
	}
}
