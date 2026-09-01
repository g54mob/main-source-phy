using System;
using UnityEngine;

public class ToggleStressHighlight : ToggleSetting
{
	public override bool IsActive
	{
		get
		{
			return StatMaster.stressCoded;
		}
		set
		{
		}
	}

	protected override void Awake()
	{
		renderer = GetComponent<Renderer>();
		base.Set();
		ToggleSetting.DisableOthers = (Action<ToggleSetting>)Delegate.Combine(ToggleSetting.DisableOthers, new Action<ToggleSetting>(Disable));
	}

	public override void OnClicked()
	{
		Set();
	}

	public override void Set()
	{
		InvokeDisableOthers();
		BlockSkinLoader.SetStress(!StatMaster.stressCoded);
		base.Set();
	}

	protected override void Disable(ToggleSetting ignore)
	{
		if (!(ignore == this))
		{
			BlockSkinLoader.SetStress(false);
			base.Set();
		}
	}
}
