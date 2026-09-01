using System;
using System.Collections.Generic;
using Localisation;
using UnityEngine;

namespace BesiegeDlc
{
	internal abstract class DlcProviderBase
	{
		protected readonly Dictionary<DlcManager.DlcType, DlcInfo.Dlc> DlcInfo;

		private readonly Action providerInitialized;

		private readonly Action<DlcManager.DlcType> dlcPackageInstalled;

		private readonly Dictionary<DlcManager.DlcType, bool> unlockStates;

		protected DlcProviderBase(Dictionary<DlcManager.DlcType, DlcInfo.Dlc> dlcInfo, Action providerInitializedCallback, Action<DlcManager.DlcType> dlcPackageInstalledCallback)
		{
			DlcInfo = dlcInfo;
			providerInitialized = providerInitializedCallback;
			dlcPackageInstalled = dlcPackageInstalledCallback;
			unlockStates = new Dictionary<DlcManager.DlcType, bool>();
		}

		internal abstract void SetUp();

		internal abstract void CleanUp();

		internal bool GetDlc(DlcManager.DlcType type, out DlcInfo.Dlc dlc)
		{
			return DlcInfo.TryGetValue(type, out dlc);
		}

		internal virtual string Name(DlcManager.DlcType dlcType)
		{
			DlcInfo.Dlc dlc;
			if (!GetDlc(dlcType, out dlc))
			{
				return LocalisationManager.GetTranslation(1934);
			}
			return LocalisationManager.GetTranslation(dlc.LocID);
		}

		internal virtual Sprite Icon(DlcManager.DlcType dlcType)
		{
			DlcInfo.Dlc dlc;
			if (!GetDlc(dlcType, out dlc))
			{
				return null;
			}
			return dlc.Icon;
		}

		internal abstract bool IsDlcIdInstalled(string s);

		internal abstract object PlatformID(DlcManager.DlcType dlcType);

		internal abstract bool HasPurchasedDlc(DlcManager.DlcType dlcType);

		internal abstract void OpenDlcStore(DlcManager.DlcType dlcType);

		internal abstract void OnUserSignin();

		internal virtual void OnUpdate()
		{
		}

		protected bool TryGetUnlockState(DlcManager.DlcType dlcType, out bool isUnlocked)
		{
			return unlockStates.TryGetValue(dlcType, out isUnlocked);
		}

		protected void SetUnlockState(DlcManager.DlcType dlcType, bool unlockState)
		{
			if (!unlockStates.ContainsKey(dlcType))
			{
				unlockStates.Add(dlcType, unlockState);
			}
			else
			{
				unlockStates[dlcType] = unlockState;
			}
		}

		protected void InvokeProviderInitialized()
		{
			if (providerInitialized != null)
			{
				providerInitialized();
			}
		}

		protected void InvokePackageInstalled(DlcManager.DlcType dlcType)
		{
			if (dlcPackageInstalled != null)
			{
				dlcPackageInstalled(dlcType);
			}
		}
	}
}
