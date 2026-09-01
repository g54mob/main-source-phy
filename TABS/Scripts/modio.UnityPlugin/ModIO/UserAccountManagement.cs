using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModIO
{
	public static class UserAccountManagement
	{
		public static void SubscribeToMod(int modId)
		{
			if (!LocalUser.SubscribedModIds.Contains(modId))
			{
				LocalUser.SubscribedModIds.Add(modId);
			}
			bool num = LocalUser.QueuedUnsubscribes.Contains(modId);
			bool flag = LocalUser.QueuedSubscribes.Contains(modId);
			if (num)
			{
				LocalUser.QueuedUnsubscribes.Remove(modId);
			}
			else if (!flag)
			{
				LocalUser.QueuedSubscribes.Add(modId);
			}
			LocalUser.Save();
		}

		public static void UnsubscribeFromMod(int modId)
		{
			LocalUser.SubscribedModIds.Remove(modId);
			bool flag = LocalUser.QueuedUnsubscribes.Contains(modId);
			if (LocalUser.QueuedSubscribes.Contains(modId))
			{
				LocalUser.QueuedSubscribes.Remove(modId);
			}
			else if (!flag)
			{
				LocalUser.QueuedUnsubscribes.Add(modId);
			}
			LocalUser.Save();
		}

		public static void PushSubscriptionChanges(Action onCompletedNoErrors, Action<List<WebRequestError>> onCompletedWithErrors)
		{
			int responsesPending = LocalUser.QueuedSubscribes.Count + LocalUser.QueuedUnsubscribes.Count;
			if (LocalUser.AuthenticationState == AuthenticationState.NoToken || responsesPending == 0)
			{
				if (onCompletedNoErrors != null)
				{
					onCompletedNoErrors();
				}
				return;
			}
			string userToken = LocalUser.OAuthToken;
			List<WebRequestError> errors = new List<WebRequestError>();
			List<int> subscribesPushed = new List<int>(LocalUser.QueuedSubscribes.Count);
			List<int> unsubscribesPushed = new List<int>(LocalUser.QueuedUnsubscribes.Count);
			Action onRequestCompleted = delegate
			{
				if (responsesPending <= 0)
				{
					if (userToken == LocalUser.OAuthToken)
					{
						foreach (int item in subscribesPushed)
						{
							LocalUser.QueuedSubscribes.Remove(item);
						}
						foreach (int item2 in unsubscribesPushed)
						{
							LocalUser.QueuedUnsubscribes.Remove(item2);
						}
						LocalUser.Save();
					}
					if (errors.Count == 0 && onCompletedNoErrors != null)
					{
						onCompletedNoErrors();
					}
					else if (errors.Count > 0 && onCompletedWithErrors != null)
					{
						onCompletedWithErrors(errors);
					}
				}
			};
			foreach (int modId in LocalUser.QueuedSubscribes)
			{
				APIClient.SubscribeToMod(modId, delegate
				{
					subscribesPushed.Add(modId);
					int num = responsesPending - 1;
					responsesPending = num;
					onRequestCompleted();
				}, delegate(WebRequestError e)
				{
					if (e.webRequest.responseCode == 400)
					{
						subscribesPushed.Add(modId);
					}
					else if (e.webRequest.responseCode == 404)
					{
						subscribesPushed.Add(modId);
					}
					else
					{
						errors.Add(e);
					}
					int num = responsesPending - 1;
					responsesPending = num;
					onRequestCompleted();
				});
			}
			foreach (int modId2 in LocalUser.QueuedUnsubscribes)
			{
				APIClient.UnsubscribeFromMod(modId2, delegate
				{
					int num = responsesPending - 1;
					responsesPending = num;
					unsubscribesPushed.Remove(modId2);
					onRequestCompleted();
				}, delegate(WebRequestError e)
				{
					if (e.webRequest.responseCode == 400)
					{
						unsubscribesPushed.Remove(modId2);
					}
					else if (e.webRequest.responseCode == 404)
					{
						unsubscribesPushed.Remove(modId2);
					}
					else
					{
						errors.Add(e);
					}
					int num = responsesPending - 1;
					responsesPending = num;
					onRequestCompleted();
				});
			}
		}

		public static void PullSubscriptionChanges(Action<List<ModProfile>> onSuccess, Action<WebRequestError> onError)
		{
			if (LocalUser.AuthenticationState == AuthenticationState.NoToken)
			{
				if (onSuccess != null)
				{
					onSuccess(new List<ModProfile>(0));
				}
				return;
			}
			string userToken = LocalUser.OAuthToken;
			List<ModProfile> remoteOnlySubscriptions = new List<ModProfile>();
			RequestFilter subscriptionFilter = new RequestFilter();
			subscriptionFilter.AddFieldFilter("game_id", new EqualToFilter<int>(PluginSettings.GAME_ID));
			APIPaginationParameters pagination = new APIPaginationParameters
			{
				limit = 100,
				offset = 0
			};
			Action getNextPage = null;
			Action<RequestPage<ModProfile>> onPageReceived = null;
			Action onAllPagesReceived = null;
			getNextPage = delegate
			{
				APIClient.GetUserSubscriptions(subscriptionFilter, pagination, delegate(RequestPage<ModProfile> response)
				{
					onPageReceived(response);
					if (response != null && response.items != null && response.items.Length != 0 && response.resultTotal > response.size + response.resultOffset)
					{
						pagination.offset = response.resultOffset + response.size;
						getNextPage();
					}
					else
					{
						onAllPagesReceived();
						if (onSuccess != null)
						{
							onSuccess(remoteOnlySubscriptions);
						}
					}
				}, delegate(WebRequestError e)
				{
					if (onError != null)
					{
						onError(e);
					}
				});
			};
			onPageReceived = delegate(RequestPage<ModProfile> r)
			{
				ModProfile[] items = r.items;
				foreach (ModProfile modProfile in items)
				{
					if (modProfile != null)
					{
						remoteOnlySubscriptions.Add(modProfile);
					}
				}
			};
			onAllPagesReceived = delegate
			{
				if (!(userToken != LocalUser.OAuthToken))
				{
					List<int> list = new List<int>(LocalUser.SubscribedModIds);
					foreach (int queuedUnsubscribe in LocalUser.QueuedUnsubscribes)
					{
						list.Remove(queuedUnsubscribe);
					}
					List<int> list2 = new List<int>();
					for (int i = 0; i < remoteOnlySubscriptions.Count; i++)
					{
						ModProfile modProfile = remoteOnlySubscriptions[i];
						LocalUser.QueuedSubscribes.Remove(modProfile.id);
						if (LocalUser.QueuedUnsubscribes.Contains(modProfile.id))
						{
							remoteOnlySubscriptions.RemoveAt(i);
							i--;
						}
						else if (list.Remove(modProfile.id))
						{
							remoteOnlySubscriptions.RemoveAt(i);
							i--;
						}
						else
						{
							list2.Add(modProfile.id);
						}
					}
					foreach (int item in list)
					{
						if (!LocalUser.QueuedSubscribes.Contains(item))
						{
							LocalUser.SubscribedModIds.Remove(item);
						}
					}
					LocalUser.SubscribedModIds.AddRange(list2);
					LocalUser.Save();
				}
			};
			getNextPage();
		}

		public static void UpdateUserProfile(Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			if (LocalUser.AuthenticationState != AuthenticationState.NoToken)
			{
				APIClient.GetAuthenticatedUser(delegate(UserProfile p)
				{
					LocalUser.Profile = p;
					LocalUser.Save();
					if (onSuccess != null)
					{
						onSuccess(p);
					}
				}, onError);
			}
			else if (onSuccess != null)
			{
				onSuccess(null);
			}
		}

		public static void AuthenticateWithSecurityCode(string securityCode, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			APIClient.GetOAuthToken(securityCode, delegate(string t)
			{
				LocalUser.OAuthToken = t;
				LocalUser.WasTokenRejected = false;
				LocalUser.Save();
				UpdateUserProfile(onSuccess, onError);
			}, onError);
		}

		public static void AuthenticateWithSteamEncryptedAppTicket(byte[] pTicket, uint pcbTicket, bool hasUserAcceptedTerms, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			AuthenticateWithSteamEncryptedAppTicket(Utility.EncodeEncryptedAppTicket(pTicket, pcbTicket), hasUserAcceptedTerms, onSuccess, onError);
		}

		public static void AuthenticateWithSteamEncryptedAppTicket(byte[] authTicketData, bool hasUserAcceptedTerms, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			AuthenticateWithSteamEncryptedAppTicket(Utility.EncodeEncryptedAppTicket(authTicketData, (uint)authTicketData.Length), hasUserAcceptedTerms, onSuccess, onError);
		}

		public static void AuthenticateWithSteamEncryptedAppTicket(string encodedTicket, bool hasUserAcceptedTerms, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			LocalUser.ExternalAuthentication = new ExternalAuthenticationData
			{
				ticket = encodedTicket,
				portal = UserPortal.Steam
			};
			APIClient.RequestSteamAuthentication(encodedTicket, hasUserAcceptedTerms, delegate(string t)
			{
				LocalUser.OAuthToken = t;
				LocalUser.WasTokenRejected = false;
				LocalUser.Save();
				UpdateUserProfile(onSuccess, onError);
			}, onError);
		}

		public static void AuthenticateWithGOGEncryptedAppTicket(byte[] data, uint dataSize, bool hasUserAcceptedTerms, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			AuthenticateWithGOGEncryptedAppTicket(Utility.EncodeEncryptedAppTicket(data, dataSize), hasUserAcceptedTerms, onSuccess, onError);
		}

		public static void AuthenticateWithGOGEncryptedAppTicket(string encodedTicket, bool hasUserAcceptedTerms, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			LocalUser.ExternalAuthentication = new ExternalAuthenticationData
			{
				ticket = encodedTicket,
				portal = UserPortal.Steam
			};
			APIClient.RequestGOGAuthentication(encodedTicket, hasUserAcceptedTerms, delegate(string t)
			{
				LocalUser.OAuthToken = t;
				LocalUser.WasTokenRejected = false;
				LocalUser.Save();
				UpdateUserProfile(onSuccess, onError);
			}, onError);
		}

		public static void AuthenticateWithItchIOToken(string jwtToken, bool hasUserAcceptedTerms, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			LocalUser.ExternalAuthentication = new ExternalAuthenticationData
			{
				ticket = jwtToken,
				portal = UserPortal.itchio
			};
			APIClient.RequestItchIOAuthentication(jwtToken, hasUserAcceptedTerms, delegate(string t)
			{
				LocalUser.OAuthToken = t;
				LocalUser.WasTokenRejected = false;
				LocalUser.Save();
				UpdateUserProfile(onSuccess, onError);
			}, onError);
		}

		public static void AuthenticateWithOculusRiftUserData(string oculusUserNonce, int oculusUserId, string oculusUserAccessToken, bool hasUserAcceptedTerms, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			LocalUser.ExternalAuthentication = new ExternalAuthenticationData
			{
				portal = UserPortal.Oculus,
				ticket = oculusUserAccessToken,
				additionalData = new Dictionary<string, string>
				{
					{ "oculusRiftNonce", oculusUserNonce },
					{
						"oculusRiftId",
						oculusUserId.ToString()
					}
				}
			};
			APIClient.RequestOculusRiftAuthentication(oculusUserNonce, oculusUserId, oculusUserAccessToken, hasUserAcceptedTerms, delegate(string t)
			{
				LocalUser.OAuthToken = t;
				LocalUser.WasTokenRejected = false;
				LocalUser.Save();
				UpdateUserProfile(onSuccess, onError);
			}, onError);
		}

		public static void AuthenticateWithXboxLiveToken(string xboxLiveUserToken, bool hasUserAcceptedTerms, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			LocalUser.ExternalAuthentication = new ExternalAuthenticationData
			{
				ticket = xboxLiveUserToken,
				portal = UserPortal.XboxLive
			};
			APIClient.RequestXboxLiveAuthentication(xboxLiveUserToken, hasUserAcceptedTerms, delegate(string t)
			{
				LocalUser.OAuthToken = t;
				LocalUser.WasTokenRejected = false;
				LocalUser.Save();
				UpdateUserProfile(onSuccess, onError);
			}, onError);
		}

		public static void AuthenticateWithPlayStationAuthCode(string authcode, PlayStationEnvironment environment, bool hasUserAcceptedTerms, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			LocalUser.ExternalAuthentication = new ExternalAuthenticationData
			{
				ticket = authcode,
				portal = UserPortal.PlayStationNetwork,
				playStationEnvironment = environment
			};
			APIClient.RequestPlayStationAuthentication(authcode, hasUserAcceptedTerms, environment, delegate(string t)
			{
				LocalUser.OAuthToken = t;
				LocalUser.WasTokenRejected = false;
				LocalUser.Save();
				UpdateUserProfile(onSuccess, onError);
			}, onError);
		}

		public static void ReauthenticateWithStoredExternalAuthData(bool hasUserAcceptedTerms, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			ExternalAuthenticationData externalAuthentication = LocalUser.ExternalAuthentication;
			Action<string> successCallback = delegate(string t)
			{
				LocalUser.OAuthToken = t;
				LocalUser.WasTokenRejected = false;
				LocalUser.Save();
				if (onSuccess != null)
				{
					UpdateUserProfile(onSuccess, onError);
				}
			};
			switch (LocalUser.ExternalAuthentication.portal)
			{
			case UserPortal.Steam:
				APIClient.RequestSteamAuthentication(externalAuthentication.ticket, hasUserAcceptedTerms, successCallback, onError);
				break;
			case UserPortal.GOG:
				APIClient.RequestGOGAuthentication(externalAuthentication.ticket, hasUserAcceptedTerms, successCallback, onError);
				break;
			case UserPortal.itchio:
				APIClient.RequestItchIOAuthentication(externalAuthentication.ticket, hasUserAcceptedTerms, successCallback, onError);
				break;
			case UserPortal.Oculus:
			{
				string ticket = externalAuthentication.ticket;
				string value = null;
				string value2 = null;
				int result = -1;
				string text = null;
				if (externalAuthentication.additionalData == null)
				{
					text = "The user id and nonce are missing.";
				}
				else if (!externalAuthentication.additionalData.TryGetValue("oculusRiftNonce", out value) || string.IsNullOrEmpty(value))
				{
					text = "The nonce is missing.";
				}
				else if (!externalAuthentication.additionalData.TryGetValue("oculusRiftId", out value2) || string.IsNullOrEmpty(value2))
				{
					text = "The user id is missing.";
				}
				else if (!int.TryParse(value2, out result))
				{
					text = "The user id is not parseable as an integer.";
				}
				if (text != null)
				{
					Debug.LogWarning("[mod.io] Unable to authenticate using stored Oculus Rift user data.\n" + text);
					if (onError != null)
					{
						WebRequestError obj = WebRequestError.GenerateLocal(text);
						onError(obj);
					}
				}
				else
				{
					APIClient.RequestOculusRiftAuthentication(value, result, ticket, hasUserAcceptedTerms, successCallback, onError);
				}
				break;
			}
			case UserPortal.XboxLive:
				APIClient.RequestXboxLiveAuthentication(externalAuthentication.ticket, hasUserAcceptedTerms, successCallback, onError);
				break;
			case UserPortal.PlayStationNetwork:
				APIClient.RequestPlayStationAuthentication(externalAuthentication.ticket, hasUserAcceptedTerms, externalAuthentication.playStationEnvironment, successCallback, onError);
				break;
			default:
				throw new NotImplementedException();
			}
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void AuthenticateWithSteamEncryptedAppTicket(byte[] pTicket, uint pcbTicket, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			AuthenticateWithSteamEncryptedAppTicket(pTicket, pcbTicket, hasUserAcceptedTerms: false, onSuccess, onError);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void AuthenticateWithSteamEncryptedAppTicket(byte[] authTicketData, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			AuthenticateWithSteamEncryptedAppTicket(authTicketData, hasUserAcceptedTerms: false, onSuccess, onError);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void AuthenticateWithSteamEncryptedAppTicket(string encodedTicket, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			AuthenticateWithSteamEncryptedAppTicket(encodedTicket, hasUserAcceptedTerms: false, onSuccess, onError);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void AuthenticateWithGOGEncryptedAppTicket(byte[] data, uint dataSize, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			AuthenticateWithGOGEncryptedAppTicket(data, dataSize, hasUserAcceptedTerms: false, onSuccess, onError);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void AuthenticateWithGOGEncryptedAppTicket(string encodedTicket, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			AuthenticateWithGOGEncryptedAppTicket(encodedTicket, hasUserAcceptedTerms: false, onSuccess, onError);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void AuthenticateWithItchIOToken(string jwtToken, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			AuthenticateWithItchIOToken(jwtToken, hasUserAcceptedTerms: false, onSuccess, onError);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void AuthenticateWithOculusRiftUserData(string oculusUserNonce, int oculusUserId, string oculusUserAccessToken, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			AuthenticateWithOculusRiftUserData(oculusUserNonce, oculusUserId, oculusUserAccessToken, hasUserAcceptedTerms: false, onSuccess, onError);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void AuthenticateWithXboxLiveToken(string xboxLiveUserToken, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			AuthenticateWithXboxLiveToken(xboxLiveUserToken, hasUserAcceptedTerms: true, onSuccess, onError);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void ReauthenticateWithStoredExternalAuthData(Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			ReauthenticateWithStoredExternalAuthData(hasUserAcceptedTerms: true, onSuccess, onError);
		}
	}
}
