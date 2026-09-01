using System;
using System.Collections.Generic;
using ModIO.API;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ModIO
{
	public static class APIClient
	{
		private struct GetRequestHandle
		{
			public UnityWebRequestAsyncOperation operation;

			public List<Action<string>> successCallbacks;

			public List<Action<WebRequestError>> errorCallbacks;
		}

		[Serializable]
		private struct AccessTokenObject
		{
			public string access_token;
		}

		public const string API_VERSION = "v1";

		public const string API_URL_TESTSERVER = "https://api.test.mod.io/";

		public const string API_URL_PRODUCTIONSERVER = "https://api.mod.io/";

		public const string PLATFORM_HEADER_KEY = "x-modio-platform";

		public const string PLATFORM_HEADER_VALUE = "windows";

		public const string PORTAL_HEADER_KEY = "x-modio-portal";

		public static readonly string USER_AGENT_HEADER = "modioUnityPlugin-" + ModIOVersion.Current.ToString("X.Y.Z");

		public static readonly string EXTERNAL_AUTH_CONSENT_KEY = "terms_agreed";

		public static string userDisplayName;

		public static readonly string[] MODIO_REQUEST_HEADER_KEYS = new string[8] { "authorization", "accept-language", "content-type", "x-unity-version", "user-agent", "x-modio-platform", "x-modio-portal", EXTERNAL_AUTH_CONSENT_KEY };

		public static string languageCode = "en";

		private static Dictionary<string, GetRequestHandle> _activeGetRequests = new Dictionary<string, GetRequestHandle>();

		public static bool AssertAuthorizationDetails(bool isUserTokenRequired)
		{
			if (PluginSettings.GAME_ID <= 0 || string.IsNullOrEmpty(PluginSettings.GAME_API_KEY))
			{
				Debug.LogError("[mod.io] No API requests can be executed without a valid Game Id and Game API Key. These need to be saved into the Plugin Settings (mod.io > Edit Settings before any requests can be sent to the API.");
				return false;
			}
			if (isUserTokenRequired)
			{
				if (string.IsNullOrEmpty(LocalUser.OAuthToken))
				{
					Debug.LogError("[mod.io] API request to modification or User-specific endpoints cannot be made without first setting the User Authorization Data instance with a valid token.");
					return false;
				}
				if (LocalUser.WasTokenRejected)
				{
					Debug.LogWarning("[mod.io] An API request is being made with a UserAuthenticationData token that has been flagged as previously rejected. A check to ensure LocalUser.AuthenticationState == AuthenticationState.ValidToken should be made prior to making user-authorization calls.");
				}
			}
			return true;
		}

		public static string BuildEndpointURL(string baseURL, string filterString, APIPaginationParameters pagination)
		{
			string text = ((pagination != null) ? ("&_limit=" + pagination.limit + "&_offset=" + pagination.offset) : string.Empty);
			return baseURL + "?" + filterString + text;
		}

		public static void ApplyStandardHeaders(UnityWebRequest webRequest)
		{
			webRequest.SetRequestHeader("Accept-Language", languageCode);
			webRequest.SetRequestHeader("x-modio-platform", "windows");
			webRequest.SetRequestHeader("x-modio-portal", ServerConstants.ConvertUserPortalToHeaderValue(PluginSettings.USER_PORTAL));
		}

		public static UnityWebRequest GenerateQuery(string endpointURL, string filterString, APIPaginationParameters pagination)
		{
			AssertAuthorizationDetails(false);
			string uri = BuildEndpointURL(endpointURL, filterString, pagination);
			UnityWebRequest unityWebRequest = UnityWebRequest.Get(uri);
			if (LocalUser.AuthenticationState == AuthenticationState.ValidToken)
			{
				unityWebRequest.SetRequestHeader("Authorization", "Bearer " + LocalUser.OAuthToken);
			}
			else
			{
				unityWebRequest.url = unityWebRequest.url + "&api_key=" + PluginSettings.GAME_API_KEY;
			}
			ApplyStandardHeaders(unityWebRequest);
			return unityWebRequest;
		}

		public static UnityWebRequest GenerateGetRequest(string endpointURL, string filterString, APIPaginationParameters pagination)
		{
			AssertAuthorizationDetails(true);
			string uri = BuildEndpointURL(endpointURL, filterString, pagination);
			UnityWebRequest unityWebRequest = UnityWebRequest.Get(uri);
			unityWebRequest.SetRequestHeader("Authorization", "Bearer " + LocalUser.OAuthToken);
			ApplyStandardHeaders(unityWebRequest);
			return unityWebRequest;
		}

		public static UnityWebRequest GeneratePutRequest(string endpointURL, StringValueParameter[] valueFields)
		{
			AssertAuthorizationDetails(true);
			WWWForm wWWForm = new WWWForm();
			if (valueFields != null)
			{
				foreach (StringValueParameter stringValueParameter in valueFields)
				{
					wWWForm.AddField(stringValueParameter.key, stringValueParameter.value);
				}
			}
			UnityWebRequest unityWebRequest;
			if (valueFields == null || valueFields.Length == 0)
			{
				unityWebRequest = new UnityWebRequest(endpointURL, "PUT");
				unityWebRequest.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
				unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			}
			else
			{
				unityWebRequest = UnityWebRequest.Post(endpointURL, wWWForm);
				unityWebRequest.method = "PUT";
			}
			unityWebRequest.SetRequestHeader("Authorization", "Bearer " + LocalUser.OAuthToken);
			ApplyStandardHeaders(unityWebRequest);
			return unityWebRequest;
		}

		public static UnityWebRequest GeneratePostRequest(string endpointURL, StringValueParameter[] valueFields, BinaryDataParameter[] dataFields)
		{
			AssertAuthorizationDetails(true);
			WWWForm wWWForm = new WWWForm();
			if (valueFields != null)
			{
				foreach (StringValueParameter stringValueParameter in valueFields)
				{
					wWWForm.AddField(stringValueParameter.key, stringValueParameter.value);
				}
			}
			if (dataFields != null)
			{
				foreach (BinaryDataParameter binaryDataParameter in dataFields)
				{
					wWWForm.AddBinaryData(binaryDataParameter.key, binaryDataParameter.contents, binaryDataParameter.fileName, binaryDataParameter.mimeType);
				}
			}
			UnityWebRequest unityWebRequest;
			if (wWWForm.data.Length == 0)
			{
				unityWebRequest = new UnityWebRequest(endpointURL, "POST");
				unityWebRequest.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
				unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			}
			else
			{
				unityWebRequest = UnityWebRequest.Post(endpointURL, wWWForm);
			}
			unityWebRequest.SetRequestHeader("Authorization", "Bearer " + LocalUser.OAuthToken);
			ApplyStandardHeaders(unityWebRequest);
			return unityWebRequest;
		}

		public static UnityWebRequest GenerateDeleteRequest(string endpointURL, StringValueParameter[] valueFields)
		{
			AssertAuthorizationDetails(true);
			WWWForm wWWForm = new WWWForm();
			if (valueFields != null)
			{
				foreach (StringValueParameter stringValueParameter in valueFields)
				{
					wWWForm.AddField(stringValueParameter.key, stringValueParameter.value);
				}
			}
			UnityWebRequest unityWebRequest;
			if (valueFields == null || valueFields.Length == 0)
			{
				unityWebRequest = new UnityWebRequest(endpointURL, "DELETE");
				unityWebRequest.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
				unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			}
			else
			{
				unityWebRequest = UnityWebRequest.Post(endpointURL, wWWForm);
				unityWebRequest.method = "DELETE";
			}
			unityWebRequest.SetRequestHeader("Authorization", "Bearer " + LocalUser.OAuthToken);
			ApplyStandardHeaders(unityWebRequest);
			return unityWebRequest;
		}

		public static UnityWebRequestAsyncOperation SendRequest(UnityWebRequest webRequest, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = null;
			if (webRequest.method == "GET")
			{
				string response = null;
				if (RequestCache.TryGetResponse(webRequest.url, out response))
				{
					if (successCallback != null)
					{
						successCallback(response);
					}
					return null;
				}
				GetRequestHandle value;
				if (!_activeGetRequests.TryGetValue(webRequest.url, out value))
				{
					value = new GetRequestHandle
					{
						operation = null,
						successCallbacks = new List<Action<string>>(),
						errorCallbacks = new List<Action<WebRequestError>>()
					};
					value.operation = webRequest.SendWebRequest();
					value.operation.completed += HandleGetResponse;
					_activeGetRequests.Add(webRequest.url, value);
				}
				if (successCallback != null)
				{
					value.successCallbacks.Add(successCallback);
				}
				if (errorCallback != null)
				{
					value.errorCallbacks.Add(errorCallback);
				}
				unityWebRequestAsyncOperation = value.operation;
			}
			else
			{
				unityWebRequestAsyncOperation = webRequest.SendWebRequest();
				unityWebRequestAsyncOperation.completed += delegate
				{
					string success = null;
					WebRequestError error = null;
					ProcessRequestResponse(webRequest, out success, out error);
					if (error != null)
					{
						if (errorCallback != null)
						{
							errorCallback(error);
						}
					}
					else if (successCallback != null)
					{
						successCallback(success);
					}
				};
			}
			return unityWebRequestAsyncOperation;
		}

		public static UnityWebRequestAsyncOperation SendRequest<T>(UnityWebRequest webRequest, Action<T> successCallback, Action<WebRequestError> errorCallback)
		{
			Action<string> successCallback2 = delegate(string responseBody)
			{
				if (successCallback != null)
				{
					T obj = default(T);
					try
					{
						obj = JsonConvert.DeserializeObject<T>(responseBody);
					}
					catch (Exception e)
					{
						Debug.LogWarning("[mod.io] Failed to convert response into " + typeof(T).ToString() + " representation\n\n" + Utility.GenerateExceptionDebugString(e));
					}
					successCallback(obj);
				}
			};
			return SendRequest(webRequest, successCallback2, errorCallback);
		}

		public static UnityWebRequestAsyncOperation SendRequest(UnityWebRequest webRequest, Action successCallback, Action<WebRequestError> errorCallback)
		{
			return SendRequest(webRequest, (Action<string>)delegate
			{
				if (successCallback != null)
				{
					successCallback();
				}
			}, errorCallback);
		}

		private static void HandleGetResponse(UnityWebRequestAsyncOperation operation)
		{
			if (operation == null)
			{
				Debug.LogWarning("[mod.io] Attempted to process response a null operation.");
				return;
			}
			if (operation == null || operation.webRequest == null)
			{
				Debug.LogWarning("[mod.io] Unable to retrieve UnityWebRequest from operation.");
				return;
			}
			string url = operation.webRequest.url;
			GetRequestHandle value;
			if (!_activeGetRequests.TryGetValue(url, out value))
			{
				Debug.LogWarning("[mod.io] Unable to retrieve the GetRequestHandle for the url: " + url);
				return;
			}
			_activeGetRequests.Remove(url);
			string success = null;
			WebRequestError error = null;
			ProcessRequestResponse(operation.webRequest, out success, out error);
			if (error != null)
			{
				foreach (Action<WebRequestError> errorCallback in value.errorCallbacks)
				{
					if (errorCallback != null)
					{
						errorCallback(error);
					}
				}
				return;
			}
			RequestCache.StoreResponse(url, success);
			foreach (Action<string> successCallback in value.successCallbacks)
			{
				if (successCallback != null)
				{
					successCallback(success);
				}
			}
		}

		private static void ProcessRequestResponse(UnityWebRequest webRequest, out string success, out WebRequestError error)
		{
			success = null;
			error = null;
			string requestHeader = webRequest.GetRequestHeader("authorization");
			bool flag = !string.IsNullOrEmpty(requestHeader) && requestHeader.StartsWith("Bearer ");
			if (flag && LocalUser.OAuthToken != requestHeader.Substring(7))
			{
				error = WebRequestError.GenerateLocal("User token changed while waiting for the request to complete.");
				return;
			}
			if (webRequest.isNetworkError() || webRequest.isHttpError())
			{
				error = WebRequestError.GenerateFromWebRequest(webRequest);
				if (flag && error.isAuthenticationInvalid)
				{
					LocalUser.WasTokenRejected |= error.isAuthenticationInvalid;
					LocalUser.Save();
				}
				return;
			}
			success = string.Empty;
			if (webRequest.downloadHandler == null || webRequest.downloadHandler is FileDownloadHandler)
			{
				return;
			}
			try
			{
				success = webRequest.downloadHandler.text;
			}
			catch
			{
				success = string.Empty;
			}
		}

		public static void GetTermsOfUse(Action<TermsOfUseInfo> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/authenticate/terms";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, string.Empty, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static UnityWebRequest GenerateAuthenticationRequest(string endpointURL, bool hasUserAcceptedTerms, string displayNamePortal, string authenticationKey, string authenticationValue)
		{
			KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(authenticationKey, authenticationValue);
			return GenerateAuthenticationRequest(endpointURL, hasUserAcceptedTerms, displayNamePortal, keyValuePair);
		}

		public static UnityWebRequest GenerateAuthenticationRequest(string endpointURL, bool hasUserAcceptedTerms, string displayNamePortal, params KeyValuePair<string, string>[] authData)
		{
			AssertAuthorizationDetails(false);
			WWWForm wWWForm = new WWWForm();
			wWWForm.AddField("api_key", PluginSettings.GAME_API_KEY);
			for (int i = 0; i < authData.Length; i++)
			{
				KeyValuePair<string, string> keyValuePair = authData[i];
				wWWForm.AddField(keyValuePair.Key, keyValuePair.Value);
			}
			wWWForm.AddField(EXTERNAL_AUTH_CONSENT_KEY, hasUserAcceptedTerms.ToString());
			if (displayNamePortal != null)
			{
				wWWForm.AddField("display_name_portal", displayNamePortal);
				Debug.LogWarning("[APIClient] GenerateAuthenticationRequest Using display name " + displayNamePortal + "!");
			}
			else
			{
				Debug.LogWarning("[APIClient] GenerateAuthenticationRequest No display name supplied for the authentication request!");
			}
			UnityWebRequest unityWebRequest = UnityWebRequest.Post(endpointURL, wWWForm);
			ApplyStandardHeaders(unityWebRequest);
			return unityWebRequest;
		}

		public static void SendSecurityCode(string emailAddress, Action<APIMessage> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/oauth/emailrequest";
			UnityWebRequest webRequest = GenerateAuthenticationRequest(endpointURL, false, userDisplayName, "email", emailAddress);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetOAuthToken(string securityCode, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/oauth/emailexchange";
			UnityWebRequest webRequest = GenerateAuthenticationRequest(endpointURL, false, userDisplayName, "security_code", securityCode);
			Action<AccessTokenObject> successCallback2 = delegate(AccessTokenObject result)
			{
				successCallback(result.access_token);
			};
			SendRequest(webRequest, successCallback2, errorCallback);
		}

		public static void RequestSteamAuthentication(byte[] pTicket, uint pcbTicket, bool hasUserAcceptedTerms, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			if (pTicket == null || pTicket.Length == 0 || pTicket.Length > 1024)
			{
				Debug.LogWarning("[mod.io] Steam Ticket is invalid. Ensure that the pTicket is not null, and is less than 1024 bytes.");
				if (errorCallback != null)
				{
					errorCallback(WebRequestError.GenerateLocal("Steam Ticket is invalid. Ensure that the pTicket is not null, and is less than 1024 bytes."));
				}
				return;
			}
			string text = Utility.EncodeEncryptedAppTicket(pTicket, pcbTicket);
			if (string.IsNullOrEmpty(text))
			{
				if (errorCallback != null)
				{
					string errorMessage = "Failed to convert Steam ticket and so authentication cannot be attempted.";
					errorCallback(WebRequestError.GenerateLocal(errorMessage));
				}
			}
			else
			{
				RequestSteamAuthentication(text, hasUserAcceptedTerms, successCallback, errorCallback);
			}
		}

		public static void RequestSteamAuthentication(string base64EncodedTicket, bool hasUserAcceptedTerms, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			if (string.IsNullOrEmpty(base64EncodedTicket))
			{
				Debug.LogWarning("[mod.io] Encoded Steam Ticket is invalid. Ensure that the base64EncodedTicket is not null or empty.");
				if (errorCallback != null)
				{
					errorCallback(WebRequestError.GenerateLocal("Encoded Steam Ticket is invalid. Ensure that the base64EncodedTicket is not null or empty."));
				}
				return;
			}
			string endpointURL = PluginSettings.API_URL + "/external/steamauth";
			UnityWebRequest webRequest = GenerateAuthenticationRequest(endpointURL, hasUserAcceptedTerms, userDisplayName, "appdata", base64EncodedTicket);
			Action<AccessTokenObject> successCallback2 = delegate(AccessTokenObject result)
			{
				successCallback(result.access_token);
			};
			SendRequest(webRequest, successCallback2, errorCallback);
		}

		public static void RequestGOGAuthentication(byte[] data, uint dataSize, bool hasUserAcceptedTerms, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			if (data == null || data.Length == 0 || data.Length > 1024)
			{
				Debug.LogWarning("[mod.io] GOG Ticket is invalid. Ensure that the data is not null, and is less than 1024 bytes.");
				if (errorCallback != null)
				{
					errorCallback(WebRequestError.GenerateLocal("GOG Ticket is invalid. Ensure that the data is not null, and is less than 1024 bytes."));
				}
				return;
			}
			string text = Utility.EncodeEncryptedAppTicket(data, dataSize);
			if (string.IsNullOrEmpty(text))
			{
				if (errorCallback != null)
				{
					string errorMessage = "Failed to convert GOG ticket and so authentication cannot be attempted.";
					errorCallback(WebRequestError.GenerateLocal(errorMessage));
				}
			}
			else
			{
				RequestGOGAuthentication(text, hasUserAcceptedTerms, successCallback, errorCallback);
			}
		}

		public static void RequestGOGAuthentication(string base64EncodedTicket, bool hasUserAcceptedTerms, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			if (string.IsNullOrEmpty(base64EncodedTicket))
			{
				Debug.LogWarning("[mod.io] Encoded GOG Galaxy Ticket is invalid. Ensure that the base64EncodedTicket is not null or empty.");
				if (errorCallback != null)
				{
					errorCallback(WebRequestError.GenerateLocal("Encoded GOG Galaxy Ticket is invalid. Ensure that the base64EncodedTicket is not null or empty."));
				}
				return;
			}
			string endpointURL = PluginSettings.API_URL + "/external/galaxyauth";
			UnityWebRequest webRequest = GenerateAuthenticationRequest(endpointURL, hasUserAcceptedTerms, userDisplayName, "appdata", base64EncodedTicket);
			Action<AccessTokenObject> successCallback2 = delegate(AccessTokenObject result)
			{
				successCallback(result.access_token);
			};
			SendRequest(webRequest, successCallback2, errorCallback);
		}

		public static void RequestItchIOAuthentication(string jwtToken, bool hasUserAcceptedTerms, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			if (string.IsNullOrEmpty(jwtToken))
			{
				Debug.LogWarning("[mod.io] itch.io JWT Token is invalid. Ensure that the jwtToken is not null or empty.");
				if (errorCallback != null)
				{
					errorCallback(WebRequestError.GenerateLocal("itch.io JWT Token is invalid. Ensure that the jwtToken is not null or empty."));
				}
				return;
			}
			string endpointURL = PluginSettings.API_URL + "/external/itchioauth";
			UnityWebRequest webRequest = GenerateAuthenticationRequest(endpointURL, hasUserAcceptedTerms, userDisplayName, "itchio_token", jwtToken);
			Action<AccessTokenObject> successCallback2 = delegate(AccessTokenObject result)
			{
				successCallback(result.access_token);
			};
			SendRequest(webRequest, successCallback2, errorCallback);
		}

		public static void RequestOculusRiftAuthentication(string oculusUserNonce, int oculusUserId, string oculusUserAccessToken, bool hasUserAcceptedTerms, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			if (string.IsNullOrEmpty(oculusUserNonce))
			{
				Debug.LogWarning("[mod.io] Oculus Rift user nonce is invalid. Ensure that the oculusUserNonce is not null or empty.");
				if (errorCallback != null)
				{
					errorCallback(WebRequestError.GenerateLocal("Oculus Rift user nonce is invalid. Ensure that the oculusUserNonce is not null or empty."));
				}
				return;
			}
			if (string.IsNullOrEmpty(oculusUserAccessToken))
			{
				Debug.LogWarning("[mod.io] Oculus Rift user access token is invalid. Ensure that the oculusUserAccessToken is not null or empty.");
				if (errorCallback != null)
				{
					errorCallback(WebRequestError.GenerateLocal("Oculus Rift user access token is invalid. Ensure that the oculusUserAccessToken is not null or empty."));
				}
				return;
			}
			string endpointURL = PluginSettings.API_URL + "/external/oculusauth";
			KeyValuePair<string, string>[] authData = new KeyValuePair<string, string>[3]
			{
				new KeyValuePair<string, string>("nonce", oculusUserNonce),
				new KeyValuePair<string, string>("user_id", oculusUserId.ToString()),
				new KeyValuePair<string, string>("access_token", oculusUserAccessToken)
			};
			UnityWebRequest webRequest = GenerateAuthenticationRequest(endpointURL, hasUserAcceptedTerms, userDisplayName, authData);
			Action<AccessTokenObject> successCallback2 = delegate(AccessTokenObject result)
			{
				successCallback(result.access_token);
			};
			SendRequest(webRequest, successCallback2, errorCallback);
		}

		public static void RequestXboxLiveAuthentication(string xboxLiveUserToken, bool hasUserAcceptedTerms, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			if (string.IsNullOrEmpty(xboxLiveUserToken))
			{
				Debug.LogWarning("[mod.io] Xbox Live token is invalid. Ensure that the xboxLiveUserToken is not null or empty.");
				if (errorCallback != null)
				{
					errorCallback(WebRequestError.GenerateLocal("Xbox Live token is invalid. Ensure that the xboxLiveUserToken is not null or empty."));
				}
			}
			string endpointURL = PluginSettings.API_URL + "/external/xboxauth";
			UnityWebRequest webRequest = GenerateAuthenticationRequest(endpointURL, hasUserAcceptedTerms, userDisplayName, "xbox_token", xboxLiveUserToken);
			Action<AccessTokenObject> successCallback2 = delegate(AccessTokenObject result)
			{
				successCallback(result.access_token);
			};
			SendRequest(webRequest, successCallback2, errorCallback);
		}

		public static void GetAllGames(RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<GameProfile>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetGame(Action<GameProfile> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID;
			UnityWebRequest webRequest = GenerateQuery(endpointURL, string.Empty, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void EditGame(EditGameParameters parameters, Action<GameProfile> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID;
			UnityWebRequest webRequest = GeneratePutRequest(endpointURL, parameters.stringValues.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static string BuildGetModEndpointURL(int gameId, int modId)
		{
			return "/games/" + gameId + "/mods/" + modId;
		}

		public static string BuildGetAllModsEndpointURL(int gameId, string filterString, APIPaginationParameters pagination)
		{
			string baseURL = "/games/" + gameId + "/mods";
			return BuildEndpointURL(baseURL, filterString, pagination);
		}

		public static void GetAllMods(RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<ModProfile>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, filter.GenerateFilterString(), pagination);
			Action<RequestPage<ModProfile>> successCallback2 = delegate(RequestPage<ModProfile> r)
			{
				RequestCache.StoreMods(PluginSettings.GAME_ID, r.items);
				if (successCallback != null)
				{
					successCallback(r);
				}
			};
			SendRequest(webRequest, successCallback2, errorCallback);
		}

		public static void GetMod(int modId, Action<ModProfile> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + BuildGetModEndpointURL(PluginSettings.GAME_ID, modId);
			UnityWebRequest webRequest = GenerateQuery(endpointURL, string.Empty, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void AddMod(AddModParameters parameters, Action<ModProfile> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods";
			UnityWebRequest webRequest = GeneratePostRequest(endpointURL, parameters.stringValues.ToArray(), parameters.binaryData.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void EditMod(int modId, EditModParameters parameters, Action<ModProfile> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId;
			UnityWebRequest webRequest = GeneratePutRequest(endpointURL, parameters.stringValues.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void DeleteMod(int modId, Action successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId;
			UnityWebRequest webRequest = GenerateDeleteRequest(endpointURL, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetAllModfiles(int modId, RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<Modfile>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/files";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetModfile(int modId, int modfileId, Action<Modfile> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/files/" + modfileId;
			UnityWebRequest webRequest = GenerateQuery(endpointURL, string.Empty, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void AddModfile(int modId, AddModfileParameters parameters, Action<Modfile> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/files";
			UnityWebRequest webRequest = GeneratePostRequest(endpointURL, parameters.stringValues.ToArray(), parameters.binaryData.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void EditModfile(int modId, int modfileId, EditModfileParameters parameters, Action<Modfile> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/files/" + modfileId;
			UnityWebRequest webRequest = GeneratePutRequest(endpointURL, parameters.stringValues.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void AddGameMedia(AddGameMediaParameters parameters, Action<APIMessage> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/media";
			UnityWebRequest webRequest = GeneratePostRequest(endpointURL, parameters.stringValues.ToArray(), parameters.binaryData.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void AddModMedia(int modId, AddModMediaParameters parameters, Action<APIMessage> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/media";
			UnityWebRequest webRequest = GeneratePostRequest(endpointURL, parameters.stringValues.ToArray(), parameters.binaryData.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void DeleteModMedia(int modId, DeleteModMediaParameters parameters, Action successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/media";
			UnityWebRequest webRequest = GenerateDeleteRequest(endpointURL, parameters.stringValues.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void SubscribeToMod(int modId, Action<ModProfile> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/subscribe";
			UnityWebRequest webRequest = GeneratePostRequest(endpointURL, null, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void UnsubscribeFromMod(int modId, Action successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/subscribe";
			UnityWebRequest webRequest = GenerateDeleteRequest(endpointURL, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetModEvents(int modId, RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<ModEvent>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/events";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetAllModEvents(RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<ModEvent>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/events";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetAllModStats(RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<ModStatistics>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/stats";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetModStats(int modId, Action<ModStatistics> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/stats";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, string.Empty, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetGameTagOptions(Action<RequestPage<ModTagCategory>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/tags";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, string.Empty, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void AddGameTagOption(AddGameTagOptionParameters parameters, Action<APIMessage> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/tags";
			UnityWebRequest webRequest = GeneratePostRequest(endpointURL, parameters.stringValues.ToArray(), parameters.binaryData.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void DeleteGameTagOption(DeleteGameTagOptionParameters parameters, Action successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/tags";
			UnityWebRequest webRequest = GenerateDeleteRequest(endpointURL, parameters.stringValues.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetModTags(int modId, RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<ModTag>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/tags";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void AddModTags(int modId, AddModTagsParameters parameters, Action<APIMessage> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/tags";
			UnityWebRequest webRequest = GeneratePostRequest(endpointURL, parameters.stringValues.ToArray(), parameters.binaryData.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void DeleteModTags(int modId, DeleteModTagsParameters parameters, Action successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/tags";
			UnityWebRequest webRequest = GenerateDeleteRequest(endpointURL, parameters.stringValues.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void AddModRating(int modId, AddModRatingParameters parameters, Action<APIMessage> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/ratings";
			UnityWebRequest webRequest = GeneratePostRequest(endpointURL, parameters.stringValues.ToArray(), parameters.binaryData.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetAllModKVPMetadata(int modId, APIPaginationParameters pagination, Action<RequestPage<MetadataKVP>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/metadatakvp";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, string.Empty, pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void AddModKVPMetadata(int modId, AddModKVPMetadataParameters parameters, Action<APIMessage> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/metadatakvp";
			UnityWebRequest webRequest = GeneratePostRequest(endpointURL, parameters.stringValues.ToArray(), parameters.binaryData.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void DeleteModKVPMetadata(int modId, DeleteModKVPMetadataParameters parameters, Action successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/metadatakvp";
			UnityWebRequest webRequest = GenerateDeleteRequest(endpointURL, parameters.stringValues.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetAllModDependencies(int modId, RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<ModDependency>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/dependencies";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void AddModDependencies(int modId, AddModDependenciesParameters parameters, Action<APIMessage> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/dependencies";
			UnityWebRequest webRequest = GeneratePostRequest(endpointURL, parameters.stringValues.ToArray(), parameters.binaryData.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void DeleteModDependencies(int modId, DeleteModDependenciesParameters parameters, Action successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/dependencies";
			UnityWebRequest webRequest = GenerateDeleteRequest(endpointURL, parameters.stringValues.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetAllModTeamMembers(int modId, RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<ModTeamMember>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/team";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void AddModTeamMember(int modId, AddModTeamMemberParameters parameters, Action<APIMessage> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/team";
			UnityWebRequest webRequest = GeneratePostRequest(endpointURL, parameters.stringValues.ToArray(), parameters.binaryData.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void UpdateModTeamMember(int modId, int teamMemberId, UpdateModTeamMemberParameters parameters, Action<APIMessage> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/team/" + teamMemberId;
			UnityWebRequest webRequest = GeneratePutRequest(endpointURL, parameters.stringValues.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void DeleteModTeamMember(int modId, int teamMemberId, Action successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/team/" + teamMemberId;
			UnityWebRequest webRequest = GenerateDeleteRequest(endpointURL, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetAllModComments(int modId, RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<ModComment>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/comments";
			UnityWebRequest webRequest = GenerateQuery(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetModComment(int modId, int commentId, Action<ModComment> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/comments/" + commentId;
			UnityWebRequest webRequest = GenerateQuery(endpointURL, string.Empty, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void DeleteModComment(int modId, int commentId, Action successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/games/" + PluginSettings.GAME_ID + "/mods/" + modId + "/comments/" + commentId;
			UnityWebRequest webRequest = GenerateDeleteRequest(endpointURL, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetResourceOwner(APIResourceType resourceType, int resourceID, Action<UserProfile> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/general/owner";
			StringValueParameter[] valueFields = new StringValueParameter[2]
			{
				StringValueParameter.Create("resource_type", resourceType.ToString().ToLower()),
				StringValueParameter.Create("resource_id", resourceID)
			};
			UnityWebRequest webRequest = GeneratePostRequest(endpointURL, valueFields, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void SubmitReport(SubmitReportParameters parameters, Action<APIMessage> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/report";
			UnityWebRequest webRequest = GeneratePostRequest(endpointURL, parameters.stringValues.ToArray(), parameters.binaryData.ToArray());
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetAuthenticatedUser(Action<UserProfile> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/me";
			UnityWebRequest webRequest = GenerateGetRequest(endpointURL, string.Empty, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetUserSubscriptions(RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<ModProfile>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/me/subscribed";
			UnityWebRequest webRequest = GenerateGetRequest(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetUserEvents(RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<UserEvent>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/me/events";
			UnityWebRequest webRequest = GenerateGetRequest(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetUserGames(Action<RequestPage<GameProfile>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/me/games";
			UnityWebRequest webRequest = GenerateGetRequest(endpointURL, string.Empty, null);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetUserMods(RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<ModProfile>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/me/mods";
			UnityWebRequest webRequest = GenerateGetRequest(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetUserModfiles(RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<Modfile>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/me/files";
			UnityWebRequest webRequest = GenerateGetRequest(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		public static void GetUserRatings(RequestFilter filter, APIPaginationParameters pagination, Action<RequestPage<ModRating>> successCallback, Action<WebRequestError> errorCallback)
		{
			string endpointURL = PluginSettings.API_URL + "/me/ratings";
			UnityWebRequest webRequest = GenerateGetRequest(endpointURL, filter.GenerateFilterString(), pagination);
			SendRequest(webRequest, successCallback, errorCallback);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static UnityWebRequest GenerateAuthenticationRequest(string endpointURL, string authenticationKey, string authenticationValue)
		{
			return GenerateAuthenticationRequest(endpointURL, false, userDisplayName, authenticationKey, authenticationValue);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static UnityWebRequest GenerateAuthenticationRequest(string endpointURL, params KeyValuePair<string, string>[] authData)
		{
			return GenerateAuthenticationRequest(endpointURL, false, userDisplayName, authData);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void RequestSteamAuthentication(byte[] pTicket, uint pcbTicket, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			RequestSteamAuthentication(pTicket, pcbTicket, false, successCallback, errorCallback);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void RequestSteamAuthentication(string base64EncodedTicket, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			RequestSteamAuthentication(base64EncodedTicket, false, successCallback, errorCallback);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void RequestGOGAuthentication(byte[] data, uint dataSize, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			RequestGOGAuthentication(data, dataSize, false, successCallback, errorCallback);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void RequestGOGAuthentication(string base64EncodedTicket, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			RequestGOGAuthentication(base64EncodedTicket, false, successCallback, errorCallback);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void RequestItchIOAuthentication(string jwtToken, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			RequestItchIOAuthentication(jwtToken, false, successCallback, errorCallback);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void RequestOculusRiftAuthentication(string oculusUserNonce, int oculusUserId, string oculusUserAccessToken, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			RequestOculusRiftAuthentication(oculusUserNonce, oculusUserId, oculusUserAccessToken, false, successCallback, errorCallback);
		}

		[Obsolete("Now requires the hasUserAcceptedTerms flag to be provided.")]
		public static void RequestXboxLiveAuthentication(string xboxLiveUserToken, Action<string> successCallback, Action<WebRequestError> errorCallback)
		{
			RequestXboxLiveAuthentication(xboxLiveUserToken, false, successCallback, errorCallback);
		}

		[Obsolete("No longer requires an ExternalAuthenticationProvider parameter.")]
		public static void GetTermsOfUse(ExternalAuthenticationProvider authProvider, Action<TermsOfUseInfo> successCallback, Action<WebRequestError> errorCallback)
		{
			GetTermsOfUse(successCallback, errorCallback);
		}
	}
}
