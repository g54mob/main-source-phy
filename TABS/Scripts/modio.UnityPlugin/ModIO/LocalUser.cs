using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public struct LocalUser
	{
		public static readonly string FILENAME;

		private static LocalUser _instance;

		public static bool isLoaded;

		public UserProfile profile;

		public string oAuthToken;

		public bool wasTokenRejected;

		public List<int> enabledModIds;

		public List<int> subscribedModIds;

		public List<int> queuedSubscribes;

		public List<int> queuedUnsubscribes;

		[JsonIgnore]
		public ExternalAuthenticationData externalAuthentication;

		public static LocalUser instance
		{
			get
			{
				return _instance;
			}
			set
			{
				_instance = value;
				AssertListsNotNull(ref _instance);
			}
		}

		[JsonIgnore]
		public AuthenticationState authenticationState
		{
			get
			{
				if (string.IsNullOrEmpty(oAuthToken))
				{
					return AuthenticationState.NoToken;
				}
				if (wasTokenRejected)
				{
					return AuthenticationState.RejectedToken;
				}
				return AuthenticationState.ValidToken;
			}
		}

		public static int UserId
		{
			get
			{
				if (_instance.profile == null)
				{
					return -1;
				}
				return _instance.profile.id;
			}
		}

		public static UserProfile Profile
		{
			get
			{
				return _instance.profile;
			}
			set
			{
				_instance.profile = value;
			}
		}

		public static string OAuthToken
		{
			get
			{
				return _instance.oAuthToken;
			}
			set
			{
				_instance.oAuthToken = value;
			}
		}

		public static bool WasTokenRejected
		{
			get
			{
				return _instance.wasTokenRejected;
			}
			set
			{
				_instance.wasTokenRejected = value;
			}
		}

		public static List<int> EnabledModIds
		{
			get
			{
				return _instance.enabledModIds;
			}
			set
			{
				if (value == null)
				{
					value = new List<int>();
				}
				_instance.enabledModIds = value;
			}
		}

		public static List<int> SubscribedModIds
		{
			get
			{
				return _instance.subscribedModIds;
			}
			set
			{
				if (value == null)
				{
					value = new List<int>();
				}
				_instance.subscribedModIds = value;
			}
		}

		public static List<int> QueuedSubscribes
		{
			get
			{
				return _instance.queuedSubscribes;
			}
			set
			{
				if (value == null)
				{
					value = new List<int>();
				}
				_instance.queuedSubscribes = value;
			}
		}

		public static List<int> QueuedUnsubscribes
		{
			get
			{
				return _instance.queuedUnsubscribes;
			}
			set
			{
				if (value == null)
				{
					value = new List<int>();
				}
				_instance.queuedUnsubscribes = value;
			}
		}

		public static ExternalAuthenticationData ExternalAuthentication
		{
			get
			{
				return _instance.externalAuthentication;
			}
			set
			{
				_instance.externalAuthentication = value;
			}
		}

		public static AuthenticationState AuthenticationState => _instance.authenticationState;

		static LocalUser()
		{
			FILENAME = "user.data";
			_instance = default(LocalUser);
			AssertListsNotNull(ref _instance);
			isLoaded = false;
		}

		public static void Load(Action callback = null)
		{
			isLoaded = false;
			UserDataStorage.ReadJSONFile(FILENAME, delegate(string path, bool success, LocalUser fileData)
			{
				AssertListsNotNull(ref fileData);
				_instance = fileData;
				isLoaded = success;
				if (callback != null)
				{
					callback();
				}
			});
		}

		public static void Save(Action callback = null)
		{
			UserDataStorage.WriteJSONFile(FILENAME, _instance, delegate
			{
				if (callback != null)
				{
					callback();
				}
			});
		}

		public static void AssertListsNotNull(ref LocalUser userData)
		{
			if (userData.enabledModIds == null || userData.subscribedModIds == null || userData.queuedSubscribes == null || userData.queuedUnsubscribes == null)
			{
				if (userData.enabledModIds == null)
				{
					userData.enabledModIds = new List<int>();
				}
				if (userData.subscribedModIds == null)
				{
					userData.subscribedModIds = new List<int>();
				}
				if (userData.queuedSubscribes == null)
				{
					userData.queuedSubscribes = new List<int>();
				}
				if (userData.queuedUnsubscribes == null)
				{
					userData.queuedUnsubscribes = new List<int>();
				}
			}
		}
	}
}
