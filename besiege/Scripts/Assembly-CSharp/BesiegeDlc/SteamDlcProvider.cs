using System;
using System.Collections.Generic;
using System.Linq;
using Steamworks;
using UnityEngine;

namespace BesiegeDlc
{
	internal sealed class SteamDlcProvider : DlcProviderBase
	{
		private const float PollFrequencySeconds = 1f;

		private Callback<DlcInstalled_t> dlcInstalledCallback;

		private float lastDlcPollTime;

		public SteamDlcProvider(Dictionary<DlcManager.DlcType, DlcInfo.Dlc> list, Action providerInitializedCallback, Action<DlcManager.DlcType> dlcPackageInstalled)
			: base(list, providerInitializedCallback, dlcPackageInstalled)
		{
		}

		internal override void SetUp()
		{
			RegisterDlcInstalled();
			CheckDlcInstalled(true);
			InvokeProviderInitialized();
		}

		internal override void CleanUp()
		{
			UnRegisterDlcInstalled();
		}

		internal override void OnUserSignin()
		{
		}

		internal override void OnUpdate()
		{
			if (!(lastDlcPollTime + 1f > Time.realtimeSinceStartup))
			{
				CheckDlcInstalled(false);
				lastDlcPollTime = Time.realtimeSinceStartup;
			}
		}

		private void CheckDlcInstalled(bool onlyCacheDlc)
		{
			int dLCCount = SteamApps.GetDLCCount();
			for (int i = 0; i < dLCCount; i++)
			{
				AppId_t dlcAppId;
				bool pbAvailable;
				string pchName;
				if (!SteamApps.BGetDLCDataByIndex(i, out dlcAppId, out pbAvailable, out pchName, 128))
				{
					continue;
				}
				DlcInfo.Dlc dlc = DlcInfo.Values.SingleOrDefault((DlcInfo.Dlc x) => x.SteamAppId == dlcAppId.m_AppId);
				if (dlc != null)
				{
					DlcManager.DlcType dlcType = dlc.dlcType;
					bool isUnlocked;
					TryGetUnlockState(dlcType, out isUnlocked);
					pbAvailable = HasPurchasedDlc(dlcAppId);
					SetUnlockState(dlcType, pbAvailable);
					bool flag = pbAvailable && !isUnlocked;
					if (!onlyCacheDlc && flag)
					{
						InvokePackageInstalled(dlcType);
					}
				}
			}
		}

		internal override bool IsDlcIdInstalled(string s)
		{
			uint result;
			if (uint.TryParse(s, out result))
			{
				AppId_t dlcAppId = new AppId_t(result);
				return HasPurchasedDlc(dlcAppId);
			}
			return false;
		}

		internal override bool HasPurchasedDlc(DlcManager.DlcType dlcType)
		{
			DlcInfo.Dlc dlc;
			if (!GetDlc(dlcType, out dlc))
			{
				return false;
			}
			bool isUnlocked;
			if (!TryGetUnlockState(dlcType, out isUnlocked))
			{
				AppId_t dlcAppId = new AppId_t(dlc.SteamAppId);
				isUnlocked = HasPurchasedDlc(dlcAppId);
				SetUnlockState(dlcType, isUnlocked);
			}
			return isUnlocked;
		}

		private bool HasPurchasedDlc(AppId_t dlcAppId)
		{
			return SteamApps.BIsDlcInstalled(dlcAppId);
		}

		internal override void OpenDlcStore(DlcManager.DlcType dlcType)
		{
			object obj = PlatformID(dlcType);
			if (obj != null)
			{
				AppId_t nAppID = (AppId_t)obj;
				EOverlayToStoreFlag eFlag = EOverlayToStoreFlag.k_EOverlayToStoreFlag_None;
				SteamFriends.ActivateGameOverlayToStore(nAppID, eFlag);
			}
		}

		internal override object PlatformID(DlcManager.DlcType dlcType)
		{
			DlcInfo.Dlc dlc;
			if (!GetDlc(dlcType, out dlc))
			{
				return AppId_t.Invalid;
			}
			return new AppId_t(dlc.SteamAppId);
		}

		private void RegisterDlcInstalled()
		{
			dlcInstalledCallback = Callback<DlcInstalled_t>.Create(OnDlcInstalled);
		}

		private void OnDlcInstalled(DlcInstalled_t param)
		{
			AppId_t appId = param.m_nAppID;
			DlcInfo.Dlc dlc = DlcInfo.Values.SingleOrDefault((DlcInfo.Dlc x) => x.SteamAppId == appId.m_AppId);
			if (dlc == null)
			{
				Debug.LogWarning("Dlc was installed but not from our game, app id: " + appId.m_AppId);
				return;
			}
			Debug.Log("Dlc was installed: " + dlc.dlcType);
			InvokePackageInstalled(dlc.dlcType);
		}

		private void UnRegisterDlcInstalled()
		{
			if (dlcInstalledCallback != null)
			{
				dlcInstalledCallback.Unregister();
			}
			dlcInstalledCallback = null;
		}
	}
}
