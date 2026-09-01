using System;
using BitCode.Dlc;
using BitCode.ErrorHandling;
using BitCode.IO;
using BitCode.L10n;
using BitCode.Networking;
using BitCode.Platform;
using BitCode.Users;
using JetBrains.Annotations;

namespace BitCode
{
	public interface IPlatformServices
	{
		[CanBeNull]
		ILocalAccountManager LocalAccountManager { get; }

		[CanBeNull]
		ISaveDataManager SaveDataManager { get; }

		[CanBeNull]
		IDlcManager DlcManager { get; }

		[CanBeNull]
		IMultiplayerSessionManager MultiplayerSessionManager { get; }

		[CanBeNull]
		IFriendManager FriendManager { get; }

		[CanBeNull]
		IGameInvitationManager GameInvitationManager { get; }

		[CanBeNull]
		IAchievementManager AchievementManager { get; }

		[CanBeNull]
		ISystemLanguageProvider LanguageProvider { get; }

		[CanBeNull]
		IProfanityFilter ProfanityFilter { get; }

		[CanBeNull]
		IPopupDialog PopupDialog { get; }

		[CanBeNull]
		IVirtualKeyboard VirtualKeyboard { get; }

		[CanBeNull]
		ExceptionHandlingService ExceptionHandlingService { get; }

		event Action<IPlatformService, Exception> InternalErrorOccurred;

		[CanBeNull]
		IPermissionRuleManager<TGameFeature> GetPermissionRulesManager<TGameFeature>();
	}
}
