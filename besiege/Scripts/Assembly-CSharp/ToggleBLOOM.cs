using System;
using UnityEngine;

public class ToggleBLOOM : ToggleSetting
{
	public BloomAndLensFlares bloomCode;

	public override bool IsActive
	{
		get
		{
			return OptionsMaster.BesiegeConfig.Bloom;
		}
		set
		{
			OptionsMaster.BesiegeConfig.Bloom = value;
		}
	}

	protected override void Awake()
	{
		if (bloomCode == null)
		{
			bloomCode = Camera.main.gameObject.GetComponent<BloomAndLensFlares>();
		}
		ReferenceMaster.onBloomChanged = (Action)Delegate.Combine(ReferenceMaster.onBloomChanged, new Action(Set));
		base.Awake();
	}

	public override void OnClicked()
	{
		base.OnClicked();
		if (ReferenceMaster.onBloomChanged != null)
		{
			ReferenceMaster.onBloomChanged();
		}
	}

	protected override void OnDestroy()
	{
		ReferenceMaster.onBloomChanged = (Action)Delegate.Remove(ReferenceMaster.onBloomChanged, new Action(Set));
	}

	public override void Set()
	{
		base.Set();
		if (bloomCode != null && (double)SystemInfo.graphicsShaderLevel <= 2.0)
		{
			bloomCode.enabled = IsActive;
			if (IsActive)
			{
				OptionsMaster.SetBloom();
			}
		}
		else
		{
			OptionsMaster.SetBloom();
		}
	}
}
