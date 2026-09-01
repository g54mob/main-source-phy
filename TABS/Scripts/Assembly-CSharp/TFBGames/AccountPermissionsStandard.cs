using System;
using BitCode.Users;
using Landfall.TABS;
using UnityEngine;

namespace TFBGames
{
	public class AccountPermissionsStandard : IAccountPermissions, IService
	{
		private AccountManager accountManager;

		private ILocalAccount account;

		public bool IsSignedIn => account != null;

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void OnStart()
		{
			accountManager = ServiceLocator.GetService<AccountManager>();
			accountManager.ActiveAccountChanged += OnActiveAccountChanged;
			accountManager.FireWhenAccountIsSelected(OnAccountIsSelected);
		}

		public void UnRegister()
		{
			if (accountManager != null)
			{
				accountManager.ActiveAccountChanged -= OnActiveAccountChanged;
			}
		}

		public void CanUploadUgcAsync(bool showPopup, string popupMessage, Action<bool> doneCallback)
		{
			OnGotPermission(account != null && account.PermittedToCreateUgc, showPopup, popupMessage, doneCallback);
		}

		public void CanViewAndDownloadUgcAsync(bool showPopup, string popupMessage, Action<bool> doneCallback)
		{
			OnGotPermission(account != null && account.PermittedToViewUgc, showPopup, popupMessage, doneCallback);
		}

		public void CanPlayInAMultiplayerSessionAsync(bool showPopup, string popupMessage, Action<bool> doneCallback)
		{
			if (account == null)
			{
				OnGotPermission(hasPermission: false, showPopup, popupMessage, doneCallback);
				return;
			}
			account.CheckMultiplayerPermissionsAsync(MultiplayerMode.Online, delegate(ILocalAccount user, bool permitted)
			{
				OnGotPermission(permitted, showPopup, popupMessage, doneCallback);
			});
		}

		public void CanPlayCrossNetworkSessionAsync(Action<bool> doneCallback)
		{
			if (account == null)
			{
				OnGotPermission(hasPermission: false, showPopup: false, null, doneCallback);
				return;
			}
			try
			{
				account.CheckCrossNetworkPlayAsync(MultiplayerMode.Online, delegate(ILocalAccount user, bool permitted)
				{
					OnGotPermission(permitted, showPopup: false, null, doneCallback);
				});
			}
			catch (NotImplementedException)
			{
				Debug.LogError("CheckCrossNetworkPlayAsync has not been implemented in BitCode for the current platform.");
				OnGotPermission(hasPermission: false, showPopup: false, null, doneCallback);
			}
		}

		private void OnAccountIsSelected(ILocalAccount initialAccount)
		{
			account = initialAccount;
		}

		private void OnActiveAccountChanged(ILocalAccount newAccount)
		{
			account = newAccount;
		}

		private void OnGotPermission(bool hasPermission, bool showPopup, string popupMessage, Action<bool> doneCallback)
		{
			if (hasPermission)
			{
				doneCallback?.Invoke(obj: true);
				return;
			}
			if (!showPopup)
			{
				doneCallback?.Invoke(obj: false);
				return;
			}
			ServiceLocator.GetService<ModalPanel>().PopUp(popupMessage, delegate
			{
				doneCallback?.Invoke(obj: false);
			});
		}
	}
}
