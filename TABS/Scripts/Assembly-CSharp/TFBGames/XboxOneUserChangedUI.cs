using BitCode.Users;
using TMPro;
using UnityEngine;

namespace TFBGames
{
	public class XboxOneUserChangedUI : MonoBehaviour, IUserChangedUI, IService
	{
		private const string NoUserSignedInMessage = "User signed out.\nProgress will not be saved.\n\nLoading the main menu...";

		private const string NewUserSignedInMessage = "A new user signed in:\n{0}\n\nLoading the main menu...";

		private const float LoadNextSceneDelay = 5f;

		[SerializeField]
		protected TextMeshProUGUI message;

		private AccountManager accountManager;

		private FileIOWrapper fileIO;

		private IPlatformUtils platformUtils;

		private WaitForStorage waitForStorage;

		private float loadNextSceneTime;

		private bool loadNextScene;

		private bool didShowOrHide;

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnStart()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
		}

		public void OnUpdate()
		{
		}

		private void Awake()
		{
			Object.DontDestroyOnLoad(base.gameObject);
			if (!didShowOrHide)
			{
				base.gameObject.SetActive(value: false);
			}
		}

		private void OnDisable()
		{
			if (accountManager != null)
			{
				accountManager.ActiveAccountChanged -= OnActiveAccountChanged;
			}
		}

		private void Update()
		{
			if (loadNextScene && !(loadNextSceneTime >= Time.realtimeSinceStartup) && !platformUtils.IsUIOpenOrLostFocus)
			{
				loadNextScene = false;
				waitForStorage.FireWhenReady(OnStorageReady);
			}
		}

		public void Show(ILocalAccount newAccount)
		{
			didShowOrHide = true;
			fileIO = ServiceLocator.GetService<FileIOWrapper>();
			platformUtils = ServiceLocator.GetService<IPlatformUtils>();
			waitForStorage = ServiceLocator.GetService<WaitForStorage>();
			accountManager = ServiceLocator.GetService<AccountManager>();
			accountManager.ActiveAccountChanged -= OnActiveAccountChanged;
			accountManager.ActiveAccountChanged += OnActiveAccountChanged;
			if (newAccount == null)
			{
				message.text = "User signed out.\nProgress will not be saved.\n\nLoading the main menu...";
			}
			else
			{
				message.text = $"A new user signed in:\n{newAccount.Name.Value}\n\nLoading the main menu...";
			}
			base.gameObject.SetActive(value: true);
		}

		private void Hide()
		{
			didShowOrHide = true;
			base.gameObject.SetActive(value: false);
		}

		private void OnActiveAccountChanged(ILocalAccount account)
		{
			loadNextScene = true;
			loadNextSceneTime = Time.realtimeSinceStartup + 5f;
		}

		private void OnStorageReady()
		{
			fileIO.WaitForAsyncsToFinish(delegate
			{
				TABSSceneManager.LoadMainMenu();
				Hide();
			});
		}
	}
}
