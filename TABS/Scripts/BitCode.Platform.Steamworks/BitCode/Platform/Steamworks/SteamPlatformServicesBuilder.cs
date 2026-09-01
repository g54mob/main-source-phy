using System;
using System.Threading.Tasks;
using BitCode.IO;
using BitCode.Platform.Steamworks.Dlc;
using BitCode.Platform.Steamworks.Networking;
using JetBrains.Annotations;
using Steamworks;

namespace BitCode.Platform.Steamworks
{
	public class SteamPlatformServicesBuilder : IPlatformServicesBuilder
	{
		private readonly IServiceUpdater UiqWRGkuDLeBUJrcKfNWbgaWARxT;

		private readonly AppId_t MTushcWpGUYeOzSLkRDicrnRckGK;

		private readonly bool GIliECyfRGUnpgSGjiCODNyrZfcD;

		private IIOWrapper fcFcXjILWOuMCzoGnoHzVdbpOAQM;

		private string nkhjRWxzTvzVyDqXmxxVDguISDci;

		private bool OZKTljYUjaafLhhuxYQIKjZjHyWi;

		private bool ZbjbxEITrwBeLXDtZtfKDgjGWsjr;

		private bool eZicQbAJlBAUqABdNtUBtxibTHQnA;

		private bool zbiaoNhaGqCMEriHAOCDHfusMLjU;

		private bool KFIscVYSOkqmaVpebVTqyajgKbpJ;

		private bool aUOfVLHAtamiJePHeuAojfFTlEiEA;

		public SteamPlatformServicesBuilder([NotNull] IServiceUpdater serviceUpdater, AppId_t appId, bool useSteamUILanguage = false)
		{
			while (true)
			{
				int num = -1444806741;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1679518850)) % 5)
					{
					case 2u:
						break;
					default:
						return;
					case 3u:
						GIliECyfRGUnpgSGjiCODNyrZfcD = useSteamUILanguage;
						num = ((int)num2 * -62253070) ^ 0x5EEE6DDD;
						continue;
					case 0u:
						MTushcWpGUYeOzSLkRDicrnRckGK = appId;
						num = ((int)num2 * -2134239771) ^ -1649606540;
						continue;
					case 1u:
						UiqWRGkuDLeBUJrcKfNWbgaWARxT = serviceUpdater;
						num = ((int)num2 * -408717479) ^ -814144828;
						continue;
					case 4u:
						return;
					}
					break;
				}
			}
		}

		public SteamPlatformServicesBuilder WithSave([NotNull] IIOWrapper ioWrapper, [NotNull] string basePath)
		{
			fcFcXjILWOuMCzoGnoHzVdbpOAQM = ioWrapper;
			while (true)
			{
				int num = 292489697;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x59475743)) % 4)
					{
					case 3u:
						break;
					case 2u:
						nkhjRWxzTvzVyDqXmxxVDguISDci = basePath;
						num = ((int)num2 * -2092725646) ^ -1574704834;
						continue;
					case 1u:
						OZKTljYUjaafLhhuxYQIKjZjHyWi = true;
						num = ((int)num2 * -40662912) ^ -1601860593;
						continue;
					default:
						return this;
					}
					break;
				}
			}
		}

		public SteamPlatformServicesBuilder WithSocial()
		{
			ZbjbxEITrwBeLXDtZtfKDgjGWsjr = true;
			return this;
		}

		public SteamPlatformServicesBuilder WithInvites()
		{
			eZicQbAJlBAUqABdNtUBtxibTHQnA = true;
			return this;
		}

		public SteamPlatformServicesBuilder WithMultiplayer()
		{
			throw new NotImplementedException();
		}

		public SteamPlatformServicesBuilder WithDlc()
		{
			KFIscVYSOkqmaVpebVTqyajgKbpJ = true;
			return this;
		}

		public SteamPlatformServicesBuilder WithAchievements()
		{
			aUOfVLHAtamiJePHeuAojfFTlEiEA = true;
			return this;
		}

		public Task<IPlatformServices> Build()
		{
			SteamService steamService = new SteamService(UiqWRGkuDLeBUJrcKfNWbgaWARxT, MTushcWpGUYeOzSLkRDicrnRckGK);
			SteamLocalAccountManager steamLocalAccountManager = new SteamLocalAccountManager(steamService);
			SteamLanguageProvider steamLanguageProvider = new SteamLanguageProvider(GIliECyfRGUnpgSGjiCODNyrZfcD);
			SteamFriendManager steamFriendManager = default(SteamFriendManager);
			SteamMultiplayerSessionManager steamMultiplayerSessionManager = default(SteamMultiplayerSessionManager);
			SteamGameInvitationManager steamGameInvitationManager = default(SteamGameInvitationManager);
			SteamDlcManager steamDlcManager = default(SteamDlcManager);
			ISaveDataManager saveDataManager = default(ISaveDataManager);
			while (true)
			{
				int num = -1908090456;
				while (true)
				{
					uint num2;
					object obj2;
					object obj3;
					object obj6;
					object obj;
					object obj5;
					object obj4;
					SteamAchievementManager steamAchievementManager;
					switch ((num2 = (uint)(num ^ -724314500)) % 10)
					{
					case 6u:
						break;
					case 5u:
						obj2 = null;
						goto IL_0071;
					case 9u:
						obj3 = null;
						goto IL_008b;
					case 0u:
						obj6 = null;
						goto IL_00af;
					case 4u:
						if (!aUOfVLHAtamiJePHeuAojfFTlEiEA)
						{
							num = (int)(num2 * 1697641507) ^ -1324856733;
							continue;
						}
						obj = new SteamAchievementManager(steamService, UiqWRGkuDLeBUJrcKfNWbgaWARxT);
						goto IL_015c;
					case 1u:
						obj5 = null;
						goto IL_00e2;
					case 2u:
						obj4 = null;
						goto IL_00f8;
					case 8u:
						if (ZbjbxEITrwBeLXDtZtfKDgjGWsjr)
						{
							obj3 = new SteamFriendManager(steamService);
							goto IL_008b;
						}
						num = (int)(num2 * 77721607) ^ -99195907;
						continue;
					case 7u:
						if (KFIscVYSOkqmaVpebVTqyajgKbpJ)
						{
							obj2 = new SteamDlcManager(steamService);
							goto IL_0071;
						}
						num = (int)(num2 * 846577550) ^ -53704591;
						continue;
					default:
						{
							obj = null;
							goto IL_015c;
						}
						IL_008b:
						steamFriendManager = (SteamFriendManager)obj3;
						if (!eZicQbAJlBAUqABdNtUBtxibTHQnA)
						{
							num = -1820143638;
							continue;
						}
						obj4 = new SteamGameInvitationManager(steamService, steamLocalAccountManager);
						goto IL_00f8;
						IL_00e2:
						steamMultiplayerSessionManager = (SteamMultiplayerSessionManager)obj5;
						num = -934440279;
						continue;
						IL_00f8:
						steamGameInvitationManager = (SteamGameInvitationManager)obj4;
						if (zbiaoNhaGqCMEriHAOCDHfusMLjU)
						{
							obj5 = new SteamMultiplayerSessionManager(steamService);
							goto IL_00e2;
						}
						num = -394386393;
						continue;
						IL_0071:
						steamDlcManager = (SteamDlcManager)obj2;
						if (!OZKTljYUjaafLhhuxYQIKjZjHyWi)
						{
							num = -643500668;
							continue;
						}
						obj6 = new SimpleSaveDataManager(fcFcXjILWOuMCzoGnoHzVdbpOAQM, nkhjRWxzTvzVyDqXmxxVDguISDci);
						goto IL_00af;
						IL_00af:
						saveDataManager = (ISaveDataManager)obj6;
						num = -649783346;
						continue;
						IL_015c:
						steamAchievementManager = (SteamAchievementManager)obj;
						return Task.FromResult((IPlatformServices)new SteamPlatformServices(steamService, steamLocalAccountManager, saveDataManager, steamDlcManager, steamFriendManager, steamMultiplayerSessionManager, steamGameInvitationManager, steamAchievementManager, steamLanguageProvider));
					}
					break;
				}
			}
		}
	}
}
