using System;
using System.Threading.Tasks;
using Landfall.TABS;

namespace TFBGames
{
	public class PermissionsHelper : IService
	{
		public const string ALLOW_UGC_SETTINGS_KEY = "ALLOW_UGC";

		private const string NoInternetError = "NETWORK_ERROR_UNKNOWN";

		private IAccountPermissions m_AccountPermissions;

		private ModalPanel m_ModalPanel;

		private IPlayerPrefsPlatform m_PlayerPrefs;

		private IInternetStatusService m_internetStatusService;

		public bool CanViewDownloadTabs { get; set; }

		public void CheckWorkshopPermissions(Func<bool> didCancelChecks, CheckWorkshopPermissionsCallback callback)
		{
			if (!m_AccountPermissions.IsSignedIn)
			{
				m_ModalPanel.PopUp("POPUP_NOT_SIGNED_IN_TO_VIEW", delegate
				{
					if (!DidCancelChecks())
					{
						callback?.Invoke(PermissionsHelperResult.Failed);
					}
				});
				return;
			}
			if (m_PlayerPrefs.GetInt("ALLOW_UGC") != 0)
			{
				m_ModalPanel.PopUp("POPUP_NO_OPT_IN_TO_UGC", delegate
				{
					if (!DidCancelChecks())
					{
						callback?.Invoke(PermissionsHelperResult.Failed);
					}
				});
				return;
			}
			CheckNetworkAccess(async delegate(bool success)
			{
				if (!DidCancelChecks())
				{
					if (!success)
					{
						callback?.Invoke(PermissionsHelperResult.Failed);
					}
					else if (!(await IsOnline(showPopup: true)))
					{
						callback?.Invoke(PermissionsHelperResult.Failed);
					}
					else
					{
						m_AccountPermissions.CanViewAndDownloadUgcAsync(showPopup: true, "POPUP_NOT_ALLOWED_TO_VIEW_UGC", delegate(bool permitted)
						{
							if (!DidCancelChecks())
							{
								if (!permitted)
								{
									callback?.Invoke(PermissionsHelperResult.Failed);
								}
								else
								{
									callback?.Invoke(PermissionsHelperResult.Succeeded);
								}
							}
						});
					}
				}
			});
			bool DidCancelChecks()
			{
				if (didCancelChecks != null && didCancelChecks())
				{
					callback?.Invoke(PermissionsHelperResult.Cancelled);
					return true;
				}
				return false;
			}
		}

		public void OnStart()
		{
			m_AccountPermissions = ServiceLocator.GetService<IAccountPermissions>();
			m_ModalPanel = ServiceLocator.GetService<ModalPanel>();
			m_PlayerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
			m_internetStatusService = ServiceLocator.GetService<IInternetStatusService>();
		}

		public async Task<bool> IsOnline(bool showPopup)
		{
			if (m_internetStatusService == null)
			{
				return true;
			}
			bool num = await m_internetStatusService.IsConnected(connectIfNotConnected: true);
			if (!num && showPopup)
			{
				ServiceLocator.GetService<ModalPanel>().PopUp("NETWORK_ERROR_UNKNOWN", delegate
				{
				});
			}
			return num;
		}

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

		public void UnRegister()
		{
		}

		private void CheckNetworkAccess(Action<bool> callback)
		{
			callback?.Invoke(obj: true);
		}
	}
}
