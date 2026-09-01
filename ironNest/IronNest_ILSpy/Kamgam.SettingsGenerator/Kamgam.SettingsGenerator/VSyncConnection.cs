using System;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class VSyncConnection : Connection<bool>
{
	[NonSerialized]
	protected bool vSyncEnabled;

	public override bool Get()
	{
		int vSyncCount = QualitySettings.vSyncCount;
		bool flag = vSyncCount == 0;
		return !flag;
	}

	public override void Set(bool vSyncEnabled)
	{
		QualitySettings.vSyncCount = (vSyncEnabled ? 1 : 0);
		base.NotifyListenersIfChanged(vSyncEnabled);
		this.vSyncEnabled = vSyncEnabled;
	}

	public override void OnQualityChanged(int qualityLevel)
	{
		Set(vSyncEnabled);
		base.OnQualityChanged(qualityLevel);
	}
}
