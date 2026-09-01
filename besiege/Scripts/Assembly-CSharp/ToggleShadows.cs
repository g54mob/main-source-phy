using System;
using UnityEngine;

public class ToggleShadows : ToggleSetting
{
	public Light lighty;

	public override bool IsActive
	{
		get
		{
			return OptionsMaster.BesiegeConfig.ShadowsEnabled;
		}
		set
		{
			OptionsMaster.BesiegeConfig.ShadowsEnabled = value;
		}
	}

	protected override void Awake()
	{
		if (lighty == null)
		{
			lighty = GameObject.Find("Directional light").GetComponent<Light>();
		}
		ReferenceMaster.onShadowsChanged = (Action)Delegate.Combine(ReferenceMaster.onShadowsChanged, new Action(Set));
		base.Awake();
		Set();
	}

	protected override void OnDestroy()
	{
		ReferenceMaster.onShadowsChanged = (Action)Delegate.Remove(ReferenceMaster.onShadowsChanged, new Action(Set));
	}

	public override void OnClicked()
	{
		base.OnClicked();
		Set();
	}

	public override void Set()
	{
		base.Set();
		OptionsMaster.SetShadows(IsActive, lighty);
	}
}
