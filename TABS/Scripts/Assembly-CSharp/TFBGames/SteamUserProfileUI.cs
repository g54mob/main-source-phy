using BitCode.Graphics;
using BitCode.Users;

namespace TFBGames
{
	public class SteamUserProfileUI : UserProfileUI
	{
		private ILocalAccount localUserAccount;

		protected override void Awake()
		{
			base.Awake();
			SteamPlatformUtils steamPlatformUtils = ServiceLocator.GetService<IPlatformUtils>() as SteamPlatformUtils;
			if (accountManager != null)
			{
				localUserAccount = accountManager.ActiveAccount;
			}
			IUserAccountProperty<ImageData> avatarImage = localUserAccount.AvatarImage;
			profilePicture.sprite = steamPlatformUtils?.CreateSpriteFromImageData(avatarImage.Value);
		}
	}
}
