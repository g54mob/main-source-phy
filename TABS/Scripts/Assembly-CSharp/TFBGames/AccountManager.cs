using System;
using System.Diagnostics;
using BitCode.Users;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TFBGames
{
	public class AccountManager : ServicePrefab
	{
		private const string ChangeAccountScene = "XboxOneChangeUser";

		private const string GamePassNoUserWarningMessage = "NO_USER_PROFILE_SELECTED";

		private bool _selectedActiveAccount = true;

		private FileIOWrapper _fileIO;

		private IAccountMonitor _monitor;

		private bool _isChangingAccount;

		private ILocalAccount _pendingAccount;

		private bool sentSteamInitialized;

		public ILocalAccountManager LocalAccountManager { get; private set; }

		public bool SelectedActiveAccount
		{
			get
			{
				return _selectedActiveAccount;
			}
			set
			{
				_selectedActiveAccount = value;
			}
		}

		public ILocalAccount ActiveAccount { get; private set; }

		public event Action<ILocalAccount> PreActiveAccountChange;

		public event Action<ILocalAccount> ActiveAccountChanged;

		private event Action<ILocalAccount> _accountSelectedCallbacks;

		public void FireWhenAccountIsSelected(Action<ILocalAccount> callback)
		{
			if (callback != null)
			{
				if (SelectedActiveAccount)
				{
					callback(ActiveAccount);
				}
				else
				{
					_accountSelectedCallbacks += callback;
				}
			}
		}

		public void OnSelectedActiveAccount()
		{
			SelectedActiveAccount = true;
			_monitor?.OnSelectedActiveAccount(ActiveAccount);
			this._accountSelectedCallbacks?.Invoke(ActiveAccount);
			this._accountSelectedCallbacks = null;
		}

		public override void OnRegister()
		{
			base.OnRegister();
			IPlatformManager service = ServiceLocator.GetService<IPlatformManager>();
			if (service != null && service.Services != null)
			{
				ILocalAccountManager localAccountManager = (LocalAccountManager = service.Services.LocalAccountManager);
				localAccountManager.AccountAdded += AccountManagerOnAccountAdded;
				localAccountManager.AccountLeft += AccountManagerOnAccountLeft;
				localAccountManager.AccountSignInFailed += AccountManagerOnAccountSignInFailed;
			}
		}

		public override void OnAwake()
		{
			base.OnAwake();
			_fileIO = ServiceLocator.GetService<FileIOWrapper>();
			_monitor = ServiceLocator.GetService<IAccountMonitor>();
			if (_monitor != null)
			{
				ActiveAccount = _monitor.InitialAccount;
				_monitor.AccountChanged += OnAccountChanged;
			}
			else
			{
				ActiveAccount = LocalAccountManager?.GetPrimaryAccount();
			}
		}

		public override void UnRegister()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
			if (_monitor != null)
			{
				_monitor.AccountChanged -= OnAccountChanged;
			}
			this.PreActiveAccountChange = null;
			this.ActiveAccountChanged = null;
			if (LocalAccountManager is IDisposable disposable)
			{
				disposable.Dispose();
			}
			base.UnRegister();
		}

		private void OnAccountChanged(ILocalAccount account)
		{
			if (!SelectedActiveAccount)
			{
				ActiveAccount = account;
				return;
			}
			_pendingAccount = account;
			if (!_isChangingAccount)
			{
				_isChangingAccount = true;
				ServiceLocator.GetService<IUserChangedUI>().Show(_pendingAccount);
				_fileIO.WaitForAsyncsToFinish(delegate
				{
					SceneManager.sceneLoaded += OnSceneLoaded;
					TABSSceneManager.LoadScene("XboxOneChangeUser", forceInstantLoad: true);
				});
			}
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (scene.name != "XboxOneChangeUser")
			{
				return;
			}
			SceneManager.sceneLoaded -= OnSceneLoaded;
			_isChangingAccount = false;
			try
			{
				this.PreActiveAccountChange?.Invoke(_pendingAccount);
			}
			catch (Exception)
			{
				throw;
			}
			ActiveAccount = _pendingAccount;
			_pendingAccount = null;
			try
			{
				this.ActiveAccountChanged?.Invoke(ActiveAccount);
			}
			catch (Exception)
			{
				throw;
			}
		}

		[Conditional("ACCOUNT_MANAGER_WAIT_FOR_USER")]
		private void CheckIfActiveAccountIsSelected(bool allLoginAttemptsFailed)
		{
			if (!SelectedActiveAccount)
			{
				ActiveAccount = LocalAccountManager?.GetPrimaryAccount();
				if (ActiveAccount != null || allLoginAttemptsFailed)
				{
					OnSelectedActiveAccount();
				}
			}
		}

		private void AccountManagerOnAccountAdded(ILocalAccount account)
		{
		}

		private void AccountManagerOnAccountLeft(ILocalAccount account)
		{
		}

		private void AccountManagerOnAccountSignInFailed(Exception exception)
		{
		}

		public void ShowSignInPrompt(SignInPromptOptions options = SignInPromptOptions.Default)
		{
			try
			{
				LocalAccountManager.PromptSignIn(options);
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}

		public string GetUserId()
		{
			ServiceLocator.GetService<AccountManager>();
			return string.Empty;
		}

		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		private static void LogInfo(string message, params object[] args)
		{
			message = $"[AccountManager] {message}";
			UnityEngine.Debug.LogFormat(message, args);
		}

		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		private static void LogError(string message, params object[] args)
		{
			message = $"[AccountManager] {message}";
			UnityEngine.Debug.LogErrorFormat(message, args);
		}
	}
}
