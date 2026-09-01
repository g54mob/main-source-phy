using System;
using System.Collections.Generic;
using BitCode.Users;

namespace TFBGames
{
	public class DlcCheckerService : IService
	{
		private const int CheckDlcMaxDelay = 5;

		private AccountManager accountManager;

		private IDlcManagerService dlcManager;

		private bool busyCheckingDlc;

		private List<string> allDlcIds = new List<string>();

		private readonly Queue<string> checkDlcIds = new Queue<string>();

		private int checkDlcDelay;

		public void OnRegister()
		{
		}

		public void OnAwake()
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
			dlcManager = ServiceLocator.GetService<IDlcManagerService>();
			if (dlcManager != null)
			{
				dlcManager.PreGotAccessToDlc += OnPreGotAccessToDlc;
				dlcManager.PreLostAccessToAllDlc += OnPreLostAccessToAllDlc;
				accountManager = ServiceLocator.GetService<AccountManager>();
				accountManager.ActiveAccountChanged += OnActiveAccountChanged;
				if (accountManager.LocalAccountManager != null)
				{
					accountManager.LocalAccountManager.AccountAdded += OnAccountAdded;
				}
				allDlcIds.Add(dlcManager.AprilFoolsBugsDlcId);
				ServiceLocator.GetService<WaitForStorage>().FireWhenReady(OnStorageReady);
			}
		}

		public void UnRegister()
		{
			if (dlcManager != null)
			{
				dlcManager.PreGotAccessToDlc -= OnPreGotAccessToDlc;
				dlcManager.PreLostAccessToAllDlc -= OnPreLostAccessToAllDlc;
			}
			if (accountManager != null)
			{
				accountManager.ActiveAccountChanged -= OnActiveAccountChanged;
				if (accountManager.LocalAccountManager != null)
				{
					accountManager.LocalAccountManager.AccountAdded -= OnAccountAdded;
				}
			}
		}

		public void OnUpdate()
		{
			if (checkDlcIds.Count > 0 && !busyCheckingDlc && checkDlcDelay-- <= 0)
			{
				busyCheckingDlc = true;
				string dlcId = checkDlcIds.Dequeue();
				dlcManager.HasAccessToDlc(dlcId, delegate(bool hasAccess, Exception exception)
				{
					busyCheckingDlc = false;
					SetDlcAccess(dlcId, hasAccess);
				});
			}
		}

		private void CheckUsersDlc()
		{
			if (!dlcManager.NeedsUserForDlc || (!(accountManager == null) && accountManager.ActiveAccount != null))
			{
				checkDlcDelay = 5;
				checkDlcIds.Clear();
				int i = 0;
				for (int count = allDlcIds.Count; i < count; i++)
				{
					checkDlcIds.Enqueue(allDlcIds[i]);
				}
			}
		}

		private void RemoveAccessToAllDlc()
		{
			checkDlcIds.Clear();
			int i = 0;
			for (int count = allDlcIds.Count; i < count; i++)
			{
				SetDlcAccess(allDlcIds[i], hasAccess: false);
			}
		}

		private void SetDlcAccess(string dlcId, bool hasAccess)
		{
			if (!string.IsNullOrEmpty(dlcId) && dlcId.Equals(dlcManager.AprilFoolsBugsDlcId, StringComparison.InvariantCultureIgnoreCase))
			{
				Bugs._DLC_ACTIVATED = hasAccess;
			}
		}

		private void OnActiveAccountChanged(ILocalAccount account)
		{
			CheckUsersDlc();
		}

		private void OnAccountAdded(ILocalAccount account)
		{
			if (account != accountManager.ActiveAccount)
			{
				CheckUsersDlc();
			}
		}

		private void OnStorageReady()
		{
			CheckUsersDlc();
		}

		private void OnPreGotAccessToDlc(string dlcId)
		{
			SetDlcAccess(dlcId, hasAccess: true);
		}

		private void OnPreLostAccessToAllDlc()
		{
			RemoveAccessToAllDlc();
			CheckUsersDlc();
		}
	}
}
