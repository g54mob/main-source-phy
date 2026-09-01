using BitCode.Users;
using Landfall.TABS;
using UnityEngine;

namespace TFBGames
{
	public static class ProjectMarsHelpers
	{
		public static Sprite GetSpriteFromUser(IUserAccount account)
		{
			Sprite result = null;
			if (account.AvatarImage.Status != UserAccountPropertyStatus.Loaded)
			{
				return null;
			}
			if (ServiceLocator.GetService<IPlatformUtils>() is PlatformImageHandling platformImageHandling)
			{
				result = platformImageHandling.CreateSpriteFromImageData(account.AvatarImage.Value);
			}
			return result;
		}

		public static PlayerProfile ProfileFromUserAccount(IUserAccount account, object customData = null)
		{
			PlayerProfile result = null;
			if (account != null)
			{
				bool num = account.Name.Status == UserAccountPropertyStatus.Loaded;
				bool flag = account.OnlineStatus.Status == UserAccountPropertyStatus.Loaded;
				string playerName = (num ? account.Name.Value : "[NAME NOT LOADED]");
				string statusString = (flag ? account.OnlineStatus.Value.ToString() : "(???)");
				Sprite spriteFromUser = GetSpriteFromUser(account);
				result = new PlayerProfile(playerName, spriteFromUser, statusString, Team.Red, customData);
			}
			return result;
		}
	}
}
