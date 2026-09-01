using System;
using BitCode.Users;
using UnityEngine;

namespace TFBGames
{
	public class AccountPropertyInitializer : MonoBehaviour
	{
		private LanguageInitializationService m_languageInitializationService;

		private AccountManager m_accountManager;

		private void Start()
		{
			m_languageInitializationService = ServiceLocator.GetService<LanguageInitializationService>();
			m_accountManager = ServiceLocator.GetService<AccountManager>();
			m_accountManager.ActiveAccountChanged += OnActiveAccountChanged;
			m_accountManager.FireWhenAccountIsSelected(OnAccountIsSelected);
		}

		private void OnDestroy()
		{
			if (m_accountManager != null)
			{
				m_accountManager.ActiveAccountChanged -= OnActiveAccountChanged;
			}
		}

		private void InitializeAccount(ILocalAccount localAccount)
		{
			if (localAccount == null)
			{
				return;
			}
			if (m_languageInitializationService != null)
			{
				m_languageInitializationService.QueueLanguageInitializationCallback();
			}
			localAccount.Name.SetTracked(track: true);
			try
			{
				localAccount.AvatarImage.SetTracked(track: true);
			}
			catch (NotImplementedException)
			{
			}
		}

		private void OnAccountIsSelected(ILocalAccount initialAccount)
		{
			InitializeAccount(initialAccount);
		}

		private void OnActiveAccountChanged(ILocalAccount newAccount)
		{
			InitializeAccount(newAccount);
		}
	}
}
