using System;
using UnityEngine;

public class ToggleAO : ToggleSetting
{
	public SSAOEffectDepthCutoff aoCode;

	public SSAOPro aoCodePro;

	public override bool IsActive
	{
		get
		{
			return OptionsMaster.BesiegeConfig.ScreenSpaceAmbientOcclusion;
		}
		set
		{
			OptionsMaster.BesiegeConfig.ScreenSpaceAmbientOcclusion = value;
		}
	}

	protected override void Awake()
	{
		if (aoCodePro == null)
		{
			aoCodePro = Camera.main.gameObject.GetComponent<SSAOPro>();
		}
		if (aoCode == null)
		{
			aoCode = Camera.main.gameObject.GetComponent<SSAOEffectDepthCutoff>();
		}
		ReferenceMaster.onSSAOChanged = (Action)Delegate.Combine(ReferenceMaster.onSSAOChanged, new Action(Set));
		base.Awake();
	}

	protected override void OnDestroy()
	{
		ReferenceMaster.onSSAOChanged = (Action)Delegate.Remove(ReferenceMaster.onSSAOChanged, new Action(Set));
	}

	public override void OnClicked()
	{
		base.OnClicked();
		if (ReferenceMaster.onSSAOChanged != null)
		{
			ReferenceMaster.onSSAOChanged();
		}
	}

	public override void Set()
	{
		base.Set();
		if (aoCode != null)
		{
			aoCodePro.enabled = IsActive;
		}
	}
}
