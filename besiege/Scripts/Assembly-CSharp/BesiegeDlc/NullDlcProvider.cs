using System;
using System.Collections.Generic;

namespace BesiegeDlc
{
	internal sealed class NullDlcProvider : DlcProviderBase
	{
		internal NullDlcProvider(Dictionary<DlcManager.DlcType, DlcInfo.Dlc> list, Action providerInitialized, Action<DlcManager.DlcType> dlcPackageInstalled)
			: base(list, providerInitialized, dlcPackageInstalled)
		{
		}

		internal override void SetUp()
		{
		}

		internal override void CleanUp()
		{
		}

		internal override bool IsDlcIdInstalled(string s)
		{
			return false;
		}

		internal override bool HasPurchasedDlc(DlcManager.DlcType dlcType)
		{
			return false;
		}

		internal override void OpenDlcStore(DlcManager.DlcType dlcType)
		{
		}

		internal override object PlatformID(DlcManager.DlcType dlcType)
		{
			return null;
		}

		internal override void OnUserSignin()
		{
			InvokeProviderInitialized();
		}
	}
}
