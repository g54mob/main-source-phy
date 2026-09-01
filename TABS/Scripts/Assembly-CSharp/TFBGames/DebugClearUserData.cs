using UnityEngine;

namespace TFBGames
{
	public class DebugClearUserData : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Mark this game object as DontDestroyOnLoad. Only works if it has no parent.")]
		protected bool dontDestroyOnLoad;

		[SerializeField]
		[Tooltip("Containers to delete on Xbox.")]
		private string[] xboxContainerNames = new string[3] { "SavesLoaderContainer", "Preferences", "Files" };

		private bool isClearingData;

		private int asyncCounter;

		private bool loggedEmptyContainerNames;

		private AccountManager accountManager;

		private IPlayerPrefsPlatform playerPrefs;

		private void Awake()
		{
			LogInfo("Destroying myself because we're not in development...");
			Object.Destroy(this);
		}

		private void Start()
		{
			accountManager = ServiceLocator.GetService<AccountManager>();
			playerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
		}

		public bool IsPlatformSupported()
		{
			return false;
		}

		public void ClearActiveUserData()
		{
		}

		private void Init()
		{
		}

		private void OnAsyncDone()
		{
			asyncCounter--;
			if (asyncCounter <= 0)
			{
				OnClearUserDataDone();
			}
		}

		private void OnClearUserDataDone()
		{
			isClearingData = false;
			asyncCounter = 0;
		}

		private static void LogInfo(string message, params object[] args)
		{
			message = $"[DebugClearUserData] {message}";
			Debug.LogFormat(message, args);
		}
	}
}
