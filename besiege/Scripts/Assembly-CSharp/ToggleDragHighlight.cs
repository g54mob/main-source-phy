using System;
using UnityEngine;

public class ToggleDragHighlight : ToggleSetting
{
	public override bool IsActive
	{
		get
		{
			return StatMaster.aeroCoded;
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
		BlockSkinLoader.SetAero(!StatMaster.aeroCoded);
		base.Set();
	}

	protected override void Disable(ToggleSetting ignore)
	{
		if (!(ignore == this))
		{
			BlockSkinLoader.SetAero(false);
			base.Set();
		}
	}
}
