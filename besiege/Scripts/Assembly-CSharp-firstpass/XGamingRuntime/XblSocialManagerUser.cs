using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblSocialManagerUser
	{
		public ulong XboxUserId { get; private set; }

		public bool IsFavorite { get; private set; }

		public bool IsFollowingUser { get; private set; }

		public bool IsFollowedByCaller { get; private set; }

		public string DisplayName { get; private set; }

		public string RealName { get; private set; }

		public string DisplayPicUrlRaw { get; private set; }

		public bool UseAvatar { get; private set; }

		public string Gamerscore { get; private set; }

		public string Gamertag { get; private set; }

		public string ModernGamertag { get; private set; }

		public string ModernGamertagSuffix { get; private set; }

		public string UniqueModernGamertag { get; private set; }

		public XblSocialManagerPresenceRecord PresenceRecord { get; private set; }

		public XblTitleHistory TitleHistory { get; private set; }

		public XblPreferredColor PreferredColor { get; private set; }

		internal XblSocialManagerUser(XGamingRuntime.Interop.XblSocialManagerUser interopUser)
		{
			XboxUserId = interopUser.xboxUserId;
			IsFavorite = interopUser.isFavorite;
			IsFollowingUser = interopUser.isFollowingUser;
			IsFollowedByCaller = interopUser.isFollowedByCaller;
			DisplayName = Converters.ByteArrayToString(interopUser.displayName);
			RealName = Converters.ByteArrayToString(interopUser.realName);
			DisplayPicUrlRaw = Converters.ByteArrayToString(interopUser.displayPicUrlRaw);
			UseAvatar = interopUser.useAvatar;
			Gamerscore = Converters.ByteArrayToString(interopUser.gamerscore);
			Gamertag = Converters.ByteArrayToString(interopUser.gamertag);
			ModernGamertag = Converters.ByteArrayToString(interopUser.modernGamertag);
			ModernGamertagSuffix = Converters.ByteArrayToString(interopUser.modernGamertagSuffix);
			UniqueModernGamertag = Converters.ByteArrayToString(interopUser.uniqueModernGamertag);
			PresenceRecord = new XblSocialManagerPresenceRecord(interopUser.presenceRecord);
			TitleHistory = new XblTitleHistory(interopUser.titleHistory);
			PreferredColor = new XblPreferredColor(interopUser.preferredColor);
		}
	}
}
