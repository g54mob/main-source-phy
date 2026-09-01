using System;
using System.Runtime.CompilerServices;
using BitCode.IO;
using BitCode.Platform.Steamworks.Dlc;
using BitCode.Platform.Steamworks.Networking;
using JetBrains.Annotations;

namespace BitCode.Platform.Steamworks
{
	public class SteamPlatformServices : DisposablePlatformServices
	{
		[CompilerGenerated]
		private readonly SteamService rHVqWivvTgVqxiGczgSyjmfuEQQB;

		public SteamService SteamApiService
		{
			[CompilerGenerated]
			get
			{
				return rHVqWivvTgVqxiGczgSyjmfuEQQB;
			}
		}

		internal SteamPlatformServices([NotNull] SteamService P_0, [NotNull] SteamLocalAccountManager P_1, ISaveDataManager P_2, SteamDlcManager P_3, SteamFriendManager P_4, SteamMultiplayerSessionManager P_5, SteamGameInvitationManager P_6, SteamAchievementManager P_7, SteamLanguageProvider P_8)
			: base(P_1, P_2, P_3, P_4, P_5, P_6, P_7, P_8)
		{
			rHVqWivvTgVqxiGczgSyjmfuEQQB = P_0;
			AddService(P_0);
		}

		public override IPermissionRuleManager<TGameFeature> GetPermissionRulesManager<TGameFeature>()
		{
			throw new NotImplementedException();
		}
	}
}
