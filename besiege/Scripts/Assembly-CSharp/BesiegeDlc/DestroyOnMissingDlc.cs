using System;
using UnityEngine;

namespace BesiegeDlc
{
	internal sealed class DestroyOnMissingDlc : MonoBehaviour
	{
		public DlcManager.DlcType Dlc;

		public bool keepEnabledMP;

		private bool init;

		private void Start()
		{
			DlcManager instance = DlcManager.Instance;
			instance.DlcSettingsChanged = (Action)Delegate.Combine(instance.DlcSettingsChanged, new Action(OnDlcChanged));
			init = true;
			OnDlcChanged();
		}

		private void OnDestroy()
		{
			if (init && DlcManager.Instance != null)
			{
				DlcManager instance = DlcManager.Instance;
				instance.DlcSettingsChanged = (Action)Delegate.Remove(instance.DlcSettingsChanged, new Action(OnDlcChanged));
			}
		}

		public void OnDlcChanged()
		{
			switch (DlcManager.Instance.GetDlcStatus(Dlc))
			{
			case DlcManager.DlcStatusType.MissingDlc:
				UnityEngine.Object.Destroy(base.gameObject);
				break;
			case DlcManager.DlcStatusType.DisabledOnServer:
				base.gameObject.SetActive(keepEnabledMP);
				break;
			case DlcManager.DlcStatusType.Allowed:
				base.gameObject.SetActive(true);
				break;
			}
		}
	}
}
