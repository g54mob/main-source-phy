using System;
using BitCode.Dlc;
using BitCode.ErrorHandling;
using BitCode.IO;
using BitCode.L10n;
using BitCode.Networking;
using BitCode.Platform;
using BitCode.Users;

namespace BitCode
{
	public abstract class DisposablePlatformServicesWithPermissions<TPlatformPermission> : DisposablePlatformServices
	{
		private readonly IPlatformPermissionRuleManager<TPlatformPermission> permissionRuleManager;

		internal DisposablePlatformServicesWithPermissions(ILocalAccountManager localAccountManager = null, ISaveDataManager saveDataManager = null, IDlcManager dlcManager = null, IFriendManager friendManager = null, IMultiplayerSessionManager multiplayerSessionManager = null, IGameInvitationManager gameInvitationManager = null, IAchievementManager achievementManager = null, ISystemLanguageProvider languageProvider = null, IProfanityFilter profanityFilter = null, IPopupDialog popupDialog = null, IVirtualKeyboard virtualKeyboard = null, ExceptionHandlingService exceptionHandlingService = null, IPlatformPermissionRuleManager<TPlatformPermission> permissionRuleManager = null)
			: base(localAccountManager, saveDataManager, dlcManager, friendManager, multiplayerSessionManager, gameInvitationManager, achievementManager, languageProvider, profanityFilter, popupDialog, virtualKeyboard, exceptionHandlingService)
		{
			this.permissionRuleManager = permissionRuleManager;
			AddService(permissionRuleManager);
		}

		public override IPermissionRuleManager<TGameFeature> GetPermissionRulesManager<TGameFeature>()
		{
			if (permissionRuleManager == null)
			{
				while (true)
				{
					uint num;
					switch ((num = 1952082908u) % 3)
					{
					case 0u:
						continue;
					case 2u:
						return null;
					}
					break;
				}
			}
			PermissionRuleManager<TGameFeature, TPlatformPermission> obj = permissionRuleManager as PermissionRuleManager<TGameFeature, TPlatformPermission>;
			if (obj == null)
			{
				Type typeFromHandle = typeof(TGameFeature);
				throw new InvalidOperationException($"The given game permission type {typeFromHandle.FullName} is not supported.");
			}
			return obj;
		}
	}
}
