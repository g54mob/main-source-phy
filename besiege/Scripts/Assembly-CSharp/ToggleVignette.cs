using System;
using UnityEngine;

public class ToggleVignette : ToggleSetting
{
	public Vignetting vigCode;

	public override bool IsActive
	{
		get
		{
			return OptionsMaster.BesiegeConfig.Vignette;
		}
		set
		{
			OptionsMaster.BesiegeConfig.Vignette = value;
		}
	}

	protected override void Awake()
	{
		if (vigCode == null)
		{
			vigCode = Camera.main.gameObject.GetComponent<Vignetting>();
		}
		ReferenceMaster.onVignetteChanged = (Action)Delegate.Combine(ReferenceMaster.onVignetteChanged, new Action(Set));
		base.Awake();
	}

	public override void OnClicked()
	{
		base.OnClicked();
		Set();
	}

	protected override void OnDestroy()
	{
		ReferenceMaster.onVignetteChanged = (Action)Delegate.Remove(ReferenceMaster.onVignetteChanged, new Action(Set));
	}

	public override void Set()
	{
		base.Set();
		if (vigCode != null)
		{
			vigCode.intensity = ((!IsActive) ? 2f : 3.5f);
		}
	}
}
