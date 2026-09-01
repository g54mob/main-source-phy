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

		[Obsolete("No longer used.")]
		public const string MANIFEST_FILENAME = "browser_manifest.data";

		private static ModBrowser _instance = null;

		public static Action<string, Action<int, string>> filterMethod;

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

		[HideInInspector]
		[Obsolete]
		public ExplorerView explorerView;

		[HideInInspector]
		[Obsolete]
		public InspectorView inspectorView;

		[HideInInspector]
		[Obsolete]
		public SubscriptionsView subscriptionsView;

		[HideInInspector]
		[Obsolete]
		public LoginDialog loginDialog;

		[HideInInspector]
		[Obsolete]
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
					_instance = UIUtilities.FindComponentInAllScenes<ModBrowser>(true);
					if (_instance == null)
					{
						GameObject gameObject = new GameObject("Mod Browser");
						_instance = gameObject.AddComponent<ModBrowser>();
					}
				}
				return _instance;
			}
		}

		public GameProfile gameProfile
		{
			get
			{
				return m_gameProfile;
			}
		}

		[Obsolete("Use PluginSettings.REQUEST_LOGGING instead")]
		public bool debugAllAPIRequests
		{
			get
			{
				return PluginSettings.REQUEST_LOGGING.logAllResponses;
			}
		}

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
		public StateToggleDisplay explorerViewIndicator
		{
			get
			{
				return explorerView.isActiveIndicator;
			}
		}

		[Obsolete("Use SubscriptionView.isActiveIndicator instead")]
		public StateToggleDisplay subscriptionsViewIndicator
		{
			get
			{
				return subscriptionsView.isActiveIndicator;
			}
		}

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

		public void PushSubscriptionChanges()
		{
			UserAccountManagement.PushSubscriptionChanges(null, null);
		}

		private IEnumerator InitializeModBrowser()
		{
			bool isDone = false;
			yield return null;
			yield return null;
			yield return null;
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
			IEnumerable<IGameProfileUpdateReceiver> gameUpdateReceivers = GetComponentsInChildren<IGameProfileUpdateReceiver>(true);
			foreach (IGameProfileUpdateReceiver receiver in gameUpdateReceivers)
			{
				receiver.OnGameProfileUpdated(m_gameProfile);
			}
			IEnumerable<IAuthenticatedUserUpdateReceiver> userUpdateReceivers = GetComponentsInChildren<IAuthenticatedUserUpdateReceiver>(true);
			foreach (IAuthenticatedUserUpdateReceiver receiver2 in userUpdateReceivers)
			{
				receiver2.OnUserProfileUpdated(LocalUser.Profile);
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
				bool isAttemptingReauth = true;
				UserAccountManagement.ReauthenticateWithStoredExternalAuthData(false, delegate(UserProfile u)
				{
					IEnumerable<IAuthenticatedUserUpdateReceiver> componentsInChildren = GetComponentsInChildren<IAuthenticatedUserUpdateReceiver>(true);
					foreach (IAuthenticatedUserUpdateReceiver item in componentsInChildren)
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
				bool isRequestDone = false;
				WebRequestError requestError = null;
				APIClient.GetGame(delegate(GameProfile g)
				{
					if (!(this == null))
					{
						m_gameProfile = g;
						CacheClient.SaveGameProfile(g, null);
						IEnumerable<IGameProfileUpdateReceiver> componentsInChildren = GetComponentsInChildren<IGameProfileUpdateReceiver>(true);
						foreach (IGameProfileUpdateReceiver item in componentsInChildren)
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
				int reattemptDelay = CalculateReattemptDelay(requestError);
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
					}
					break;
				}
				if (requestError.isRequestUnresolvable || reattemptDelay < 0)
				{
					Debug.LogWarning("[mod.io] Fetching Game Profile failed.\n---[ Response Info ]---\n" + DebugUtilities.GetResponseInfo(requestError.webRequest));
					MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to collect game data from mod.io.\n" + requestError.displayMessage);
					break;
				}
				MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to collect game data from mod.io.\n" + requestError.displayMessage + "\nRetrying in " + reattemptDelay + " seconds");
				yield return new WaitForSecondsRealtime(reattemptDelay);
			}
		}

		private IEnumerator FetchUserProfile()
		{
			bool succeeded = false;
			while (!succeeded)
			{
				bool isRequestDone = false;
				WebRequestError requestError = null;
				UserAccountManagement.UpdateUserProfile(delegate(UserProfile u)
				{
					IEnumerable<IAuthenticatedUserUpdateReceiver> componentsInChildren = GetComponentsInChildren<IAuthenticatedUserUpdateReceiver>(true);
					foreach (IAuthenticatedUserUpdateReceiver item in componentsInChildren)
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
					int reattemptDelay = CalculateReattemptDelay(requestError);
					if (requestError.isAuthenticationInvalid)
					{
						MessageSystem.QueueMessage(MessageDisplayData.Type.Error, requestError.displayMessage);
						yield break;
					}
					if (requestError.isRequestUnresolvable || reattemptDelay < 0)
					{
						Debug.LogWarning("[mod.io] Fetching User Profile failed.\n---[ Response Info ]---\n" + DebugUtilities.GetResponseInfo(requestError.webRequest));
						MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to collect user profile data from mod.io.\n" + requestError.displayMessage);
						yield break;
					}
					MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to collect user profile data from mod.io.\n" + requestError.displayMessage + "\nRetrying in " + reattemptDelay + " seconds");
					yield return new WaitForSecondsRealtime(reattemptDelay);
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
			Action<APIPaginationParameters> requestDelegate = null;
			bool request_isDone = false;
			RequestPage<ModProfile> request_page = null;
			WebRequestError request_error = null;
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
				int[] modIdArray = LocalUser.SubscribedModIds.ToArray();
				if (modIdArray.Length == 0)
				{
					yield break;
				}
				RequestFilter modFilter = new RequestFilter();
				modFilter.AddFieldFilter("id", new InArrayFilter<int>(modIdArray));
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
					int reattemptDelay = CalculateReattemptDelay(request_error);
					if (request_error.isAuthenticationInvalid)
					{
						MessageSystem.QueueMessage(MessageDisplayData.Type.Error, request_error.displayMessage);
						yield break;
					}
					if (request_error.isRequestUnresolvable || reattemptDelay < 0)
					{
						MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to retrieve subscription data from mod.io servers.\n" + request_error.displayMessage);
						yield break;
					}
					MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to retrieve subscription data from mod.io servers.\n" + request_error.displayMessage + "\nRetrying in " + reattemptDelay + " seconds");
					yield return new WaitForSecondsRealtime(reattemptDelay);
					continue;
				}
				ModProfile[] items = request_page.items;
				foreach (ModProfile profile in items)
				{
					if (!queuedUnsubscribes.Contains(profile.id))
					{
						subProfiles.Add(profile);
						if (!localOnlySubscriptions.Remove(profile.id))
						{
							subsAdded.Add(profile.id);
						}
					}
				}
				CacheClient.SaveModProfiles(request_page.items, null);
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
			List<int> subsRemoved = new List<int>();
			foreach (int modId in localOnlySubscriptions)
			{
				if (!queuedSubscribes.Contains(modId))
				{
					subsRemoved.Add(modId);
					LocalUser.SubscribedModIds.Remove(modId);
				}
			}
			LocalUser.SubscribedModIds.AddRange(subsAdded);
			LocalUser.Save();
			OnSubscriptionsChanged(subsAdded, subsRemoved);
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
			RequestFilter requestFilter = new RequestFilter();
			requestFilter.sortFieldName = "id";
			requestFilter.isSortAscending = false;
			RequestFilter requestFilter2 = requestFilter;
			requestFilter2.AddFieldFilter("game_id", new EqualToFilter<int>(0)
			{
				filterValue = PluginSettings.GAME_ID
			});
			APIPaginationParameters aPIPaginationParameters = new APIPaginationParameters();
			aPIPaginationParameters.offset = 0;
			aPIPaginationParameters.limit = 1;
			APIPaginationParameters pagination = aPIPaginationParameters;
			if (LocalUser.AuthenticationState == AuthenticationState.ValidToken)
			{
				APIClient.GetUserEvents(requestFilter2, pagination, delegate(RequestPage<UserEvent> r)
				{
					if (r.items.Length > 0)
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
			APIClient.GetAllModEvents(requestFilter2, pagination, delegate(RequestPage<ModEvent> r)
			{
				if (r.items.Length > 0)
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
			if (LocalUser.AuthenticationState == AuthenticationState.ValidToken && !isRequestDone)
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
					int reattemptDelay = CalculateReattemptDelay(requestError);
					if (requestError.isAuthenticationInvalid)
					{
						MessageSystem.QueueMessage(MessageDisplayData.Type.Error, requestError.displayMessage);
						yield break;
					}
					if (requestError.isRequestUnresolvable || reattemptDelay < 0)
					{
						yield break;
					}
					yield return new WaitForSecondsRealtime(reattemptDelay);
				}
				else
				{
					retrievedRatings.AddRange(response.items);
					isRequestDone = response.size + response.resultOffset >= response.resultTotal;
				}
			}
			_state.userRatings = new Dictionary<int, ModRatingValue>();
			foreach (ModRating rating in retrievedRatings)
			{
				_state.userRatings.Add(rating.modId, rating.ratingValue);
			}
		}

		private IEnumerator VerifySubscriptionInstallations()
		{
			List<int> subscribedModIds = LocalUser.SubscribedModIds;
			Dictionary<int, List<int>> groupedIds = new Dictionary<int, List<int>>();
			foreach (int modId in subscribedModIds)
			{
				groupedIds.Add(modId, new List<int>());
			}
			bool gotModVersions = false;
			IList<ModfileIdPair> installedModVersions = null;
			ModManager.QueryInstalledModVersions(false, delegate(List<ModfileIdPair> r)
			{
				installedModVersions = r;
				gotModVersions = true;
			});
			while (!gotModVersions)
			{
				yield return null;
			}
			foreach (ModfileIdPair idPair in installedModVersions)
			{
				if (subscribedModIds.Contains(idPair.modId))
				{
					groupedIds[idPair.modId].Add(idPair.modfileId);
					continue;
				}
				bool isUninstallDone = false;
				ModManager.UninstallMod(idPair.modId, delegate
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
			ModManager.GetModProfiles(subscribedModIds, delegate(ModProfile[] modProfiles)
			{
				ModProfile profile;
				for (int i = 0; i < modProfiles.Length; i++)
				{
					profile = modProfiles[i];
					if (profile != null && profile.currentBuild != null && LocalUser.SubscribedModIds.Contains(profile.id))
					{
						if (profile.currentBuild.modId != profile.id)
						{
							Debug.LogWarning("[mod.io] Profile '" + profile.name + "(" + profile.id + ") has a bad modfile.\nThe modfile.modId is mismatched (" + profile.currentBuild.modId + ").");
						}
						else if (modfilesToAssert.TrueForAll((Modfile x) => x.modId != profile.currentBuild.modId || x.id != profile.currentBuild.id))
						{
							modfilesToAssert.Add(profile.currentBuild);
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
					if (onError != null)
					{
						onError(error);
					}
					modProfiles = null;
					isDone = true;
				}
				else
				{
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
			bool isFetchRequired = ServerTimeStamp.Now - _state.lastSync_timestamp > 30 || LocalUser.UserId != _state.lastSync_userId;
			if (!m_isSyncInProgress && isFetchRequired)
			{
				m_isSyncInProgress = true;
				int sync_userId = LocalUser.UserId;
				int timestamp = ServerTimeStamp.Now;
				bool invalidUserEvent = LocalUser.AuthenticationState != AuthenticationState.NoToken && _state.userEventId <= 0;
				if (_state.modEventId <= 0 || invalidUserEvent || _state.lastSync_userId != sync_userId)
				{
					yield return StartCoroutine(PerformInitialSubscriptionSync());
					StartCoroutine(VerifySubscriptionInstallations());
				}
				else
				{
					yield return StartCoroutine(PullRemoteEventsAndUpdate());
				}
				int remainingTime = 2 - (ServerTimeStamp.Now - timestamp);
				if (remainingTime > 0)
				{
					yield return new WaitForSeconds(remainingTime);
				}
				m_isSyncInProgress = false;
				_state.lastSync_timestamp = ServerTimeStamp.Now;
				_state.lastSync_userId = sync_userId;
			}
			else
			{
				yield return new WaitForSeconds(2f);
			}
			if (onComplete != null)
			{
				onComplete();
			}
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
			List<int> subbedMods = LocalUser.SubscribedModIds;
			if (subbedMods == null || subbedMods.Count <= 0)
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
					string messageContent = list.Count + " subscription" + ((list.Count <= 1) ? string.Empty : "s") + " retrieved from the server";
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
			List<int> modfileChanged = new List<int>();
			List<int> deletedMods = new List<int>();
			foreach (ModEvent modEvent in modEvents)
			{
				switch (modEvent.eventType)
				{
				case ModEventType.ModfileChanged:
					modfileChanged.Add(modEvent.modId);
					break;
				case ModEventType.ModDeleted:
					deletedMods.Add(modEvent.modId);
					break;
				}
			}
			if (deletedMods.Count > 0)
			{
				List<int> subscribedModIds = LocalUser.SubscribedModIds;
				foreach (int modId in deletedMods)
				{
					subscribedModIds.Remove(modId);
				}
				OnSubscriptionsChanged(null, deletedMods);
				int deletedModCount = deletedMods.Count;
				string message = ((deletedModCount != 1) ? (deletedModCount + " subscribed mods have become unavailable and have been removed from your subscriptions.") : "One of your subscribed mods is now unavailable and was removed from your subscriptions.");
				MessageSystem.QueueMessage(MessageDisplayData.Type.Info, message);
			}
			if (modfileChanged.Count <= 0)
			{
				yield break;
			}
			APIPaginationParameters pagination = new APIPaginationParameters
			{
				limit = 100,
				offset = 0
			};
			RequestFilter modFilter = new RequestFilter();
			modFilter.sortFieldName = "id";
			modFilter.AddFieldFilter("id", new InArrayFilter<int>
			{
				filterArray = modfileChanged.ToArray()
			});
			bool isRequestDone = false;
			RequestPage<ModProfile> response = null;
			WebRequestError requestError = null;
			APIClient.GetAllMods(modFilter, pagination, delegate(RequestPage<ModProfile> r)
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
				}
				else
				{
					MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to update installed mods.\n" + requestError.displayMessage);
				}
				yield break;
			}
			List<Modfile> latestBuilds = new List<Modfile>(response.items.Length);
			List<int> subscribedModIds2 = LocalUser.SubscribedModIds;
			ModProfile[] items = response.items;
			foreach (ModProfile profile in items)
			{
				if (profile != null && profile.currentBuild != null && subscribedModIds2.Contains(profile.id))
				{
					latestBuilds.Add(profile.currentBuild);
				}
			}
			yield return StartCoroutine(ModManager.AssertDownloadedAndInstalled_Coroutine(latestBuilds));
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
			IEnumerable<IAuthenticatedUserUpdateReceiver> componentsInChildren = GetComponentsInChildren<IAuthenticatedUserUpdateReceiver>(true);
			foreach (IAuthenticatedUserUpdateReceiver item in componentsInChildren)
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
			ModManager.GetModProfile(modId, delegate(ModProfile p)
			{
				if (this != null && base.isActiveAndEnabled && p != null && p.currentBuild != null && LocalUser.SubscribedModIds.Contains(p.id))
				{
					StartCoroutine(ModManager.AssertDownloadedAndInstalled_Coroutine(new Modfile[1] { p.currentBuild }));
				}
			}, delegate(WebRequestError requestError)
			{
				if (requestError.isAuthenticationInvalid)
				{
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
			DownloadClient.CancelAnyModBinaryDownloads(modId, delegate
			{
				CacheClient.DeleteAllModfileAndBinaryData(modId, null);
				ModManager.UninstallMod(modId, null);
			});
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
				ModManager.GetModProfiles(addedSubscriptions, delegate(ModProfile[] modProfiles)
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
			IEnumerable<IModSubscriptionsUpdateReceiver> componentsInChildren = GetComponentsInChildren<IModSubscriptionsUpdateReceiver>(true);
			foreach (IModSubscriptionsUpdateReceiver item in componentsInChildren)
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
			IEnumerable<IModEnabledReceiver> componentsInChildren = GetComponentsInChildren<IModEnabledReceiver>(true);
			foreach (IModEnabledReceiver item in componentsInChildren)
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
			IEnumerable<IModDisabledReceiver> componentsInChildren = GetComponentsInChildren<IModDisabledReceiver>(true);
			foreach (IModDisabledReceiver item in componentsInChildren)
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
				IEnumerable<IModRatingAddedReceiver> ratingReceivers = GetComponentsInChildren<IModRatingAddedReceiver>(true);
				foreach (IModRatingAddedReceiver item in ratingReceivers)
				{
					item.OnModRatingAdded(modId, ratingValue);
				}
				AddModRatingParameters addModRatingParameters = new AddModRatingParameters();
				addModRatingParameters.ratingValue = ratingValue;
				AddModRatingParameters parameters = addModRatingParameters;
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
									if (item2 != null)
									{
										item2.OnModRatingAdded(modId, oldRating);
									}
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
			ModRatingValue value;
			if (!_state.userRatings.TryGetValue(modId, out value))
			{
				return ModRatingValue.None;
			}
			return value;
		}

		private void OnModInstalled(ModfileIdPair idPair)
		{
			if (!(this == null))
			{
				ModManager.GetModProfile(idPair.modId, delegate(ModProfile p)
				{
					MessageSystem.QueueMessage(MessageDisplayData.Type.Info, p.name + " was successfully downloaded and installed.");
				}, null);
			}
		}

		private void OnModfileDownloadFailed(ModfileIdPair idPair, WebRequestError error)
		{
			if (!(this == null))
			{
				ModManager.GetModProfile(idPair.modId, delegate(ModProfile p)
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
				if (onError != null)
				{
					onError(null);
				}
			}
			else
			{
				ModProfileRequestManager.instance.FetchModProfilePage(explorerView.GenerateRequestFilter(), pageIndex * explorerView.itemsPerPage, explorerView.itemsPerPage, onSuccess, onError);
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
			inspectorView.gameObject.SetActive(false);
		}

		[Obsolete("Use LoginDialog.gameObject.SetActive(true) instead.")]
		public void OpenLoginDialog()
		{
			loginDialog.gameObject.SetActive(true);
		}

		[Obsolete("Use LoginDialog.gameObject.SetActive(false) instead.")]
		public void CloseLoginDialog()
		{
			loginDialog.gameObject.SetActive(false);
		}
	}
}
