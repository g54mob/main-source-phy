using System;
using System.Collections;
using System.Collections.Generic;
using ModIO.API;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class ModBrowser : MonoBehaviour
	{
		[Serializable]
		private struct BrowserState
		{
			public int lastSync_timestamp;

			public int lastSync_userId;

			public int modEventId;

			public int userEventId;

			public Dictionary<int, ModRatingValue> userRatings;
		}

		[Obsolete]
		public struct SubscriptionViewFilter
		{
			public Func<ModProfile, bool> titleFilterDelegate;

			public Comparison<ModProfile> sortDelegate;
		}

		private static ModBrowser _instance = null;

		private const float WaitForInternetDelay = 1f;

		private static BrowserState _state = new BrowserState
		{
			lastSync_timestamp = 0,
			lastSync_userId = -1,
			modEventId = -1,
			userEventId = -1,
			userRatings = new Dictionary<int, ModRatingValue>()
		};

		private GameProfile m_gameProfile = new GameProfile();

		private bool m_isSyncInProgress;

		private Func<bool> m_connectedToInternet;

		private readonly WaitForSecondsRealtime m_initializeForUserWaitFor = new WaitForSecondsRealtime(1f);

		private readonly WaitForSecondsRealtime m_fetchGameProfileWaitFor = new WaitForSecondsRealtime(1f);

		private readonly WaitForSecondsRealtime m_fetchUserProfileWaitFor = new WaitForSecondsRealtime(1f);

		[Obsolete("No longer used.")]
		public const string MANIFEST_FILENAME = "browser_manifest.data";

		[Obsolete]
		[HideInInspector]
		public ExplorerView explorerView;

		[Obsolete]
		[HideInInspector]
		public InspectorView inspectorView;

		[Obsolete]
		[HideInInspector]
		public SubscriptionsView subscriptionsView;

		[Obsolete]
		[HideInInspector]
		public LoginDialog loginDialog;

		[Obsolete]
		[HideInInspector]
		public UserView loggedUserView;

		[Obsolete("Use AuthenticatedUserViewController.m_guestData instead.")]
		private UserDisplayData m_guestData;

		[Obsolete]
		private UserProfile m_userProfile;

		[Obsolete("Use SubscriptionView.titleFilterDelegate and sortDelegate instead.")]
		public SubscriptionViewFilter subscriptionViewFilter;

		public static ModBrowser instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = UIUtilities.FindComponentInAllScenes<ModBrowser>(includeInactive: true);
					if (_instance == null)
					{
						_instance = new GameObject("Mod Browser").AddComponent<ModBrowser>();
					}
				}
				return _instance;
			}
		}

		public GameProfile gameProfile => m_gameProfile;

		[Obsolete("Use PluginSettings.REQUEST_LOGGING instead")]
		public bool debugAllAPIRequests => PluginSettings.REQUEST_LOGGING.logAllResponses;

		[Obsolete("Use ExplorerView.prevPageButton instead.")]
		public Button prevPageButton
		{
			get
			{
				return explorerView.prevPageButton;
			}
			set
			{
				explorerView.prevPageButton = value;
			}
		}

		[Obsolete("Use ExplorerView.nextPageButton instead.")]
		public Button nextPageButton
		{
			get
			{
				return explorerView.nextPageButton;
			}
			set
			{
				explorerView.nextPageButton = value;
			}
		}

		[Obsolete("Use ExporerView.isActiveIndicator instead.")]
		public StateToggleDisplay explorerViewIndicator => explorerView.isActiveIndicator;

		[Obsolete("Use SubscriptionView.isActiveIndicator instead")]
		public StateToggleDisplay subscriptionsViewIndicator => subscriptionsView.isActiveIndicator;

		private void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
			}
		}

		private void OnEnable()
		{
			StartCoroutine(InitializeModBrowser());
			ModManager.onModBinaryInstalled += OnModInstalled;
			DownloadClient.modfileDownloadFailed += OnModfileDownloadFailed;
		}

		private void OnDisable()
		{
			UserAccountManagement.PushSubscriptionChanges(null, null);
			ModManager.onModBinaryInstalled -= OnModInstalled;
			DownloadClient.modfileDownloadFailed -= OnModfileDownloadFailed;
		}

		public void Initialize(Func<bool> connectedToInternet)
		{
			m_connectedToInternet = connectedToInternet;
		}

		private bool IsConnectedToInternet()
		{
			if (m_connectedToInternet != null)
			{
				return m_connectedToInternet();
			}
			return false;
		}

		private IEnumerator InitializeModBrowser()
		{
			bool isDone = false;
			CacheClient.LoadGameProfile(delegate(GameProfile p)
			{
				if (p == null)
				{
					m_gameProfile = new GameProfile();
					m_gameProfile.id = PluginSettings.GAME_ID;
				}
				else
				{
					m_gameProfile = p;
				}
				isDone = true;
			});
			while (!isDone)
			{
				yield return null;
			}
			isDone = false;
			LocalUser.Load(delegate
			{
				isDone = true;
			});
			while (!isDone)
			{
				yield return null;
			}
			isDone = false;
			if (this == null || !base.isActiveAndEnabled)
			{
				yield break;
			}
			foreach (IGameProfileUpdateReceiver item in (IEnumerable<IGameProfileUpdateReceiver>)GetComponentsInChildren<IGameProfileUpdateReceiver>(includeInactive: true))
			{
				item.OnGameProfileUpdated(m_gameProfile);
			}
			foreach (IAuthenticatedUserUpdateReceiver item2 in (IEnumerable<IAuthenticatedUserUpdateReceiver>)GetComponentsInChildren<IAuthenticatedUserUpdateReceiver>(includeInactive: true))
			{
				item2.OnUserProfileUpdated(LocalUser.Profile);
			}
			if (ServerTimeStamp.Now - _state.lastSync_timestamp > 120 || LocalUser.UserId != _state.lastSync_userId)
			{
				StartCoroutine(FetchGameProfile());
				yield return StartCoroutine(InitializeForUser());
			}
		}

		private IEnumerator InitializeForUser()
		{
			if (LocalUser.AuthenticationState == AuthenticationState.ValidToken)
			{
				yield return StartCoroutine(FetchUserProfile());
			}
			else if (!string.IsNullOrEmpty(LocalUser.ExternalAuthentication.ticket))
			{
				while (!IsConnectedToInternet())
				{
					yield return m_initializeForUserWaitFor;
				}
				bool isAttemptingReauth = true;
				UserAccountManagement.ReauthenticateWithStoredExternalAuthData(hasUserAcceptedTerms: true, delegate(UserProfile u)
				{
					foreach (IAuthenticatedUserUpdateReceiver item in (IEnumerable<IAuthenticatedUserUpdateReceiver>)GetComponentsInChildren<IAuthenticatedUserUpdateReceiver>(includeInactive: true))
					{
						item.OnUserProfileUpdated(u);
					}
					isAttemptingReauth = false;
				}, delegate(WebRequestError e)
				{
					Debug.Log("[mod.io] Failed to reauthenticate using stored external authentication data.\n" + e.errorMessage);
					isAttemptingReauth = false;
				});
				while (isAttemptingReauth)
				{
					yield return null;
				}
			}
			if (!(this == null) && base.isActiveAndEnabled)
			{
				yield return StartCoroutine(UpdateSubscriptions());
			}
		}

		private IEnumerator FetchGameProfile()
		{
			bool succeeded = false;
			while (!succeeded)
			{
				while (!IsConnectedToInternet())
				{
					yield return m_fetchGameProfileWaitFor;
				}
				bool isRequestDone = false;
				WebRequestError requestError = null;
				APIClient.GetGame(delegate(GameProfile g)
				{
					if (!(this == null))
					{
						m_gameProfile = g;
						CacheClient.SaveGameProfile(g, null);
						foreach (IGameProfileUpdateReceiver item in (IEnumerable<IGameProfileUpdateReceiver>)GetComponentsInChildren<IGameProfileUpdateReceiver>(includeInactive: true))
						{
							item.OnGameProfileUpdated(g);
						}
						succeeded = true;
						isRequestDone = true;
					}
				}, delegate(WebRequestError e)
				{
					requestError = e;
					isRequestDone = true;
				});
				while (!isRequestDone)
				{
					yield return null;
				}
				if (requestError == null)
				{
					continue;
				}
				int num = CalculateReattemptDelay(requestError);
				if (requestError.isAuthenticationInvalid)
				{
					if (LocalUser.AuthenticationState == AuthenticationState.NoToken)
					{
						Debug.LogWarning("[mod.io] Unable to retrieve the game profile from the mod.io servers. Please check you Game Id and APIKey in the PluginSettings. [Resources/modio_settings]");
						MessageSystem.QueueMessage(MessageDisplayData.Type.Error, "Failed to collect game data from mod.io.\n" + requestError.displayMessage);
					}
					else
					{
						MessageSystem.QueueMessage(MessageDisplayData.Type.Error, requestError.displayMessage);
						LocalUser.WasTokenRejected = true;
						LocalUser.Save();
					}
					break;
				}
				if (requestError.isRequestUnresolvable || num < 0)
				{
					Debug.LogWarning("[mod.io] Fetching Game Profile failed.\n---[ Response Info ]---\n" + DebugUtilities.GetResponseInfo(requestError.webRequest));
					MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to collect game data from mod.io.\n" + requestError.displayMessage);
					break;
				}
				MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to collect game data from mod.io.\n" + requestError.displayMessage + "\nRetrying in " + num + " seconds");
				yield return new WaitForSecondsRealtime(num);
			}
		}

		private IEnumerator FetchUserProfile()
		{
			bool succeeded = false;
			while (!succeeded)
			{
				while (!IsConnectedToInternet())
				{
					yield return m_fetchUserProfileWaitFor;
				}
				bool isRequestDone = false;
				WebRequestError requestError = null;
				UserAccountManagement.UpdateUserProfile(delegate(UserProfile u)
				{
					foreach (IAuthenticatedUserUpdateReceiver item in (IEnumerable<IAuthenticatedUserUpdateReceiver>)GetComponentsInChildren<IAuthenticatedUserUpdateReceiver>(includeInactive: true))
					{
						item.OnUserProfileUpdated(u);
					}
					succeeded = true;
					isRequestDone = true;
				}, delegate(WebRequestError e)
				{
					requestError = e;
					isRequestDone = true;
				});
				while (!isRequestDone)
				{
					yield return null;
				}
				if (requestError != null)
				{
					int num = CalculateReattemptDelay(requestError);
					if (requestError.isAuthenticationInvalid)
					{
						MessageSystem.QueueMessage(MessageDisplayData.Type.Error, requestError.displayMessage);
						LocalUser.WasTokenRejected = true;
						LocalUser.Save();
						yield break;
					}
					if (requestError.isRequestUnresolvable || num < 0)
					{
						Debug.LogWarning("[mod.io] Fetching User Profile failed.\n---[ Response Info ]---\n" + DebugUtilities.GetResponseInfo(requestError.webRequest));
						MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to collect user profile data from mod.io.\n" + requestError.displayMessage);
						LocalUser.WasTokenRejected = true;
						LocalUser.Save();
						yield break;
					}
					MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to collect user profile data from mod.io.\n" + requestError.displayMessage + "\nRetrying in " + num + " seconds");
					yield return new WaitForSecondsRealtime(num);
				}
			}
			StartCoroutine(FetchUserRatings());
		}

		private IEnumerator PerformInitialSubscriptionSync()
		{
			int userId = LocalUser.UserId;
			Func<bool> hasUserChanged = () => userId != LocalUser.UserId;
			if (LocalUser.AuthenticationState == AuthenticationState.ValidToken)
			{
				bool isPushDone = false;
				UserAccountManagement.PushSubscriptionChanges(delegate
				{
					isPushDone = true;
				}, delegate
				{
					isPushDone = true;
				});
				while (!isPushDone)
				{
					yield return null;
				}
			}
			if (hasUserChanged())
			{
				yield break;
			}
			bool request_isDone = false;
			RequestPage<ModProfile> request_page = null;
			WebRequestError request_error = null;
			Action<APIPaginationParameters> requestDelegate;
			if (LocalUser.AuthenticationState == AuthenticationState.ValidToken)
			{
				RequestFilter userSubFilter = new RequestFilter();
				userSubFilter.AddFieldFilter("game_id", new EqualToFilter<int>(PluginSettings.GAME_ID));
				requestDelegate = delegate(APIPaginationParameters p)
				{
					APIClient.GetUserSubscriptions(userSubFilter, p, delegate(RequestPage<ModProfile> r)
					{
						request_isDone = true;
						request_page = r;
					}, delegate(WebRequestError e)
					{
						request_isDone = true;
						request_error = e;
					});
				};
			}
			else
			{
				int[] array = LocalUser.SubscribedModIds.ToArray();
				if (array.Length == 0)
				{
					yield break;
				}
				RequestFilter modFilter = new RequestFilter();
				modFilter.AddFieldFilter("id", new InArrayFilter<int>(array));
				requestDelegate = delegate(APIPaginationParameters p)
				{
					APIClient.GetAllMods(modFilter, p, delegate(RequestPage<ModProfile> r)
					{
						request_isDone = true;
						request_page = r;
					}, delegate(WebRequestError e)
					{
						request_isDone = true;
						request_error = e;
					});
				};
			}
			List<ModProfile> subProfiles = new List<ModProfile>();
			List<int> localOnlySubscriptions = new List<int>(LocalUser.SubscribedModIds);
			List<int> queuedUnsubscribes = LocalUser.QueuedUnsubscribes;
			List<int> subsAdded = new List<int>();
			APIPaginationParameters pagination = new APIPaginationParameters
			{
				limit = 100,
				offset = 0
			};
			bool allPagesReceived = false;
			while (!allPagesReceived && !hasUserChanged())
			{
				request_isDone = false;
				request_page = null;
				request_error = null;
				requestDelegate(pagination);
				while (!request_isDone)
				{
					yield return null;
				}
				if (request_error != null)
				{
					int num = CalculateReattemptDelay(request_error);
					if (request_error.isAuthenticationInvalid)
					{
						MessageSystem.QueueMessage(MessageDisplayData.Type.Error, request_error.displayMessage);
						LocalUser.WasTokenRejected = true;
						LocalUser.Save();
						yield break;
					}
					if (request_error.isRequestUnresolvable || num < 0)
					{
						MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to retrieve subscription data from mod.io servers.\n" + request_error.displayMessage);
						yield break;
					}
					MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to retrieve subscription data from mod.io servers.\n" + request_error.displayMessage + "\nRetrying in " + num + " seconds");
					yield return new WaitForSecondsRealtime(num);
					continue;
				}
				ModProfile[] items = request_page.items;
				foreach (ModProfile modProfile in items)
				{
					if (!queuedUnsubscribes.Contains(modProfile.id))
					{
						subProfiles.Add(modProfile);
						if (!localOnlySubscriptions.Remove(modProfile.id))
						{
							subsAdded.Add(modProfile.id);
						}
					}
				}
				allPagesReceived = request_page.items.Length < request_page.size;
				if (!allPagesReceived)
				{
					pagination.offset += pagination.limit;
				}
			}
			if (hasUserChanged() || !allPagesReceived)
			{
				yield break;
			}
			List<int> queuedSubscribes = LocalUser.QueuedSubscribes;
			List<int> list = new List<int>();
			foreach (int item in localOnlySubscriptions)
			{
				if (!queuedSubscribes.Contains(item))
				{
					list.Add(item);
					LocalUser.SubscribedModIds.Remove(item);
				}
			}
			LocalUser.SubscribedModIds.AddRange(subsAdded);
			LocalUser.Save();
			OnSubscriptionsChanged(subsAdded, list);
			bool isIdFetchDone = false;
			FetchAndSetEventIds(delegate
			{
				isIdFetchDone = true;
			});
			while (!isIdFetchDone)
			{
				yield return null;
			}
		}

		private void FetchAndSetEventIds(Action onComplete = null)
		{
			bool userEventId_isDone = false;
			bool modEventId_isDone = false;
			Action handleResponse = delegate
			{
				if (userEventId_isDone && modEventId_isDone && onComplete != null)
				{
					onComplete();
				}
			};
			RequestFilter requestFilter = new RequestFilter
			{
				sortFieldName = "id",
				isSortAscending = false
			};
			requestFilter.AddFieldFilter("game_id", new EqualToFilter<int>(0)
			{
				filterValue = PluginSettings.GAME_ID
			});
			APIPaginationParameters pagination = new APIPaginationParameters
			{
				offset = 0,
				limit = 1
			};
			if (LocalUser.AuthenticationState == AuthenticationState.ValidToken)
			{
				APIClient.GetUserEvents(requestFilter, pagination, delegate(RequestPage<UserEvent> r)
				{
					if (r.items.Length != 0)
					{
						_state.userEventId = r.items[0].id;
					}
					userEventId_isDone = true;
					handleResponse();
				}, delegate
				{
					userEventId_isDone = true;
					handleResponse();
				});
			}
			else
			{
				userEventId_isDone = true;
			}
			APIClient.GetAllModEvents(requestFilter, pagination, delegate(RequestPage<ModEvent> r)
			{
				if (r.items.Length != 0)
				{
					_state.modEventId = r.items[0].id;
				}
				modEventId_isDone = true;
				handleResponse();
			}, delegate
			{
				modEventId_isDone = true;
				handleResponse();
			});
		}

		private IEnumerator FetchUserRatings()
		{
			APIPaginationParameters pagination = new APIPaginationParameters();
			RequestFilter filter = new RequestFilter();
			filter.AddFieldFilter("game_id", new EqualToFilter<int>(0)
			{
				filterValue = m_gameProfile.id
			});
			bool isRequestDone = false;
			List<ModRating> retrievedRatings = new List<ModRating>();
			while (LocalUser.AuthenticationState == AuthenticationState.ValidToken && !isRequestDone)
			{
				RequestPage<ModRating> response = null;
				WebRequestError requestError = null;
				APIClient.GetUserRatings(filter, pagination, delegate(RequestPage<ModRating> r)
				{
					response = r;
					isRequestDone = true;
				}, delegate(WebRequestError e)
				{
					requestError = e;
					isRequestDone = true;
				});
				while (!isRequestDone)
				{
					yield return null;
				}
				if (requestError != null)
				{
					int num = CalculateReattemptDelay(requestError);
					if (requestError.isAuthenticationInvalid)
					{
						MessageSystem.QueueMessage(MessageDisplayData.Type.Error, requestError.displayMessage);
						LocalUser.WasTokenRejected = true;
						LocalUser.Save();
						yield break;
					}
					if (requestError.isRequestUnresolvable || num < 0)
					{
						yield break;
					}
					yield return new WaitForSecondsRealtime(num);
				}
				else
				{
					retrievedRatings.AddRange(response.items);
					isRequestDone = response.size + response.resultOffset >= response.resultTotal;
				}
			}
			_state.userRatings = new Dictionary<int, ModRatingValue>();
			foreach (ModRating item in retrievedRatings)
			{
				_state.userRatings.Add(item.modId, item.ratingValue);
			}
		}

		private IEnumerator VerifySubscriptionInstallations()
		{
			List<int> subscribedModIds = LocalUser.SubscribedModIds;
			Dictionary<int, List<int>> groupedIds = new Dictionary<int, List<int>>();
			foreach (int item in subscribedModIds)
			{
				groupedIds.Add(item, new List<int>());
			}
			bool gotModVersions = false;
			IList<ModfileIdPair> installedModVersions = null;
			ModManager.QueryInstalledModVersions(excludeDisabledMods: false, delegate(List<ModfileIdPair> r)
			{
				installedModVersions = r;
				gotModVersions = true;
			});
			while (!gotModVersions)
			{
				yield return null;
			}
			foreach (ModfileIdPair item2 in installedModVersions)
			{
				if (subscribedModIds.Contains(item2.modId))
				{
					groupedIds[item2.modId].Add(item2.modfileId);
					continue;
				}
				bool isUninstallDone = false;
				ModManager.UninstallMod(item2.modId, delegate
				{
					isUninstallDone = true;
				});
				while (!isUninstallDone)
				{
					yield return null;
				}
			}
			List<Modfile> modfilesToAssert = new List<Modfile>(subscribedModIds.Count);
			bool isRequestDone = false;
			ModProfileRequestManager.instance.RequestModProfiles(subscribedModIds, delegate(ModProfile[] modProfiles)
			{
				foreach (ModProfile modProfile in modProfiles)
				{
					if (modProfile != null && modProfile.currentBuild != null && LocalUser.SubscribedModIds.Contains(modProfile.id))
					{
						if (modProfile.currentBuild.modId != modProfile.id)
						{
							Debug.LogWarning("[mod.io] Profile '" + modProfile.name + "(" + modProfile.id + ") has a bad modfile.\nThe modfile.modId is mismatched (" + modProfile.currentBuild.modId + ").");
						}
						else
						{
							modfilesToAssert.Add(modProfile.currentBuild);
						}
					}
				}
				isRequestDone = true;
			}, delegate
			{
				modfilesToAssert = null;
				isRequestDone = true;
			});
			while (!isRequestDone)
			{
				yield return null;
			}
			if (modfilesToAssert != null)
			{
				yield return StartCoroutine(ModManager.AssertDownloadedAndInstalled_Coroutine(modfilesToAssert));
			}
		}

		private IEnumerator FetchAllModProfiles(int[] modIds, Action<List<ModProfile>> onSuccess, Action<WebRequestError> onError)
		{
			if (modIds == null || modIds.Length == 0)
			{
				onSuccess(new List<ModProfile>(0));
				yield break;
			}
			List<ModProfile> modProfiles = new List<ModProfile>();
			APIPaginationParameters pagination = new APIPaginationParameters
			{
				limit = 100,
				offset = 0
			};
			RequestFilter filter = new RequestFilter();
			filter.AddFieldFilter("id", new InArrayFilter<int>
			{
				filterArray = modIds
			});
			bool isDone = false;
			while (!isDone)
			{
				RequestPage<ModProfile> page = null;
				WebRequestError error = null;
				APIClient.GetAllMods(filter, pagination, delegate(RequestPage<ModProfile> r)
				{
					page = r;
				}, delegate(WebRequestError e)
				{
					error = e;
				});
				while (page == null && error == null)
				{
					yield return null;
				}
				if (error != null)
				{
					onError?.Invoke(error);
					modProfiles = null;
					isDone = true;
					continue;
				}
				modProfiles.AddRange(page.items);
				if (page.resultTotal <= page.resultOffset + page.size)
				{
					isDone = true;
				}
				else
				{
					pagination.offset = page.resultOffset + page.size;
				}
			}
			if (isDone && modProfiles != null)
			{
				onSuccess(modProfiles);
			}
		}

		private int CalculateReattemptDelay(WebRequestError requestError)
		{
			if (requestError.limitedUntilTimeStamp > 0)
			{
				return requestError.limitedUntilTimeStamp - ServerTimeStamp.Now;
			}
			if (!requestError.isRequestUnresolvable)
			{
				if (requestError.isServerUnreachable && requestError.webRequest.responseCode > 0)
				{
					return 60;
				}
				return 15;
			}
			return -1;
		}

		public IEnumerator UpdateSubscriptions(Action onComplete = null)
		{
			bool flag = ServerTimeStamp.Now - _state.lastSync_timestamp > 30 || LocalUser.UserId != _state.lastSync_userId;
			if (!m_isSyncInProgress && flag)
			{
				m_isSyncInProgress = true;
				_state.lastSync_userId = LocalUser.UserId;
				int timestamp = ServerTimeStamp.Now;
				bool flag2 = LocalUser.AuthenticationState != AuthenticationState.NoToken && _state.userEventId <= 0;
				if (_state.modEventId <= 0 || flag2 || LocalUser.UserId != _state.lastSync_userId)
				{
					yield return StartCoroutine(PerformInitialSubscriptionSync());
					VerifySubscriptionInstallations();
				}
				else
				{
					yield return StartCoroutine(PullRemoteEventsAndUpdate());
				}
				int num = 2 - (ServerTimeStamp.Now - timestamp);
				if (num > 0)
				{
					yield return new WaitForSeconds(num);
				}
				m_isSyncInProgress = false;
				_state.lastSync_timestamp = ServerTimeStamp.Now;
			}
			else
			{
				yield return new WaitForSeconds(2f);
			}
			onComplete?.Invoke();
		}

		private IEnumerator PullRemoteEventsAndUpdate()
		{
			bool isRequestDone = false;
			WebRequestError requestError = null;
			isRequestDone = false;
			requestError = null;
			if (LocalUser.AuthenticationState != AuthenticationState.NoToken)
			{
				List<UserEvent> userEventReponse = null;
				ModManager.FetchUserEventsAfterId(_state.userEventId, delegate(List<UserEvent> ue)
				{
					userEventReponse = ue;
					isRequestDone = true;
				}, delegate(WebRequestError e)
				{
					requestError = e;
					isRequestDone = true;
				});
				while (!isRequestDone)
				{
					yield return null;
				}
				if (requestError != null)
				{
					if (requestError.isAuthenticationInvalid)
					{
						MessageSystem.QueueMessage(MessageDisplayData.Type.Error, requestError.displayMessage);
						LocalUser.WasTokenRejected = true;
						LocalUser.Save();
					}
					MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to synchronize subscriptions with mod.io servers.\n" + requestError.displayMessage);
					yield break;
				}
				if (LocalUser.AuthenticationState == AuthenticationState.ValidToken)
				{
					if (userEventReponse.Count > 0)
					{
						_state.userEventId = userEventReponse[userEventReponse.Count - 1].id;
						ProcessUserUpdates(userEventReponse);
					}
					bool isPushDone = false;
					UserAccountManagement.PushSubscriptionChanges(delegate
					{
						isPushDone = true;
					}, delegate
					{
						isPushDone = true;
					});
					while (!isPushDone)
					{
						yield return null;
					}
				}
			}
			isRequestDone = false;
			requestError = null;
			List<int> subscribedModIds = LocalUser.SubscribedModIds;
			if (subscribedModIds == null || subscribedModIds.Count <= 0)
			{
				yield break;
			}
			List<ModEvent> modEventResponse = null;
			ModManager.FetchModEventsAfterId(_state.modEventId, LocalUser.SubscribedModIds, delegate(List<ModEvent> me)
			{
				modEventResponse = me;
				isRequestDone = true;
			}, delegate(WebRequestError e)
			{
				requestError = e;
				isRequestDone = true;
			});
			while (!isRequestDone)
			{
				yield return null;
			}
			if (requestError != null)
			{
				if (requestError.isAuthenticationInvalid)
				{
					MessageSystem.QueueMessage(MessageDisplayData.Type.Error, requestError.displayMessage);
					LocalUser.WasTokenRejected = true;
					LocalUser.Save();
				}
				MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to synchronize subscriptions with mod.io servers.\n" + requestError.displayMessage);
			}
			else if (modEventResponse.Count > 0)
			{
				_state.modEventId = modEventResponse[modEventResponse.Count - 1].id;
				StartCoroutine(ProcessModUpdates(modEventResponse));
			}
		}

		protected void ProcessUserUpdates(List<UserEvent> userEvents)
		{
			List<int> subscribedModIds = LocalUser.SubscribedModIds;
			List<int> queuedSubscribes = LocalUser.QueuedSubscribes;
			List<int> queuedUnsubscribes = LocalUser.QueuedUnsubscribes;
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			foreach (UserEvent userEvent in userEvents)
			{
				switch (userEvent.eventType)
				{
				case UserEventType.ModSubscribed:
					queuedSubscribes.Remove(userEvent.modId);
					if (!subscribedModIds.Contains(userEvent.modId) && !queuedUnsubscribes.Contains(userEvent.modId))
					{
						subscribedModIds.Add(userEvent.modId);
						list.Add(userEvent.modId);
					}
					break;
				case UserEventType.ModUnsubscribed:
					queuedUnsubscribes.Remove(userEvent.modId);
					if (subscribedModIds.Contains(userEvent.modId) && !queuedSubscribes.Contains(userEvent.modId))
					{
						subscribedModIds.Remove(userEvent.modId);
						list2.Add(userEvent.modId);
					}
					break;
				}
			}
			LocalUser.Save();
			if (list.Count > 0 || list2.Count > 0)
			{
				OnSubscriptionsChanged(list, list2);
				if (list.Count > 0)
				{
					string messageContent = list.Count + " subscription" + ((list.Count > 1) ? "s" : "") + " retrieved from the server";
					MessageSystem.QueueMessage(MessageDisplayData.Type.Info, messageContent);
				}
			}
		}

		protected IEnumerator ProcessModUpdates(List<ModEvent> modEvents)
		{
			if (modEvents == null || modEvents.Count <= 0)
			{
				yield break;
			}
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			foreach (ModEvent modEvent in modEvents)
			{
				switch (modEvent.eventType)
				{
				case ModEventType.ModfileChanged:
					list.Add(modEvent.modId);
					break;
				case ModEventType.ModDeleted:
					list2.Add(modEvent.modId);
					break;
				}
			}
			if (list2.Count > 0)
			{
				List<int> subscribedModIds = LocalUser.SubscribedModIds;
				foreach (int item in list2)
				{
					subscribedModIds.Remove(item);
				}
				OnSubscriptionsChanged(null, list2);
				int count = list2.Count;
				string messageContent = ((count != 1) ? (count + " subscribed mods have become unavailable and have been removed from your subscriptions.") : "One of your subscribed mods is now unavailable and was removed from your subscriptions.");
				MessageSystem.QueueMessage(MessageDisplayData.Type.Info, messageContent);
			}
			if (list.Count <= 0)
			{
				yield break;
			}
			APIPaginationParameters pagination = new APIPaginationParameters
			{
				limit = 100,
				offset = 0
			};
			RequestFilter requestFilter = new RequestFilter();
			requestFilter.sortFieldName = "id";
			requestFilter.AddFieldFilter("id", new InArrayFilter<int>
			{
				filterArray = list.ToArray()
			});
			bool isRequestDone = false;
			RequestPage<ModProfile> response = null;
			WebRequestError requestError = null;
			APIClient.GetAllMods(requestFilter, pagination, delegate(RequestPage<ModProfile> r)
			{
				isRequestDone = true;
				response = r;
			}, delegate(WebRequestError e)
			{
				isRequestDone = true;
				requestError = e;
			});
			while (!isRequestDone)
			{
				yield return null;
			}
			if (requestError != null)
			{
				if (requestError.isAuthenticationInvalid)
				{
					MessageSystem.QueueMessage(MessageDisplayData.Type.Error, requestError.displayMessage);
					LocalUser.WasTokenRejected = true;
					LocalUser.Save();
				}
				else
				{
					MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to update installed mods.\n" + requestError.displayMessage);
				}
				yield break;
			}
			List<Modfile> list3 = new List<Modfile>(response.items.Length);
			List<int> subscribedModIds2 = LocalUser.SubscribedModIds;
			ModProfile[] items = response.items;
			foreach (ModProfile modProfile in items)
			{
				if (modProfile != null && modProfile.currentBuild != null && subscribedModIds2.Contains(modProfile.id))
				{
					list3.Add(modProfile.currentBuild);
				}
			}
			yield return StartCoroutine(ModManager.AssertDownloadedAndInstalled_Coroutine(list3));
		}

		public void OnUserLogin()
		{
			if (LocalUser.AuthenticationState == AuthenticationState.ValidToken)
			{
				StartCoroutine(InitializeForUser());
			}
		}

		public void LogUserOut()
		{
			UserAccountManagement.PushSubscriptionChanges(null, null);
			LocalUser localUser = LocalUser.instance;
			LocalUser.instance = new LocalUser
			{
				subscribedModIds = localUser.subscribedModIds,
				enabledModIds = localUser.enabledModIds
			};
			LocalUser.isLoaded = true;
			LocalUser.Save();
			foreach (IAuthenticatedUserUpdateReceiver item in (IEnumerable<IAuthenticatedUserUpdateReceiver>)GetComponentsInChildren<IAuthenticatedUserUpdateReceiver>(includeInactive: true))
			{
				item.OnUserLoggedOut();
			}
			MessageSystem.QueueMessage(MessageDisplayData.Type.Success, "Successfully logged out");
		}

		public void SubscribeToMod(int modId)
		{
			UserAccountManagement.SubscribeToMod(modId);
			OnSubscribedToMod(modId);
		}

		public void UnsubscribeFromMod(int modId)
		{
			UserAccountManagement.UnsubscribeFromMod(modId);
			OnUnsubscribedFromMod(modId);
		}

		public void OnSubscribedToMod(int modId)
		{
			EnableMod(modId);
			UpdateSubscriptionReceivers(new int[1] { modId }, null);
			ModProfileRequestManager.instance.RequestModProfile(modId, delegate(ModProfile p)
			{
				if (this != null && base.isActiveAndEnabled && p != null && p.currentBuild != null && LocalUser.SubscribedModIds.Contains(p.id))
				{
					StartCoroutine(ModManager.AssertDownloadedAndInstalled_Coroutine(new Modfile[1] { p.currentBuild }));
				}
			}, delegate(WebRequestError requestError)
			{
				if (requestError.isAuthenticationInvalid)
				{
					LocalUser.WasTokenRejected = true;
					LocalUser.Save();
					MessageSystem.QueueMessage(MessageDisplayData.Type.Error, requestError.displayMessage);
				}
				else
				{
					MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to start mod download. It will be retried shortly.\n" + requestError.displayMessage);
				}
			});
		}

		public void OnUnsubscribedFromMod(int modId)
		{
			DownloadClient.CancelAnyModBinaryDownloads(modId);
			CacheClient.DeleteAllModfileAndBinaryData(modId, null);
			ModManager.UninstallMod(modId, null);
			DisableMod(modId);
			UpdateSubscriptionReceivers(null, new int[1] { modId });
		}

		public void OnSubscriptionsChanged(IList<int> addedSubscriptions, IList<int> removedSubscriptions)
		{
			if (addedSubscriptions != null && addedSubscriptions.Count > 0)
			{
				foreach (int addedSubscription in addedSubscriptions)
				{
					if (!LocalUser.EnabledModIds.Contains(addedSubscription))
					{
						LocalUser.EnabledModIds.Add(addedSubscription);
					}
				}
				ModProfileRequestManager.instance.RequestModProfiles(addedSubscriptions, delegate(ModProfile[] modProfiles)
				{
					if (this != null && base.isActiveAndEnabled)
					{
						List<int> subscribedModIds = LocalUser.SubscribedModIds;
						List<Modfile> list = new List<Modfile>(modProfiles.Length);
						foreach (ModProfile modProfile in modProfiles)
						{
							if (modProfile != null && modProfile.currentBuild != null && subscribedModIds.Contains(modProfile.id))
							{
								list.Add(modProfile.currentBuild);
							}
						}
						StartCoroutine(ModManager.AssertDownloadedAndInstalled_Coroutine(list));
					}
				}, delegate(WebRequestError requestError)
				{
					if (requestError.isAuthenticationInvalid)
					{
						MessageSystem.QueueMessage(MessageDisplayData.Type.Error, requestError.displayMessage);
						LocalUser.WasTokenRejected = true;
						LocalUser.Save();
					}
					else
					{
						MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to start mod downloads. They will be retried shortly.\n" + requestError.displayMessage);
					}
				});
			}
			if (removedSubscriptions != null && removedSubscriptions.Count > 0)
			{
				foreach (int removedSubscription in removedSubscriptions)
				{
					CacheClient.DeleteAllModfileAndBinaryData(removedSubscription, null);
					ModManager.UninstallMod(removedSubscription, null);
					LocalUser.EnabledModIds.Remove(removedSubscription);
				}
			}
			LocalUser.Save();
			UpdateSubscriptionReceivers(addedSubscriptions, removedSubscriptions);
		}

		private void UpdateSubscriptionReceivers(IList<int> addedSubscriptions, IList<int> removedSubscriptions)
		{
			if (addedSubscriptions == null)
			{
				addedSubscriptions = new int[0];
			}
			if (removedSubscriptions == null)
			{
				removedSubscriptions = new int[0];
			}
			foreach (IModSubscriptionsUpdateReceiver item in (IEnumerable<IModSubscriptionsUpdateReceiver>)GetComponentsInChildren<IModSubscriptionsUpdateReceiver>(includeInactive: true))
			{
				item.OnModSubscriptionsUpdated(addedSubscriptions, removedSubscriptions);
			}
		}

		public void EnableMod(int modId)
		{
			if (!LocalUser.EnabledModIds.Contains(modId))
			{
				LocalUser.EnabledModIds.Add(modId);
				LocalUser.Save();
			}
			foreach (IModEnabledReceiver item in (IEnumerable<IModEnabledReceiver>)GetComponentsInChildren<IModEnabledReceiver>(includeInactive: true))
			{
				item.OnModEnabled(modId);
			}
		}

		public void DisableMod(int modId)
		{
			if (LocalUser.EnabledModIds.Contains(modId))
			{
				LocalUser.EnabledModIds.Remove(modId);
				LocalUser.Save();
			}
			foreach (IModDisabledReceiver item in (IEnumerable<IModDisabledReceiver>)GetComponentsInChildren<IModDisabledReceiver>(includeInactive: true))
			{
				item.OnModDisabled(modId);
			}
		}

		public void AttemptRateMod(int modId, ModRatingValue ratingValue)
		{
			if (ratingValue == ModRatingValue.None)
			{
				Debug.Log("[mod.io] Clearing a rating is currently unsupported.");
			}
			else if (LocalUser.AuthenticationState == AuthenticationState.ValidToken)
			{
				ModRatingValue oldRating = GetModRating(modId);
				IEnumerable<IModRatingAddedReceiver> ratingReceivers = GetComponentsInChildren<IModRatingAddedReceiver>(includeInactive: true);
				foreach (IModRatingAddedReceiver item in ratingReceivers)
				{
					item.OnModRatingAdded(modId, ratingValue);
				}
				AddModRatingParameters parameters = new AddModRatingParameters
				{
					ratingValue = ratingValue
				};
				APIClient.AddModRating(modId, parameters, delegate
				{
					if (this != null)
					{
						_state.userRatings[modId] = ratingValue;
					}
				}, delegate(WebRequestError e)
				{
					if (!(this == null))
					{
						if (e.webRequest.responseCode != 400)
						{
							MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, e.displayMessage);
							{
								foreach (IModRatingAddedReceiver item2 in ratingReceivers)
								{
									item2?.OnModRatingAdded(modId, oldRating);
								}
								return;
							}
						}
						_state.userRatings[modId] = ratingValue;
					}
				});
			}
			else
			{
				ViewManager.instance.ShowLoginDialog();
			}
		}

		public ModRatingValue GetModRating(int modId)
		{
			if (!_state.userRatings.TryGetValue(modId, out var value))
			{
				return ModRatingValue.None;
			}
			return value;
		}

		private void OnModInstalled(ModfileIdPair idPair)
		{
			if (!(this == null))
			{
				ModProfileRequestManager.instance.RequestModProfile(idPair.modId, delegate(ModProfile p)
				{
					MessageSystem.QueueMessage(MessageDisplayData.Type.Info, p.name + " was successfully downloaded and installed.");
				}, null);
			}
		}

		private void OnModfileDownloadFailed(ModfileIdPair idPair, WebRequestError error)
		{
			if (!(this == null))
			{
				ModProfileRequestManager.instance.RequestModProfile(idPair.modId, delegate(ModProfile p)
				{
					MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, p.name + " failed to download.\n" + error.displayMessage);
				}, null);
			}
		}

		[Obsolete("Use ViewManager.ActivateExplorerView() instead.")]
		public void ShowExplorerView()
		{
			ViewManager.instance.ActivateExplorerView();
		}

		[Obsolete("Use ModProfileRequestManager.FetchModProfilePage() instead.")]
		public void RequestExplorerPage(int pageIndex, Action<RequestPage<ModProfile>> onSuccess, Action<WebRequestError> onError)
		{
			if (explorerView == null)
			{
				onError?.Invoke(null);
			}
			else
			{
				ModProfileRequestManager.instance.FetchModProfilePage(explorerView.GenerateRequestFilter(), pageIndex * explorerView.itemsPerPage, explorerView.itemsPerPage, ExplorerView.SearchMethod.All, onSuccess, onError);
			}
		}

		[Obsolete("Use ExplorerView.UpdatePageButtonInteractibility() instead.")]
		public void UpdateExplorerViewPageButtonInteractibility()
		{
			explorerView.UpdatePageButtonInteractibility();
		}

		[Obsolete("Use ExplorerView.Refresh() instead.")]
		public void UpdateExplorerFilters()
		{
			explorerView.Refresh();
		}

		[Obsolete("Use ExplorerView.ChangePage() instead.")]
		public void ChangeExplorerPage(int direction)
		{
			explorerView.ChangePage(direction);
		}

		[Obsolete("Use ViewManager.ActivateSubscriptionsView() instead.")]
		public void ShowSubscriptionsView()
		{
			ViewManager.instance.ActivateSubscriptionsView();
		}

		[Obsolete("Use SubscriptionsView.Refresh() instead.")]
		public void RequestSubscribedModProfiles(Action<List<ModProfile>> onSuccess, Action<WebRequestError> onError)
		{
			subscriptionsView.Refresh();
		}

		[Obsolete("Use SubscriptionsView.UpdateFilter() instead.")]
		public void UpdateSubscriptionFilters()
		{
			subscriptionsView.Refresh();
		}

		[Obsolete("Use ViewManager.InspectMod() instead.")]
		public void InspectMod(int modId)
		{
			ViewManager.instance.InspectMod(modId);
		}

		[Obsolete("Use ViewManager.InspectMod() instead.")]
		public void InspectDiscoverItem(ModView view)
		{
			InspectMod(view.data.profile.modId);
		}

		[Obsolete("Use ViewManager.InspectMod() instead.")]
		public void InspectSubscriptionItem(ModView view)
		{
			InspectMod(view.data.profile.modId);
		}

		[Obsolete("Use InspectorView.gameObject.SetActive(false) instead.")]
		public void CloseInspector()
		{
			inspectorView.gameObject.SetActive(value: false);
		}

		[Obsolete("Use LoginDialog.gameObject.SetActive(true) instead.")]
		public void OpenLoginDialog()
		{
			loginDialog.gameObject.SetActive(value: true);
		}

		[Obsolete("Use LoginDialog.gameObject.SetActive(false) instead.")]
		public void CloseLoginDialog()
		{
			loginDialog.gameObject.SetActive(value: false);
		}
	}
}
