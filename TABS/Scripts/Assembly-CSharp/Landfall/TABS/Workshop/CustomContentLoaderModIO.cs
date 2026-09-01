using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitCode.Users;
using DM;
using LevelCreator;
using ModIO;
using Steamworks;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.Workshop
{
	public class CustomContentLoaderModIO : ServicePrefab
	{
		private struct ReadAsyncData
		{
			public FileInfo Info;

			public int ModID;

			public ReadAsyncDataType Type;
		}

		private struct RefreshItem
		{
			public bool IsQuickRefresh { get; }

			public WorkshopContentType? QuickRefreshType { get; }

			public bool HasPermissionToLoadMods { get; }

			public System.Action DoneCallback { get; }

			public RefreshItem(bool isQuickRefresh, WorkshopContentType? quickRefreshType, bool hasPermissionToLoadMods, System.Action doneCallback)
			{
				IsQuickRefresh = isQuickRefresh;
				QuickRefreshType = quickRefreshType;
				HasPermissionToLoadMods = hasPermissionToLoadMods;
				DoneCallback = doneCallback;
			}
		}

		private enum ReadAsyncDataType
		{
			Unknown = 0,
			Layout = 1,
			Campaign = 2,
			Faction = 3,
			Unit = 4,
			Map = 5
		}

		public delegate void CheckPermissionToLoadModsCallback(bool didGivePermissionToLoadMods);

		private const int RefreshQueueCapacity = 10;

		private static Dictionary<WorkshopContentType, ModProfile[]> LocalUserMods;

		private Stack<LoadedCustomFactionWrapper> m_LoadedCustomFactions;

		private Stack<LoadedCustomCampaignWrapper> m_loadedCustomCampaigns;

		private Stack<LoadedCustomLayoutWrapper> m_loadedCustomCampaignLevels;

		private Stack<LoadedCustomUnitWrapper> m_loadedCustomUnits;

		private Stack<LoadedCustomMapWrapper> m_loadedCustomMaps;

		private List<int> m_DownloadingList;

		private Dictionary<int, ILoadedCustomContent[]> m_UGCDetailsDictionary;

		private LocalCustomContentLoader m_LocalCustomContentLoader;

		private WaitForStorage m_WaitForStorage;

		private AccountManager m_AccountManager;

		private static int userId = -1;

		private static System.Action m_OnLoginSuccessActionOnce;

		private IPlayerPrefsPlatform m_PlayerPrefs;

		private CallResult<EncryptedAppTicketResponse_t> m_EncryptedTicketResponse;

		private SteamManager m_steamManager;

		private bool m_Inited;

		private FileIOWrapper m_FileIO;

		private ModalPanel m_ModalPanel;

		private IModIOUserAuthenticator m_ModIOUserAuthenticator;

		private int m_BusyAuthenticatingUserCount;

		private readonly Queue<RefreshItem> m_RefreshQueue = new Queue<RefreshItem>(10);

		private bool m_IsProcessingRefreshQueueItem;

		private bool m_DidDoFullRefresh;

		private int m_QuickRefreshCount;

		private bool hasCheckedUserAuthenticated;

		public static ModRating[] LocalUserRatings { get; private set; }

		public static GameProfile ModIOGameProfile { get; private set; }

		public static UserProfile LocalModIOUser { get; private set; }

		public static bool IsLoggedIn => LocalModIOUser != null;

		public string LocalModIOUserID
		{
			get
			{
				if (LocalModIOUser == null)
				{
					return "N/A";
				}
				return LocalModIOUser.id.ToString();
			}
		}

		public bool DidGivePermissionToLoadMods { get; private set; }

		public bool IsBusyAuthenticatingUser => m_BusyAuthenticatingUserCount > 0;

		public event System.Action ContentQuickRefreshed;

		public static void AddOnLoginSuccessAction(System.Action a)
		{
			m_OnLoginSuccessActionOnce = (System.Action)Delegate.Combine(m_OnLoginSuccessActionOnce, a);
		}

		public override void OnAwake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			UIScreenInputBlocker.DoBlockInput(open: false);
			m_WaitForStorage = ServiceLocator.GetService<WaitForStorage>();
			m_FileIO = ServiceLocator.GetService<FileIOWrapper>();
			m_AccountManager = ServiceLocator.GetService<AccountManager>();
			m_PlayerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
			m_ModalPanel = ServiceLocator.GetService<ModalPanel>();
			m_AccountManager.ActiveAccountChanged += OnActiveAccountChanged;
			m_steamManager = (SteamManager)ServiceLocator.GetService<IPlatformManager>();
			Debug.Log("Start Setup ModIO");
			m_WaitForStorage.FireWhenReady(SetupModIO);
			Debug.Log("End Setup ModIO");
		}

		public override void OnStart()
		{
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			UpdateRefreshQueue();
		}

		public override void UnRegister()
		{
			base.UnRegister();
			if (m_AccountManager != null)
			{
				m_AccountManager.ActiveAccountChanged -= OnActiveAccountChanged;
			}
		}

		private void OnActiveAccountChanged(ILocalAccount account)
		{
			CleanupUserContent();
			if (account != null)
			{
				InitializeForPlatform();
				OnStart();
			}
		}

		private void SetupModIO()
		{
			Debug.Log("Setting up ModIO Custom Content Loader");
			SetupReferences();
			SetupCallbacks();
			TryLogin();
			StartCoroutine(WaitForUserLogin());
		}

		public void LoginRuntime()
		{
			Debug.Log("Logging in runtime!");
			TryLogin();
			StartCoroutine(WaitForUserLogin());
		}

		private IEnumerator WaitForUserLogin()
		{
			float timeOut = 20f;
			float now = Time.unscaledTime;
			while (LocalModIOUser == null && !(Time.unscaledTime - now > timeOut))
			{
				yield return 0;
			}
			Init();
		}

		private void Init()
		{
			if (!IsLoggedIn)
			{
				if (ShouldRefreshOnInitWhenNotLoggedIn())
				{
					Refresh();
				}
			}
			else if (!m_Inited)
			{
				m_Inited = true;
				Debug.Log("Install Dir: " + DataStorage.INSTALLATION_DIRECTORY);
				FetchUserMods();
				FetchUserRatings();
				DownloadSubbedMods();
				GetGame();
			}
		}

		private bool ShouldRefreshOnInitWhenNotLoggedIn()
		{
			return true;
		}

		public void FetchUserRatings()
		{
			RequestFilter filter = new RequestFilter();
			APIPaginationParameters pagination = new APIPaginationParameters();
			APIClient.GetUserRatings(filter, pagination, OnRatingFetchSuccess, OnRatingFetchError);
		}

		private void OnRatingFetchError(WebRequestError obj)
		{
			Debug.LogError("Error fetching user ratings: " + obj.displayMessage);
			LocalUserRatings = new ModRating[0];
		}

		private void OnRatingFetchSuccess(RequestPage<ModRating> obj)
		{
			LocalUserRatings = obj.items;
		}

		private void GetGame()
		{
			APIClient.GetGame(OnGameSuccess, OnGameFailed);
		}

		private void DownloadSubbedMods()
		{
			StartCoroutine(WaitUntil(delegate
			{
				StartCoroutine(ModManager.DownloadAndUpdateMods_Coroutine(LocalUser.SubscribedModIds, delegate
				{
					ServiceLocator.GetService<ModalPanel>().CloseWaitPopup();
					Refresh();
				}));
			}, () => LocalUser.SubscribedModIds != null && LocalUser.SubscribedModIds.Count > 0));
		}

		private IEnumerator WaitThen(System.Action a, float t)
		{
			yield return new WaitForSecondsRealtime(t);
			a?.Invoke();
		}

		private IEnumerator WaitUntil(System.Action a, Func<bool> condition)
		{
			yield return new WaitUntil(condition);
			a?.Invoke();
		}

		public void FetchUserMods()
		{
			ModManager.FetchAuthenticatedUserMods(OnLocalUserModsSuccess, OnLocalUserModsFail);
		}

		private void OnLocalUserModsFail(WebRequestError obj)
		{
			Debug.LogError("Error fetching local user mods: " + obj.displayMessage);
		}

		private void OnLocalUserModsSuccess(List<ModProfile> obj)
		{
			LocalUserMods = new Dictionary<WorkshopContentType, ModProfile[]>();
			WorkshopContentType[] array = (WorkshopContentType[])Enum.GetValues(typeof(WorkshopContentType));
			for (int i = 0; i < array.Length; i++)
			{
				WorkshopContentType item = array[i];
				List<ModProfile> list = new List<ModProfile>();
				foreach (ModProfile item2 in obj)
				{
					if (item2.tags.ToList().Find((ModTag m) => m.name.ToLower() == item.ToString().ToLower()) != null)
					{
						list.Add(item2);
					}
				}
				LocalUserMods.Add(item, list.ToArray());
			}
		}

		public static ModProfile[] GetUserModsOfType(WorkshopContentType type)
		{
			if (LocalUserMods == null)
			{
				return new ModProfile[0];
			}
			if (!LocalUserMods.ContainsKey(type))
			{
				return new ModProfile[0];
			}
			return LocalUserMods[type];
		}

		private void OnGameFailed(WebRequestError error)
		{
			Debug.Log("Failed To Fetch Game Profile From ModIO Servers: " + error.displayMessage);
		}

		private void OnGameSuccess(GameProfile profile)
		{
			ModIOGameProfile = profile;
		}

		private void NonModIOInit()
		{
			if (!m_Inited)
			{
				m_Inited = true;
				SetupCallbacks();
				SetupReferences();
				InitializeForPlatform();
			}
		}

		private void SetupReferences()
		{
			m_loadedCustomCampaignLevels = new Stack<LoadedCustomLayoutWrapper>();
			m_loadedCustomUnits = new Stack<LoadedCustomUnitWrapper>();
			m_LoadedCustomFactions = new Stack<LoadedCustomFactionWrapper>();
			m_loadedCustomCampaigns = new Stack<LoadedCustomCampaignWrapper>();
			m_loadedCustomMaps = new Stack<LoadedCustomMapWrapper>();
			m_DownloadingList = new List<int>();
			m_UGCDetailsDictionary = new Dictionary<int, ILoadedCustomContent[]>();
			m_LocalCustomContentLoader = new LocalCustomContentLoader();
			AddOnLoginSuccessAction(LoginRuntime);
		}

		private void SetupCallbacks()
		{
			m_EncryptedTicketResponse = CallResult<EncryptedAppTicketResponse_t>.Create(OnEncryptedAuthTicketResponse);
		}

		public void RequestSteamTicket()
		{
			SteamAPICall_t hAPICall = SteamUser.RequestEncryptedAppTicket(BitConverter.GetBytes(21572), 4);
			m_EncryptedTicketResponse.Set(hAPICall);
		}

		public void TryLogin()
		{
			if (LocalUser.AuthenticationState == AuthenticationState.ValidToken && !hasCheckedUserAuthenticated)
			{
				Debug.Log("Already Logged in!");
				GetAuthenticatedUserFromModio();
				hasCheckedUserAuthenticated = true;
			}
			else
			{
				InitializeForPlatform();
				RequestLoginTicket();
			}
		}

		public void RequestLoginTicket()
		{
			if (m_steamManager.Initialized)
			{
				RequestSteamTicket();
			}
		}

		private async void InitializeForPlatform()
		{
			if (m_ModIOUserAuthenticator != null)
			{
				hasCheckedUserAuthenticated = false;
				m_BusyAuthenticatingUserCount++;
				try
				{
					await m_ModIOUserAuthenticator.AuthenticateUserAsync();
				}
				catch (Exception arg)
				{
					Debug.LogError($"Failed to authenticate user.\n{arg}");
				}
				m_BusyAuthenticatingUserCount--;
			}
		}

		private void GetAuthenticatedUserFromModio()
		{
			APIClient.GetAuthenticatedUser(delegate(UserProfile u)
			{
				Debug.Log("CURRENTLY LOGGED IN AS: " + u.username + " ID: " + u.id + " ON MOD IO");
				LocalModIOUser = u;
				if (m_OnLoginSuccessActionOnce != null)
				{
					hasCheckedUserAuthenticated = true;
					m_OnLoginSuccessActionOnce();
					m_OnLoginSuccessActionOnce = null;
				}
			}, delegate
			{
				hasCheckedUserAuthenticated = false;
				Debug.LogError("Cannot Fetch current user from MOD IO");
				LocalModIOUser = null;
			});
		}

		private void OnLoginFailed(WebRequestError obj)
		{
			Debug.LogError("Login to ModIO Failed: " + obj.displayMessage);
		}

		private void OnAuthFail(WebRequestError obj)
		{
			Debug.Log("ModIO Auth Fail: " + obj.displayMessage);
		}

		private void OnAuthSuccess(string obj)
		{
			Debug.Log("Mod IO Auth Success: " + obj);
			UserAuthenticationData instance = UserAuthenticationData.instance;
			instance.token = obj;
			UserAuthenticationData.instance = instance;
		}

		private void HandleModDirectory(string modDir)
		{
			Debug.Log("Handling Mod: " + modDir);
		}

		public async void CheckPermissionToLoadMods(bool refresh, CheckPermissionToLoadModsCallback doneCallback)
		{
			if (DidGivePermissionToLoadMods)
			{
				OnCheckedPermissionToLoadMods(refresh: false, doneCallback);
				return;
			}
			await Task.Yield();
			await Task.Yield();
			m_ModalPanel.Choice(string.Empty, "POPUP_MODIO_CONFIRM_LOAD_MODS", delegate
			{
				DidGivePermissionToLoadMods = true;
				OnCheckedPermissionToLoadMods(refresh, doneCallback);
			}, delegate
			{
				OnCheckedPermissionToLoadMods(refresh: false, doneCallback);
			});
		}

		private async void OnCheckedPermissionToLoadMods(bool refresh, CheckPermissionToLoadModsCallback doneCallback)
		{
			if (!refresh)
			{
				doneCallback?.Invoke(DidGivePermissionToLoadMods);
				return;
			}
			m_ModalPanel.WaitPopUpWithFocus("POPUP_REFRESHING_CONTENT", -1f, null, null, true);
			await Task.Delay(500);
			Refresh(delegate
			{
				m_ModalPanel.CloseWaitPopup();
				doneCallback?.Invoke(DidGivePermissionToLoadMods);
			});
		}

		public bool IsRefreshingOrWaitingToRefresh()
		{
			if (m_RefreshQueue.Count > 0)
			{
				return true;
			}
			if (m_DidDoFullRefresh)
			{
				return m_QuickRefreshCount > 0;
			}
			return true;
		}

		public void QuickRefresh(WorkshopContentType type, System.Action doneCallback)
		{
			AddItemToRefreshQueue(new RefreshItem(isQuickRefresh: true, type, DidGivePermissionToLoadMods, doneCallback));
		}

		private async void QuickRefreshInternal(WorkshopContentType type, bool hasPermissionToLoadMods, System.Action doneCallback)
		{
			Resources.UnloadUnusedAssets();
			ContentDatabase.Instance().ClearUserContent(type);
			m_QuickRefreshCount++;
			RefreshLocalContent(type, delegate
			{
				if (!hasPermissionToLoadMods)
				{
					OnRefreshedWorkshopContent(delegate
					{
						m_QuickRefreshCount--;
						doneCallback?.Invoke();
					});
				}
				else
				{
					RefreshWorkshopContent(type, delegate
					{
						m_QuickRefreshCount--;
						doneCallback?.Invoke();
					});
				}
			});
		}

		private void RefreshLocalContent(WorkshopContentType type, System.Action doneCallback)
		{
			m_LocalCustomContentLoader.SearchForLocalCustomContent(expectingNewContent: false, type, delegate
			{
				AddLocalCustomContent(type, delegate
				{
					doneCallback?.Invoke();
				});
			});
		}

		private void RefreshWorkshopContent(WorkshopContentType type, System.Action doneCallback)
		{
			SearchForWorkshopCustomContent(type, delegate
			{
				OnRefreshedWorkshopContent(doneCallback);
			});
		}

		private void OnRefreshedWorkshopContent(System.Action doneCallback)
		{
			if (m_DownloadingList.Count <= 0)
			{
				DoneDownloading(delegate
				{
					doneCallback?.Invoke();
				});
			}
			else
			{
				doneCallback?.Invoke();
			}
		}

		public void Refresh(System.Action refreshDone = null)
		{
			AddItemToRefreshQueue(new RefreshItem(isQuickRefresh: false, null, DidGivePermissionToLoadMods, refreshDone));
		}

		private void RefreshInternal(bool hasPermissionToLoadMods, System.Action refreshDone = null)
		{
			m_DidDoFullRefresh = true;
			Debug.Log("Refreshing Customcontent! " + Time.frameCount);
			QuickRefreshInternal(WorkshopContentType.Any, hasPermissionToLoadMods, delegate
			{
				this.ContentQuickRefreshed?.Invoke();
				QuickRefreshInternal(WorkshopContentType.Faction, hasPermissionToLoadMods, delegate
				{
					refreshDone?.Invoke();
				});
			});
		}

		private void OnDownloadError(WebRequestError obj)
		{
			Debug.LogError("Download Error " + obj.errorMessage);
		}

		private void OnDownloadSuccess(int modID)
		{
			Debug.Log("Download Success! " + modID + " Frame: " + Time.frameCount);
			if (m_DownloadingList.Contains(modID))
			{
				m_DownloadingList.Remove(modID);
			}
			Refresh();
		}

		private void SearchForWorkshopCustomContent(WorkshopContentType type, System.Action doneCallback)
		{
			List<KeyValuePair<ModfileIdPair, string>> loads = new List<KeyValuePair<ModfileIdPair, string>>();
			ModManager.QueryInstalledMods(null, delegate(IList<KeyValuePair<ModfileIdPair, string>> installedMods)
			{
				foreach (KeyValuePair<ModfileIdPair, string> installedMod in installedMods)
				{
					loads.Add(installedMod);
				}
				int count = loads.Count;
				AsyncCounter counter;
				if (count <= 0)
				{
					doneCallback?.Invoke();
				}
				else
				{
					counter = new AsyncCounter(count);
					for (int i = 0; i < count; i++)
					{
						KeyValuePair<ModfileIdPair, string> item = loads[i];
						ReadCustomContent(new DirectoryInfo(item.Value), item.Key.modId, type, delegate(ILoadedCustomContent[] loaded)
						{
							if (loaded != null && !m_UGCDetailsDictionary.ContainsKey(item.Key.modId))
							{
								m_UGCDetailsDictionary.Add(item.Key.modId, loaded);
								ModManager.GetModProfile(item.Key.modId, delegate(ModProfile profile)
								{
									OnModProfileSuccess(profile);
									Done();
								}, delegate(WebRequestError e)
								{
									OnModProfileFail(e);
									Done();
								});
							}
							else
							{
								Done();
							}
						});
					}
				}
				void Done()
				{
					if (counter.OnAsyncDone())
					{
						doneCallback?.Invoke();
					}
				}
			});
		}

		private void AddLocalCustomContent(WorkshopContentType type = WorkshopContentType.Any, System.Action doneCallback = null)
		{
			List<ReadAsyncData> list = new List<ReadAsyncData>();
			WorkshopContentType[] array = (WorkshopContentType[])Enum.GetValues(typeof(WorkshopContentType));
			if (type != WorkshopContentType.Any && 0 == 0)
			{
				switch (type)
				{
				case WorkshopContentType.Unit:
					array = new WorkshopContentType[1] { type };
					break;
				case WorkshopContentType.Layout:
				case WorkshopContentType.Battle:
					array = new WorkshopContentType[1] { WorkshopContentType.Battle };
					break;
				case WorkshopContentType.Campaign:
					array = new WorkshopContentType[1] { type };
					break;
				case WorkshopContentType.Faction:
					array = new WorkshopContentType[1] { type };
					break;
				case WorkshopContentType.Map:
					array = new WorkshopContentType[1] { type };
					break;
				}
			}
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				if (!m_LocalCustomContentLoader.CustomContentFiles.ContainsKey(array[i]))
				{
					continue;
				}
				List<FileInfo> list2 = m_LocalCustomContentLoader.CustomContentFiles[array[i]];
				int count = list2.Count;
				for (int j = 0; j < count; j++)
				{
					FileInfo fileInfo = list2[j];
					if (fileInfo.Extension == CustomContentFilePaths.FileEndingUnit)
					{
						list.Add(new ReadAsyncData
						{
							Info = fileInfo,
							ModID = 0,
							Type = ReadAsyncDataType.Unit
						});
					}
					else if (fileInfo.Extension == CustomContentFilePaths.FileEndingLayout || fileInfo.Extension == CustomContentFilePaths.FileEndingBattle)
					{
						list.Add(new ReadAsyncData
						{
							Info = fileInfo,
							ModID = 0,
							Type = ReadAsyncDataType.Layout
						});
					}
					else if (fileInfo.Extension == CustomContentFilePaths.FileEndingCampaign)
					{
						list.Add(new ReadAsyncData
						{
							Info = fileInfo,
							ModID = 0,
							Type = ReadAsyncDataType.Campaign
						});
					}
					else if (fileInfo.Extension == CustomContentFilePaths.FileEndingFaction)
					{
						list.Add(new ReadAsyncData
						{
							Info = fileInfo,
							ModID = 0,
							Type = ReadAsyncDataType.Faction
						});
					}
					else if (fileInfo.Extension == CustomContentFilePaths.FileEndingCustomMap)
					{
						list.Add(new ReadAsyncData
						{
							Info = fileInfo,
							ModID = 0,
							Type = ReadAsyncDataType.Map
						});
					}
				}
			}
			int count2 = list.Count;
			if (count2 <= 0)
			{
				doneCallback?.Invoke();
				return;
			}
			AsyncCounter asyncCounter = new AsyncCounter(count2);
			for (int k = 0; k < count2; k++)
			{
				ReadAsyncData readAsyncData = list[k];
				System.Action tempDoneCallback = doneCallback;
				AsyncCounter tempCounter = asyncCounter;
				switch (readAsyncData.Type)
				{
				case ReadAsyncDataType.Layout:
					ReadLayout(readAsyncData.Info, readAsyncData.ModID, delegate(LoadedCustomLayoutWrapper wrapper)
					{
						if (wrapper != null)
						{
							m_loadedCustomCampaignLevels.Push(wrapper);
						}
						if (tempCounter.OnAsyncDone())
						{
							tempDoneCallback?.Invoke();
						}
					});
					break;
				case ReadAsyncDataType.Campaign:
					ReadCampaign(readAsyncData.Info, readAsyncData.ModID, delegate
					{
						if (tempCounter.OnAsyncDone())
						{
							tempDoneCallback?.Invoke();
						}
					});
					break;
				case ReadAsyncDataType.Faction:
					ReadFaction(readAsyncData.Info, readAsyncData.ModID, delegate(LoadedCustomFactionWrapper wrapper)
					{
						if (wrapper != null)
						{
							m_LoadedCustomFactions.Push(wrapper);
						}
						if (tempCounter.OnAsyncDone())
						{
							tempDoneCallback?.Invoke();
						}
					});
					break;
				case ReadAsyncDataType.Unit:
					ReadUnit(readAsyncData.Info, readAsyncData.ModID, delegate(LoadedCustomUnitWrapper wrapper)
					{
						if (wrapper != null)
						{
							m_loadedCustomUnits.Push(wrapper);
						}
						if (tempCounter.OnAsyncDone())
						{
							tempDoneCallback?.Invoke();
						}
					});
					break;
				case ReadAsyncDataType.Map:
					ReadMap(readAsyncData.Info, readAsyncData.ModID, delegate(LoadedCustomMapWrapper wrapper)
					{
						if (wrapper != null)
						{
							m_loadedCustomMaps.Push(wrapper);
						}
						if (tempCounter.OnAsyncDone())
						{
							tempDoneCallback?.Invoke();
						}
					});
					break;
				default:
					Debug.LogErrorFormat("Unsupported load async data type: {0}", readAsyncData.Type);
					if (tempCounter.OnAsyncDone())
					{
						tempDoneCallback?.Invoke();
					}
					break;
				}
			}
		}

		private void OnModProfileFail(WebRequestError obj)
		{
			Debug.Log("ModProfileFail: " + obj.errorMessage);
		}

		private void OnModProfileSuccess(ModProfile obj)
		{
			if (!m_UGCDetailsDictionary.ContainsKey(obj.id))
			{
				Debug.LogError("Got OnModProfileSuccess but is not present in Dictionary");
				return;
			}
			ILoadedCustomContent[] array = m_UGCDetailsDictionary[obj.id];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetDetails(obj);
			}
			m_UGCDetailsDictionary.Remove(obj.id);
		}

		private void DoneDownloading(System.Action doneCallback)
		{
			ContentDatabase db = ContentDatabase.Instance();
			AddUnitsToDatabase(db);
			AddFactionsToDatabase(db);
			AddCustomMapToDatabase(db);
			AddCampaignLevelsToDatabase(db, delegate
			{
				AddCampaignsToDatabaseAsync(db, delegate
				{
					doneCallback?.Invoke();
				});
			});
		}

		private void AddFactionsToDatabase(ContentDatabase db)
		{
			DMProfanityService service = ServiceLocator.GetService<DMProfanityService>();
			while (m_LoadedCustomFactions.Count > 0)
			{
				LoadedCustomFactionWrapper currFaction = m_LoadedCustomFactions.Pop();
				currFaction.faction.modID = currFaction.ModID;
				if (DMProfanityFilter.ShouldFilter())
				{
					service.QueueProfanityMasking(currFaction.faction.Entity.Name, delegate(string s)
					{
						currFaction.faction.Entity.Name = s;
					});
					currFaction.faction.Entity.Name = "...";
				}
				db.AddUserFaction(currFaction.faction);
			}
		}

		private void AddUnitsToDatabase(ContentDatabase db)
		{
			DMProfanityService service = ServiceLocator.GetService<DMProfanityService>();
			while (m_loadedCustomUnits.Count > 0)
			{
				LoadedCustomUnitWrapper currUnit = m_loadedCustomUnits.Pop();
				currUnit.BluePrint.SetModProfile(currUnit.ModProfile);
				if (DMProfanityFilter.ShouldFilter())
				{
					service.QueueProfanityMasking(currUnit.BluePrint.Entity.Name, delegate(string s)
					{
						currUnit.BluePrint.Entity.Name = s;
					});
					service.QueueProfanityMasking(currUnit.BluePrint.UnitDescription, delegate(string s)
					{
						currUnit.BluePrint.UnitDescription = s;
					});
					currUnit.BluePrint.Entity.Name = "...";
					currUnit.BluePrint.UnitDescription = "...";
				}
				db.AddUserUnitBlueprint(currUnit.BluePrint);
			}
		}

		private void AddCampaignsToDatabaseAsync(ContentDatabase db, Action<Exception> doneCallBack)
		{
			FileIOWrapper service = ServiceLocator.GetService<FileIOWrapper>();
			DMProfanityService profanityService = ServiceLocator.GetService<DMProfanityService>();
			try
			{
				if (m_loadedCustomCampaigns.Count <= 0)
				{
					doneCallBack?.Invoke(null);
					return;
				}
				int loadedCampaignsToProcessCount = m_loadedCustomCampaigns.Count;
				int i = 0;
				for (int count = m_loadedCustomCampaigns.Count; i < count; i++)
				{
					LoadedCustomCampaignWrapper loadedCustomCampaignWrapper = m_loadedCustomCampaigns.Pop();
					TABSCampaignAsset campaign = TABSCampaignAsset.DeserializeCampaign(loadedCustomCampaignWrapper.CampaignSequence, db.GetCampaignLevel);
					campaign.SetCustomUnit(loadedCustomCampaignWrapper.ModID, loadedCustomCampaignWrapper.ModProfile, loadedCustomCampaignWrapper.ContentFile);
					string directoryName = Path.GetDirectoryName(campaign.FilePath);
					string iconPath = Path.Combine(directoryName, "Picture.png");
					service.FileExists(iconPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
					{
						int num = loadedCampaignsToProcessCount;
						loadedCampaignsToProcessCount = num - 1;
						if (exists)
						{
							campaign.SetIconPath(iconPath);
						}
						if (DMProfanityFilter.ShouldFilter())
						{
							profanityService.QueueProfanityMasking(campaign.Entity.Name, delegate(string s)
							{
								campaign.Entity.Name = s;
							});
							profanityService.QueueProfanityMasking(campaign.CampaignInfo.Description, delegate(string s)
							{
								CampaignInfo campaignInfo2 = campaign.CampaignInfo;
								campaignInfo2.Description = s;
								campaign.CampaignInfo = campaignInfo2;
							});
							profanityService.QueueProfanityMasking(campaign.CampaignInfo.ThankYouTitle, delegate(string s)
							{
								CampaignInfo campaignInfo2 = campaign.CampaignInfo;
								campaignInfo2.ThankYouTitle = s;
								campaign.CampaignInfo = campaignInfo2;
							});
							profanityService.QueueProfanityMasking(campaign.CampaignInfo.ThankYouText, delegate(string s)
							{
								CampaignInfo campaignInfo2 = campaign.CampaignInfo;
								campaignInfo2.ThankYouText = s;
								campaign.CampaignInfo = campaignInfo2;
							});
							campaign.Entity.Name = "...";
							CampaignInfo campaignInfo = campaign.CampaignInfo;
							campaignInfo.Description = "...";
							campaignInfo.ThankYouTitle = "...";
							campaignInfo.ThankYouText = "...";
							campaign.CampaignInfo = campaignInfo;
						}
						db.AddUserCampaign(campaign);
						if (loadedCampaignsToProcessCount <= 0)
						{
							doneCallBack?.Invoke(null);
						}
					});
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Adding UserCampaigns to Database Failed with exception " + ex.Message);
				doneCallBack?.Invoke(ex);
			}
		}

		private void AddCampaignLevelsToDatabase(ContentDatabase db, System.Action doneCallback)
		{
			if (m_loadedCustomCampaignLevels.Count <= 0)
			{
				doneCallback?.Invoke();
				return;
			}
			DMProfanityService profanityService = ServiceLocator.GetService<DMProfanityService>();
			AsyncCounter asyncCounter = new AsyncCounter(m_loadedCustomCampaignLevels.Count);
			while (m_loadedCustomCampaignLevels.Count > 0)
			{
				LoadedCustomLayoutWrapper wrapper = m_loadedCustomCampaignLevels.Pop();
				AsyncCounter tempCounter = asyncCounter;
				CampaignHandler.GetLoadedLayoutFromDisk(wrapper.FilePath, delegate(CampaignLevel level)
				{
					if (level == null)
					{
						if (tempCounter.OnAsyncDone())
						{
							doneCallback?.Invoke();
						}
					}
					else
					{
						TABSCampaignLevelAsset campaignLevel = TABSCampaignLevelAsset.DeserializeCampaignLevel(level, wrapper.FilePath, db.LandfallContentDatabase, db.UserContentDatabase);
						campaignLevel.SetCustomUnit(wrapper.ModID, wrapper.ModProfile);
						string iconPath = Path.GetDirectoryName(campaignLevel.FilePath) + "/Picture.png";
						campaignLevel.SetIconPath(iconPath);
						if (DMProfanityFilter.ShouldFilter())
						{
							profanityService.QueueProfanityMasking(campaignLevel.Entity.Name, delegate(string s)
							{
								campaignLevel.Entity.Name = s;
							});
							profanityService.QueueProfanityMasking(campaignLevel.CampaignInfo.Description, delegate(string s)
							{
								CampaignInfo campaignInfo2 = campaignLevel.CampaignInfo;
								campaignInfo2.Description = s;
								campaignLevel.CampaignInfo = campaignInfo2;
							});
							campaignLevel.Entity.Name = "...";
							CampaignInfo campaignInfo = campaignLevel.CampaignInfo;
							campaignInfo.Description = "...";
							campaignLevel.CampaignInfo = campaignInfo;
						}
						db.AddUserCampaignLevel(campaignLevel);
						if (tempCounter.OnAsyncDone())
						{
							doneCallback?.Invoke();
						}
					}
				});
			}
		}

		private void AddCustomMapToDatabase(ContentDatabase db)
		{
			DMProfanityService service = ServiceLocator.GetService<DMProfanityService>();
			while (m_loadedCustomMaps.Count > 0)
			{
				LoadedCustomMapWrapper currMap = m_loadedCustomMaps.Pop();
				currMap.CustomMap.SetModID(currMap.ModID);
				currMap.CustomMap.SetCustomData(currMap.ModID, currMap.ModProfile);
				if (DMProfanityFilter.ShouldFilter())
				{
					service.QueueProfanityMasking(currMap.CustomMap.Entity.Name, delegate(string s)
					{
						currMap.CustomMap.Entity.Name = s;
					});
					currMap.CustomMap.Entity.Name = "...";
				}
				db.AddUserMap(currMap.CustomMap);
			}
		}

		private void HandleMod(KeyValuePair<ModfileIdPair, string> item)
		{
			ReadCustomContent(new DirectoryInfo(item.Value), item.Key.modId, WorkshopContentType.Any, null);
		}

		private void ReadCustomContent(DirectoryInfo dir, int modID, WorkshopContentType type, Action<ILoadedCustomContent[]> doneCallback)
		{
			string rootPath = dir.FullName;
			m_FileIO.DirectoryExists(rootPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (exists)
				{
					ReadCustomContentOnDirectoryExists(modID, type, rootPath, doneCallback);
				}
				else
				{
					Debug.LogErrorFormat("Directory does not exist: {0}", rootPath);
					doneCallback?.Invoke(null);
				}
			});
		}

		private void ReadCustomContentOnDirectoryExists(int modID, WorkshopContentType type, string rootPath, Action<ILoadedCustomContent[]> doneCallback)
		{
			List<FileInfo> files = new List<FileInfo>();
			m_FileIO.GetFiles(rootPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] filesInRoot, Exception fileException)
			{
				AddFilesToList(files, filesInRoot);
				List<string> allSubFolders = new List<string>();
				int operations = 0;
				GetFolders(rootPath);
				void GetFolders(string root)
				{
					operations++;
					m_FileIO.GetDirectories(root, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] subFolders, Exception exception)
					{
						if (subFolders != null && subFolders.Length != 0)
						{
							foreach (string text in subFolders)
							{
								allSubFolders.Add(text);
								GetFolders(text);
							}
						}
						operations--;
						if (operations == 0)
						{
							ReadCustomContentOnGotSubFolders(modID, type, rootPath, files, allSubFolders.ToArray(), doneCallback);
						}
					});
				}
			});
		}

		private void ReadCustomContentOnGotSubFolders(int modID, WorkshopContentType type, string rootPath, List<FileInfo> files, string[] subFolders, Action<ILoadedCustomContent[]> doneCallback)
		{
			if (subFolders == null || subFolders.Length == 0)
			{
				ReadCustomContentOnGotFiles(modID, type, rootPath, files, doneCallback);
				return;
			}
			int num = subFolders.Length;
			AsyncCounter counter = new AsyncCounter(num);
			for (int i = 0; i < num; i++)
			{
				string path = subFolders[i];
				m_FileIO.GetFiles(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] filesInPath, Exception exception)
				{
					AddFilesToList(files, filesInPath);
					if (counter.OnAsyncDone())
					{
						ReadCustomContentOnGotFiles(modID, type, rootPath, files, doneCallback);
					}
				});
			}
		}

		private void ReadCustomContentOnGotFiles(int modID, WorkshopContentType type, string rootPath, List<FileInfo> files, Action<ILoadedCustomContent[]> doneCallback)
		{
			int count = files.Count;
			if (count <= 0)
			{
				Debug.LogErrorFormat("No files where found in Directory: {0}", rootPath);
				doneCallback?.Invoke(null);
				return;
			}
			AsyncCounter counter = new AsyncCounter(count);
			for (int i = 0; i < count; i++)
			{
				FileInfo fileInfo = files[i];
				string path = fileInfo.FullName;
				m_FileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
				{
					if (!exists)
					{
						Debug.LogErrorFormat("No ModFile folder is found: {0}", path);
						if (counter.OnAsyncDone())
						{
							doneCallback?.Invoke(null);
						}
					}
					else if (counter.OnAsyncDone())
					{
						ReadCustomContentOnFilesExist(modID, type, rootPath, files, doneCallback);
					}
				});
			}
		}

		private void ReadCustomContentOnFilesExist(int modID, WorkshopContentType type, string rootPath, List<FileInfo> files, Action<ILoadedCustomContent[]> doneCallback)
		{
			List<ReadAsyncData> list = new List<ReadAsyncData>();
			List<ILoadedCustomContent> allContentInsideMod = new List<ILoadedCustomContent>();
			int count = files.Count;
			for (int i = 0; i < count; i++)
			{
				FileInfo fileInfo = files[i];
				bool flag = type == WorkshopContentType.Any;
				if (!flag)
				{
					switch (type)
					{
					case WorkshopContentType.Unit:
						flag = fileInfo.Extension == CustomContentFilePaths.FileEndingUnit;
						break;
					case WorkshopContentType.Layout:
					case WorkshopContentType.Battle:
						flag = fileInfo.Extension == CustomContentFilePaths.FileEndingBattle || fileInfo.Extension == CustomContentFilePaths.FileEndingLayout;
						break;
					case WorkshopContentType.Campaign:
						flag = fileInfo.Extension == CustomContentFilePaths.FileEndingCampaign;
						break;
					case WorkshopContentType.Faction:
						flag = fileInfo.Extension == CustomContentFilePaths.FileEndingFaction;
						break;
					case WorkshopContentType.Map:
						flag = fileInfo.Extension == CustomContentFilePaths.FileEndingCustomMap;
						break;
					}
				}
				if (fileInfo.Extension == CustomContentFilePaths.FileEndingUnit && flag)
				{
					list.Add(new ReadAsyncData
					{
						Info = fileInfo,
						ModID = modID,
						Type = ReadAsyncDataType.Unit
					});
				}
				else if (fileInfo.Extension == CustomContentFilePaths.FileEndingLayout || (fileInfo.Extension == CustomContentFilePaths.FileEndingBattle && flag))
				{
					list.Add(new ReadAsyncData
					{
						Info = fileInfo,
						ModID = modID,
						Type = ReadAsyncDataType.Layout
					});
				}
				else if (fileInfo.Extension == CustomContentFilePaths.FileEndingCampaign && flag)
				{
					list.Add(new ReadAsyncData
					{
						Info = fileInfo,
						ModID = modID,
						Type = ReadAsyncDataType.Campaign
					});
				}
				else if (fileInfo.Extension == CustomContentFilePaths.FileEndingFaction && flag)
				{
					list.Add(new ReadAsyncData
					{
						Info = fileInfo,
						ModID = modID,
						Type = ReadAsyncDataType.Faction
					});
				}
				else if (fileInfo.Extension == CustomContentFilePaths.FileEndingCustomMap && flag)
				{
					list.Add(new ReadAsyncData
					{
						Info = fileInfo,
						ModID = modID,
						Type = ReadAsyncDataType.Map
					});
				}
			}
			int count2 = list.Count;
			if (count2 <= 0)
			{
				doneCallback?.Invoke(null);
				return;
			}
			AsyncCounter asyncCounter = new AsyncCounter(count2);
			for (int j = 0; j < count2; j++)
			{
				ReadAsyncData readAsyncData = list[j];
				Action<ILoadedCustomContent[]> tempDoneCallback = doneCallback;
				AsyncCounter tempCounter = asyncCounter;
				List<ILoadedCustomContent> tempAllContentInsideMod = allContentInsideMod;
				switch (readAsyncData.Type)
				{
				case ReadAsyncDataType.Layout:
					ReadLayout(readAsyncData.Info, readAsyncData.ModID, delegate(LoadedCustomLayoutWrapper wrapper)
					{
						if (wrapper != null && !m_loadedCustomCampaignLevels.Contains(wrapper))
						{
							m_loadedCustomCampaignLevels.Push(wrapper);
							tempAllContentInsideMod.Add(wrapper);
						}
						if (tempCounter.OnAsyncDone())
						{
							tempDoneCallback?.Invoke((tempAllContentInsideMod.Count > 0) ? tempAllContentInsideMod.ToArray() : null);
						}
					});
					break;
				case ReadAsyncDataType.Campaign:
					ReadCampaign(readAsyncData.Info, readAsyncData.ModID, delegate(ILoadedCustomContent wrapper)
					{
						if (wrapper != null)
						{
							tempAllContentInsideMod.Add(wrapper);
						}
						if (tempCounter.OnAsyncDone())
						{
							tempDoneCallback?.Invoke((tempAllContentInsideMod.Count > 0) ? tempAllContentInsideMod.ToArray() : null);
						}
					});
					break;
				case ReadAsyncDataType.Faction:
					ReadFaction(readAsyncData.Info, readAsyncData.ModID, delegate(LoadedCustomFactionWrapper wrapper)
					{
						if (wrapper != null && !m_LoadedCustomFactions.Contains(wrapper))
						{
							m_LoadedCustomFactions.Push(wrapper);
							allContentInsideMod.Add(wrapper);
						}
						if (tempCounter.OnAsyncDone())
						{
							tempDoneCallback?.Invoke((tempAllContentInsideMod.Count > 0) ? tempAllContentInsideMod.ToArray() : null);
						}
					});
					break;
				case ReadAsyncDataType.Unit:
					ReadUnit(readAsyncData.Info, readAsyncData.ModID, delegate(LoadedCustomUnitWrapper wrapper)
					{
						if (wrapper != null && !m_loadedCustomUnits.Contains(wrapper))
						{
							m_loadedCustomUnits.Push(wrapper);
							allContentInsideMod.Add(wrapper);
						}
						if (tempCounter.OnAsyncDone())
						{
							tempDoneCallback?.Invoke((tempAllContentInsideMod.Count > 0) ? tempAllContentInsideMod.ToArray() : null);
						}
					});
					break;
				case ReadAsyncDataType.Map:
					ReadMap(readAsyncData.Info, readAsyncData.ModID, delegate(LoadedCustomMapWrapper wrapper)
					{
						if (wrapper != null && !m_loadedCustomMaps.Contains(wrapper))
						{
							m_loadedCustomMaps.Push(wrapper);
							allContentInsideMod.Add(wrapper);
						}
						if (tempCounter.OnAsyncDone())
						{
							tempDoneCallback?.Invoke((tempAllContentInsideMod.Count > 0) ? tempAllContentInsideMod.ToArray() : null);
						}
					});
					break;
				default:
					Debug.LogErrorFormat("Unsupported load async data type: {0}", readAsyncData.Type);
					if (tempCounter.OnAsyncDone())
					{
						tempDoneCallback?.Invoke((tempAllContentInsideMod.Count > 0) ? tempAllContentInsideMod.ToArray() : null);
					}
					break;
				}
			}
		}

		private static void AddFilesToList(List<FileInfo> list, string[] files)
		{
			if (files != null && files.Length != 0)
			{
				int i = 0;
				for (int num = files.Length; i < num; i++)
				{
					list.Add(new FileInfo(files[i]));
				}
			}
		}

		public void ReadUnit(FileInfo file, int modID, Action<LoadedCustomUnitWrapper> doneCallback)
		{
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			string path = file.FullName;
			m_FileIO.ReadAllText(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string json, Exception readException)
			{
				if (string.IsNullOrEmpty(json))
				{
					Debug.LogFormat("Failed to load: {0}", path);
					doneCallback?.Invoke(null);
				}
				else
				{
					UnitBlueprint unitBlueprint = UnitBlueprint.DeserializedUnit(JsonUtility.FromJson<SerializedUnitBlueprint>(json), contentDatabase.AssetLoader, contentDatabase.LandfallContentDatabase);
					unitBlueprint.SetCustomUnit(modID, path);
					string iconPath = Path.Combine(file.Directory.FullName, "icon.png");
					unitBlueprint.SetIconPath(iconPath);
					doneCallback?.Invoke(new LoadedCustomUnitWrapper(unitBlueprint, Path.GetDirectoryName(path), path, 0L));
				}
			});
		}

		public static void LoadFactionFromDisk(FileInfo file, int modID, Action<LoadedCustomFactionWrapper> doneCallback)
		{
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			string path = file.FullName;
			fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!exists)
				{
					Debug.LogErrorFormat("Could not find file: {0}", path);
					doneCallback?.Invoke(null);
				}
				else
				{
					fileIO.ReadAllText(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string json, Exception readException)
					{
						if (string.IsNullOrEmpty(json))
						{
							Debug.LogErrorFormat("Failed to load: {0}", path);
							doneCallback?.Invoke(null);
						}
						else
						{
							Faction faction = Faction.Deserialize(JsonUtility.FromJson<SerializedFaction>(json));
							doneCallback?.Invoke(new LoadedCustomFactionWrapper(faction, modID, Path.GetDirectoryName(path), path, 0L));
						}
					});
				}
			});
		}

		private void ReadLayout(FileInfo info, int modID, Action<LoadedCustomLayoutWrapper> doneCallback)
		{
			CampaignHandler.GetLoadedLayoutFromDisk(info.FullName, delegate(CampaignLevel loadedLevel)
			{
				if (loadedLevel == null)
				{
					Debug.LogError("Unable to load layout, " + info.FullName);
					doneCallback?.Invoke(null);
				}
				else if (loadedLevel.ID == default(DatabaseID))
				{
					Debug.LogError("Level: " + info.FullName + " Has an empty GUID, Redo the map");
					doneCallback?.Invoke(null);
				}
				else
				{
					doneCallback?.Invoke(new LoadedCustomLayoutWrapper(info.FullName, loadedLevel.ID, modID, 0L));
				}
			});
		}

		private void ReadCampaign(FileInfo file, int modID, Action<ILoadedCustomContent> doneCallback)
		{
			CampaignHandler.GetLoadedCampaignFromDisk(file, delegate(CampaignSequence campaign)
			{
				if (campaign == null)
				{
					Debug.LogError("Unable to load campaign, " + file.FullName);
					doneCallback?.Invoke(null);
				}
				else
				{
					LoadedCustomCampaignWrapper loadedCustomCampaignWrapper = new LoadedCustomCampaignWrapper(modID, campaign, 0L, file);
					if (!m_loadedCustomCampaigns.Contains(loadedCustomCampaignWrapper))
					{
						m_loadedCustomCampaigns.Push(loadedCustomCampaignWrapper);
					}
					else
					{
						doneCallback?.Invoke(null);
					}
					doneCallback?.Invoke(loadedCustomCampaignWrapper);
				}
			});
		}

		private void ReadFaction(FileInfo file, int modID, Action<LoadedCustomFactionWrapper> doneCallback)
		{
			LoadFactionFromDisk(file, modID, delegate(LoadedCustomFactionWrapper faction)
			{
				if (faction == null)
				{
					Debug.LogErrorFormat("Failed to load faction: {0}", file.FullName);
					doneCallback?.Invoke(null);
				}
				else
				{
					doneCallback?.Invoke(faction);
				}
			});
		}

		private void ReadMap(FileInfo file, int modID, Action<LoadedCustomMapWrapper> doneCallback)
		{
			ContentDatabase.Instance();
			string path = file.FullName;
			m_FileIO.ReadAllText(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string json, Exception readException)
			{
				if (string.IsNullOrEmpty(json))
				{
					Debug.LogFormat("Failed to load: {0}", path);
					doneCallback?.Invoke(null);
				}
				else
				{
					CustomMap customMap = CustomMap.Deserialize(JsonUtility.FromJson<SerializedCustomMap>(json));
					customMap.SetModID(modID);
					DatabaseID gUID = customMap.Entity.GUID;
					string filePath = Path.Combine(file.Directory.FullName, new DatabaseID(gUID.m_ID).ToString() + CustomContentFilePaths.FileEndingCustomMap);
					customMap.SetFilePath(filePath);
					string iconPath;
					if (customMap.IconPath.EndsWith(".png"))
					{
						iconPath = Path.Combine(file.Directory.FullName, new DatabaseID(gUID.m_ID).ToString() + ".png");
					}
					else
					{
						if (!customMap.IconPath.EndsWith(".jpg"))
						{
							Debug.LogError("Map icon path or file type invalid.");
							doneCallback?.Invoke(null);
							return;
						}
						iconPath = Path.Combine(file.Directory.FullName, new DatabaseID(gUID.m_ID).ToString() + ".jpg");
					}
					customMap.SetIconPath(iconPath);
					string levelPath = Path.Combine(file.Directory.FullName, new DatabaseID(gUID.m_ID).ToString() + CustomContentFilePaths.FileEndingCustomLevel);
					customMap.SetLevelPath(levelPath);
					doneCallback?.Invoke(new LoadedCustomMapWrapper(customMap, path, customMap.Entity.GUID, modID, 0L));
				}
			});
		}

		private void CleanupUserContent()
		{
			m_Inited = false;
			LocalUserRatings = null;
			LocalUserMods?.Clear();
			ModIOGameProfile = null;
			LocalModIOUser = null;
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			TABSCampaignAsset[] array = contentDatabase.GetUserCampaignsByOnEnabled(onlyEnabled: false).ToArray();
			if (array != null && array.Length != 0)
			{
				int i = 0;
				for (int num = array.Length; i < num; i++)
				{
					TABSCampaignAsset tABSCampaignAsset = array[i];
					if (!(tABSCampaignAsset == null) && tABSCampaignAsset.Entity != null)
					{
						contentDatabase.RemoveUserCampaign(tABSCampaignAsset.Entity.GUID);
					}
				}
			}
			TABSCampaignLevelAsset[] array2 = contentDatabase.GetUserCampaignLevelsByOnEnabled(onlyEnabled: false).ToArray();
			if (array2 != null && array2.Length != 0)
			{
				int j = 0;
				for (int num2 = array2.Length; j < num2; j++)
				{
					TABSCampaignLevelAsset tABSCampaignLevelAsset = array2[j];
					if (!(tABSCampaignLevelAsset == null) && tABSCampaignLevelAsset.Entity != null)
					{
						ContentDatabase.Instance().RemoveUserCampaignLevel(tABSCampaignLevelAsset.Entity.GUID, null);
					}
				}
			}
			UnitBlueprint[] array3 = contentDatabase.GetUserUnitBlueprints().ToArray();
			if (array3 == null || array3.Length == 0)
			{
				return;
			}
			int k = 0;
			for (int num3 = array3.Length; k < num3; k++)
			{
				UnitBlueprint unitBlueprint = array3[k];
				if (!(unitBlueprint == null))
				{
					contentDatabase.RemoveUserUnitBlueprintAndEmptyFactionsCreated(unitBlueprint.Entity.GUID);
				}
			}
		}

		private void AddItemToRefreshQueue(RefreshItem item)
		{
			m_RefreshQueue.Enqueue(item);
		}

		private void UpdateRefreshQueue()
		{
			if (m_IsProcessingRefreshQueueItem || m_RefreshQueue.Count <= 0)
			{
				return;
			}
			m_IsProcessingRefreshQueueItem = true;
			RefreshItem item = m_RefreshQueue.Peek();
			if (item.IsQuickRefresh)
			{
				QuickRefreshInternal(item.QuickRefreshType.Value, item.HasPermissionToLoadMods, delegate
				{
					OnRefreshItemDone(item);
				});
			}
			else
			{
				RefreshInternal(item.HasPermissionToLoadMods, delegate
				{
					OnRefreshItemDone(item);
				});
			}
		}

		private void OnRefreshItemDone(RefreshItem item)
		{
			m_IsProcessingRefreshQueueItem = false;
			m_RefreshQueue.Dequeue();
			item.DoneCallback?.Invoke();
		}

		private static long ToUnixTime(DateTime date)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return Convert.ToInt64((date - dateTime).TotalSeconds);
		}

		private void OnEncryptedAuthTicketResponse(EncryptedAppTicketResponse_t param, bool bIOFailure)
		{
			if (bIOFailure)
			{
				Debug.LogError("Biofail: AuthTicket");
			}
			else if (param.m_eResult == EResult.k_EResultOK)
			{
				byte[] array = new byte[1024];
				bool hasUserAcceptedTerms = m_PlayerPrefs.GetInt("ALLOW_UGC") == 0;
				if (SteamUser.GetEncryptedAppTicket(array, array.Length, out var pcbTicket))
				{
					UserAccountManagement.AuthenticateWithSteamEncryptedAppTicket(array, pcbTicket, hasUserAcceptedTerms, OnSteamAuthSuccess, OnSteamAuthFailed);
					Debug.Log("Data Length: " + array.Length + " Ticket: " + pcbTicket);
				}
				else
				{
					Debug.LogError("ModIO Failed to retrive ticket!");
				}
			}
			else
			{
				Debug.LogError("ModIO: OnAuthTicketRespone: " + param.m_eResult);
			}
		}

		private static string ByteArrayToString(byte[] ba)
		{
			StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		private void OnSteamAuthFailed(WebRequestError obj)
		{
			Debug.LogError("ModIO: OnSteamAuthFailed: " + obj.errorMessage);
		}

		private void OnSteamAuthSuccess(UserProfile user)
		{
			Debug.Log("ModIO OnSteam Auth Success: " + user.nameId);
			GetAuthenticatedUserFromModio();
		}

		private void OnAllModListError(WebRequestError obj)
		{
			Debug.LogError("AllModListError: " + obj.errorMessage);
		}

		private void OnAllModListSuccess(RequestPage<ModProfile> obj)
		{
			Debug.Log("OnAllModListSuccess: Mods Returned: " + obj.resultTotal);
			for (int i = 0; i < obj.resultTotal; i++)
			{
				Debug.Log("Mod: " + obj.items[i].name + " ID: " + obj.items[i].id);
			}
		}
	}
}
